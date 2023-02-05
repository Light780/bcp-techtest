import { Injectable } from '@angular/core'
import { BehaviorSubject, Observable, ReplaySubject } from 'rxjs'
import { distinctUntilChanged, map } from 'rxjs/operators'
import { UsuarioDTO } from '../models'
import { ApiService, JwtService } from './'

const initialValue: UsuarioDTO = {
  correo: '',
  nombreCompleto: '',
  token: ''
}

@Injectable()
export class UsuarioService {
  private readonly currentUserSubject = new BehaviorSubject<UsuarioDTO>(initialValue)
  public currentUser = this.currentUserSubject.asObservable().pipe(distinctUntilChanged())

  private readonly isAuthenticatedSubject = new ReplaySubject<boolean>(1)
  public isAuthenticated = this.isAuthenticatedSubject.asObservable()

  constructor (
    private readonly apiService: ApiService,
    private readonly jwtService: JwtService
  ) {
  }

  populate (): void {
    if (this.jwtService.getToken() !== undefined) {
      this.apiService.get<UsuarioDTO>('usuario')
        .subscribe({
          next: (response) => {
            this.setAuth(response)
          },
          error: (_) => {
            this.purgeAuth()
          }
        })
    } else {
      this.purgeAuth()
    }
  }

  setAuth (usuario: UsuarioDTO): void {
    this.jwtService.setToken(usuario.token)
    this.currentUserSubject.next(usuario)
    this.isAuthenticatedSubject.next(true)
  }

  purgeAuth (): void {
    this.jwtService.deleteToken()
    this.currentUserSubject.next(initialValue)
    this.isAuthenticatedSubject.next(false)
  }

  attemptAuth (type: string, request: any): Observable<UsuarioDTO> {
    const route = (type === 'login') ? '/login' : '/register'
    return this.apiService.post<UsuarioDTO>('usuario' + route, request)
      .pipe(map(
        response => {
          this.setAuth(response)
          return response
        }
      ))
  }

  getCurrentUser (): UsuarioDTO {
    return this.currentUserSubject.value
  }
}
