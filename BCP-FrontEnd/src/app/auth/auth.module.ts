import { NgModule } from '@angular/core'
import { AuthComponent } from './pages/auth.component'
import { AuthRoutingModule } from './auth-routing.module'
import { NoAuthGuard } from './services/no-auth-guard.service'
import { SharedModule } from '../shared/shared.module'
@NgModule({
  declarations: [
    AuthComponent
  ],
  imports: [
    AuthRoutingModule,
    SharedModule
  ],
  providers: [
    NoAuthGuard
  ]
})
export class AuthModule {
}
