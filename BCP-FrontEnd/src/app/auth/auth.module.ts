import {NgModule} from '@angular/core';
import {AuthComponent} from './pages/auth.component';
import {AuthRoutingModule} from "./auth-routing.module";
import {NoAuthGuard} from "./services/no-auth-guard.service";
import {MAT_FORM_FIELD_DEFAULT_OPTIONS, MatFormFieldModule} from "@angular/material/form-field";
import {MatSelectModule} from "@angular/material/select";
import {MatInputModule} from "@angular/material/input";
import {MatGridListModule} from "@angular/material/grid-list";
import {MatButtonModule} from "@angular/material/button";
import {SharedModule} from "../shared/shared.module";

@NgModule({
  declarations: [
    AuthComponent
  ],
  imports: [
    AuthRoutingModule,
    SharedModule,
    MatInputModule,
    MatFormFieldModule,
    MatSelectModule,
    MatGridListModule,
    MatButtonModule
  ],
  providers: [
    {provide: MAT_FORM_FIELD_DEFAULT_OPTIONS, useValue: {appearance: 'outline'}},
    NoAuthGuard,
  ]
})
export class AuthModule {
}
