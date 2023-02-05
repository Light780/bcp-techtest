import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http'
import { Injectable } from '@angular/core'
import { Observable } from 'rxjs'
import { JwtService } from '../services'

@Injectable()
export class HttpTokenInterceptor implements HttpInterceptor {
  constructor (
    private readonly jwtService: JwtService
  ) { }

  intercept (req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const headerConfig: any = {
      'Content-Type': 'application/json',
      Accept: 'application/json'
    }

    const token = this.jwtService.getToken()

    if (token !== undefined) {
      headerConfig.Authorization = `Bearer ${token}`
    }

    const request = req.clone({ setHeaders: headerConfig })
    return next.handle(request)
  }
}
