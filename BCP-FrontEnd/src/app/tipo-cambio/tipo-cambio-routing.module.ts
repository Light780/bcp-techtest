import { NgModule } from '@angular/core'
import { RouterModule, Routes } from '@angular/router'
import { ConvertAmountComponent } from './pages/convert-amount.component'
import { AuthGuard } from '../core'
import { ManagementComponent } from './pages/management.component'

const routes: Routes = [
  {
    path: 'convert-amount',
    component: ConvertAmountComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'management',
    component: ManagementComponent,
    canActivate: [AuthGuard]
  }
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TipoCambioRoutingModule { }
