import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {PreloadAllModules, RouterModule, Routes} from "@angular/router";

const routes: Routes = [
  {
    path:'tipo-cambio',
    loadChildren: () => import('./tipo-cambio/tipo-cambio.module').then(m => m.TipoCambioModule)
  },
  {
    path:'auth',
    loadChildren: () => import('./auth/auth.module').then(m => m.AuthModule)
  }
]
@NgModule({
  declarations: [],
  imports: [
    RouterModule.forRoot(routes, {
      preloadingStrategy: PreloadAllModules,
    })
  ]
})
export class AppRoutingModule { }
