import { NgModule } from '@angular/core'
import { SelectorComponent } from './components/selector.component'
import { SharedModule } from '../shared/shared.module'
import { ExplorerComponent } from './pages/explorer.component'
import { MonedaRoutingModule } from './moneda-routing.module'

@NgModule({
  declarations: [SelectorComponent, ExplorerComponent],
  imports: [
    MonedaRoutingModule,
    SharedModule
  ],
  exports: [SelectorComponent]
})
export class MonedaModule { }
