import { Injectable } from '@angular/core'
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router'
import { map, Observable } from 'rxjs'
import { UsuarioService } from '../../core'
import { take } from 'rxjs/operators'

@Injectable({
  providedIn: 'root'
})
export class NoAuthGuard implements CanActivate {
  constructor (
    private readonly router: Router,
    private readonly usuarioService: UsuarioService
  ) { }

  canActivate (route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
    return this.usuarioService.isAuthenticated.pipe(take(1), map(isAuth => {
      if (!isAuth) return true
      void this.router.navigateByUrl('')
      return false
    }))
  }
}
