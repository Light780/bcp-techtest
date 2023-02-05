import { Injectable } from '@angular/core'
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router'
import { Observable } from 'rxjs'
import { map, take } from 'rxjs/operators'
import { UsuarioService } from '.'

@Injectable()
export class AuthGuard implements CanActivate {
  constructor (
    private readonly usuarioService: UsuarioService,
    private readonly router: Router
  ) {}

  canActivate (
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> | boolean {
    return this.usuarioService.isAuthenticated.pipe(take(1), map(
      isAuth => {
        if (isAuth) return true

        void this.router.navigateByUrl('/auth/login')
        return false
      }
    ))
  }
}
