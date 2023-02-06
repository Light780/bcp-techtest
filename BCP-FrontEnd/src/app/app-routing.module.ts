import { NgModule } from '@angular/core'
import { PreloadAllModules, RouterModule, Routes } from '@angular/router'
import { AuthModule } from './auth/auth.module'
import { HomeModule } from './home/home.module'
import { MonedaModule } from './moneda/moneda.module'
import { SidenavComponent } from './shared/components/sidenav.component'
import { TipoCambioModule } from './tipo-cambio/tipo-cambio.module'

const routes: Routes = [
  {
    path: 'tipo-cambio',
    component: SidenavComponent,
    loadChildren: async (): Promise<typeof TipoCambioModule> => await import('./tipo-cambio/tipo-cambio.module').then(m => m.TipoCambioModule)
  },
  {
    path: 'home',
    component: SidenavComponent,
    loadChildren: async (): Promise<typeof HomeModule> => await import('./home/home.module').then(m => m.HomeModule)
  },
  {
    path: 'auth',
    loadChildren: async (): Promise<typeof AuthModule> => await import('./auth/auth.module').then(m => m.AuthModule)
  },
  {
    path: 'moneda',
    component: SidenavComponent,
    loadChildren: async (): Promise<typeof MonedaModule> => await import('./moneda/moneda.module').then(m => m.MonedaModule)
  },
  { path: '**', redirectTo: 'home' }
]
@NgModule({
  declarations: [],
  imports: [
    RouterModule.forRoot(routes, {
      preloadingStrategy: PreloadAllModules
    })
  ]
})
export class AppRoutingModule { }
