import { NgModule } from '@angular/core'
import { ConvertAmountComponent } from './pages/convert-amount.component'
import { TipoCambioRoutingModule } from './tipo-cambio-routing.module'
import { SharedModule } from '../shared/shared.module'
import { MonedaModule } from '../moneda/moneda.module'
import { ManagementComponent } from './pages/management.component'
import { FormDialogComponent } from './components/form-dialog.component'
import { TableComponent } from './components/table.component'
@NgModule({
  declarations: [
    ConvertAmountComponent,
    ManagementComponent,
    FormDialogComponent,
    TableComponent
  ],
  imports: [
    TipoCambioRoutingModule,
    SharedModule,
    MonedaModule
  ]
})
export class TipoCambioModule { }
