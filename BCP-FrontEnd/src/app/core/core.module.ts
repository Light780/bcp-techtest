import { NgModule } from '@angular/core'
import { CommonModule } from '@angular/common'
import { HTTP_INTERCEPTORS } from '@angular/common/http'
import { HttpTokenInterceptor } from './interceptors'
import {
  ApiService,
  AuthGuard,
  JwtService,
  LoadingService,
  MonedaService,
  SwalService,
  TipoCambioService,
  UsuarioService
} from './services'

@NgModule({
  declarations: [],
  imports: [
    CommonModule

  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: HttpTokenInterceptor, multi: true },
    ApiService,
    AuthGuard,
    JwtService,
    MonedaService,
    TipoCambioService,
    UsuarioService,
    SwalService,
    LoadingService
  ]
})
export class CoreModule {
}
