import { Injectable } from '@angular/core'
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot } from '@angular/router'
import { Observable } from 'rxjs'
import { take } from 'rxjs/operators'
import { UsuarioService } from '.'

@Injectable()
export class AuthGuard implements CanActivate {
  constructor (
    private readonly usuarioService: UsuarioService
  ) {}

  canActivate (
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> {
    return this.usuarioService.isAuthenticated.pipe(take(1))
  }
}
