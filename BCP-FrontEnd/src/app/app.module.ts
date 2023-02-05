import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {AppComponent} from './app.component';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {AuthModule} from "./auth/auth.module";
import {TipoCambioModule} from "./tipo-cambio/tipo-cambio.module";
import {AppRoutingModule} from "./app-routing.module";
import {CoreModule} from "./core";
import {SharedModule} from "./shared/shared.module";

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    SharedModule,
    CoreModule,
    AuthModule,
    TipoCambioModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
}
