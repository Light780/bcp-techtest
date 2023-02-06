import { Component, EventEmitter, Input, Output } from '@angular/core'
import { SwalService, TipoCambioDTO } from 'src/app/core'

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.css']
})
export class TableComponent {
  @Input() tiposCambio: TipoCambioDTO[]
  @Output() edit = new EventEmitter<TipoCambioDTO>()
  @Output() delete = new EventEmitter<string>()
  columnsToDisplay = ['moneda', 'fecha', 'compra', 'venta', 'acciones']

  constructor (
    private readonly swal: SwalService
  ) {}

  onEdit (tc: TipoCambioDTO): void {
    this.edit.emit(tc)
  }

  onDelete (id: string): void {
    this.swal.showConfirmation('¿Está seguro de eliminar el tipo de cambio?', () => this.delete.emit(id))
  }
}
