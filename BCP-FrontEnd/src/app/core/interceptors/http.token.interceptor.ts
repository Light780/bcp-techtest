import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http'
import { Injectable } from '@angular/core'
import { finalize, Observable } from 'rxjs'
import { JwtService } from '../services'
import { LoadingService } from '../services/loading.service'

@Injectable()
export class HttpTokenInterceptor implements HttpInterceptor {
  constructor (
    private readonly jwtService: JwtService,
    private readonly loader: LoadingService
  ) { }

  intercept (req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    this.loader.show()
    const headerConfig: any = {
      'Content-Type': 'application/json',
      Accept: 'application/json'
    }

    const token = this.jwtService.getToken()

    if (token !== undefined) {
      headerConfig.Authorization = `Bearer ${token}`
    }

    const request = req.clone({ setHeaders: headerConfig })
    return next.handle(request).pipe(finalize(() => this.loader.hide()))
  }
}
