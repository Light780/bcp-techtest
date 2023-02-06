import { Component } from '@angular/core'
import { MonedaDTO, MonedaService } from 'src/app/core'

@Component({
  selector: 'app-explorer',
  templateUrl: './explorer.component.html',
  styleUrls: ['./explorer.component.css']
})
export class ExplorerComponent {
  monedas: MonedaDTO[]

  columnsToDisplay = ['codigoSunat', 'nombre']

  constructor (
    private readonly monedaService: MonedaService
  ) {
    this.monedaService.getAll().subscribe(response => {
      this.monedas = response
    })
  }
}
