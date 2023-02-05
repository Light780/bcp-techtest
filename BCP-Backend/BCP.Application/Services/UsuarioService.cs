using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BCP.Application.DTOs.Usuario;
using BCP.Application.Exceptions;
using BCP.Application.Interfaces.Common;
using BCP.Application.Interfaces.Security;
using BCP.Application.Interfaces.Services;
using BCP.Application.Wrappers;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;
namespace BCP.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IUsuarioSession _usuarioSession;

        public UsuarioService(IApplicationDbContext context, IMapper mapper, IJwtGenerator jwtGenerator
            , IUsuarioSession usuarioSession)
        {
            _context = context;
            _mapper = mapper;
            _jwtGenerator = jwtGenerator;
            _usuarioSession = usuarioSession;
        }
        
        public async Task<Response<LoginUsuarioResponse>> Register(RegisterUsuarioRequest request)
        {
            var validator = new RegisterUsuarioValidator();
            var validatorResult = await validator.ValidateAsync(request);
            if (!validatorResult.IsValid)
                throw new ValidationException(validatorResult.Errors);

            var existUserWithEmail = await _context.Usuarios.AnyAsync(u => u.Correo == request.Correo);
            if (existUserWithEmail)
                throw new ApiException("Ya existe un usuario registrado con ese email");

            var usuario = _mapper.Map<Domain.Entities.Usuario>(request);
            usuario.Id = Guid.NewGuid();
            usuario.Password = BC.HashPassword(request.Password);

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync(default);

            var loginResponse = new LoginUsuarioResponse
            {
                NombreCompleto = usuario.NombreCompleto,
                Correo = usuario.Correo,
                Token = _jwtGenerator.CreateToken(usuario)
            };
            
            return new Response<LoginUsuarioResponse>(loginResponse, "Te has registrado exitosamente");
        }

        public async Task<Response<LoginUsuarioResponse>> Login(LoginUsuarioRequest request)
        {
            var validator = new LoginUsuarioValidator();
            var validatorResult = await validator.ValidateAsync(request);
            if (!validatorResult.IsValid)
                throw new ValidationException(validatorResult.Errors);

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == request.Correo);
            if (usuario is null || !BC.Verify(request.Password, usuario.Password))
                throw new ApiException("Credenciales incorrectas");

            var loginResponse = new LoginUsuarioResponse
            {
                NombreCompleto = usuario.NombreCompleto,
                Correo = usuario.Correo,
                Token = _jwtGenerator.CreateToken(usuario)
            };
            
            return new Response<LoginUsuarioResponse>(loginResponse);
        }

        public async Task<Response<LoginUsuarioResponse>> GetByToken()
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == _usuarioSession.GetUsuarioSession());
            if (usuario is null)
                throw new ApiException("Usuario no existe");
                
            var loginResponse = new LoginUsuarioResponse
            {
                NombreCompleto = usuario.NombreCompleto,
                Correo = usuario.Correo,
                Token = _jwtGenerator.CreateToken(usuario)
            };
            
            return new Response<LoginUsuarioResponse>(loginResponse);
        }
    }
}