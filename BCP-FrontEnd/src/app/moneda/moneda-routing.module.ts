import { NgModule } from '@angular/core'
import { RouterModule, Routes } from '@angular/router'
import { ExplorerComponent } from './pages/explorer.component'
import { AuthGuard } from '../core'

const routes: Routes = [
  {
    path: 'explorer',
    component: ExplorerComponent,
    canActivate: [AuthGuard]
  }
]

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MonedaRoutingModule { }
