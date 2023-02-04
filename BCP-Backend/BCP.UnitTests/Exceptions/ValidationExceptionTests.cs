using System.Collections.Generic;
using System.Linq;
using BCP.Application.Exceptions;
using FluentValidation.Results;
using Xunit;

namespace BCP.UnitTests.Exceptions
{
    public class ValidationExceptionTests
    {
        [Fact]
        public void DefaultConstructor_CreatesEmptyList()
        {
            var errors = new ValidationException().Errors;
            Assert.Empty(errors);
        }

        [Fact]
        public void SingleValidationFailure_CreatesSingleElementList()
        {
            var failures = new List<ValidationFailure>
            {
                new ValidationFailure("Edad", "Debe ser mayor de 18")
            };
            
            var errors = new ValidationException(failures).Errors;
            
            Assert.Single(errors);
        }
        
        [Fact]
        public void MultipleValidationFailure_CreatesMultiplesElementList()
        {
            var failures = new List<ValidationFailure>
            {
                new ValidationFailure("Edad", "Debe ser mayor de 18"),
                new ValidationFailure("Correo", "Debe ser valido"),
                new ValidationFailure("Password", "Debe contener caracteres especiales")
            };
            
            var errors = new ValidationException(failures).Errors;
            
            Assert.True(errors.Count > 1);
        }
    }
}