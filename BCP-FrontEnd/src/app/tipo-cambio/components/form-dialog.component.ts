import { Component, Input } from '@angular/core'
import { FormGroup } from '@angular/forms'
import { MatDialogRef } from '@angular/material/dialog'
import { MonedaDTO, MonedaService } from 'src/app/core'

@Component({
  selector: 'app-form-dialog',
  templateUrl: './form-dialog.component.html',
  styleUrls: ['./form-dialog.component.css']
})
export class FormDialogComponent {
  @Input() parentForm: FormGroup
  @Input() isEdit: boolean

  monedas: MonedaDTO[]
  constructor (
    private readonly monedaService: MonedaService,
    public dialogRef: MatDialogRef<FormDialogComponent>
  ) {
    this.getMonedas()
  }

  getMonedas (): void {
    this.monedaService.getAll().subscribe(response => {
      this.monedas = response.filter(moneda => moneda.codigoSunat !== 'PEN')
    })
  }

  save (): void {
    if (!this.parentForm.valid) return

    this.dialogRef.close(true)
  }

  closeDialog (): void {
    this.dialogRef.close(false)
    this.parentForm.reset({
      id: '',
      moneda: '',
      fecha: new Date(),
      compra: '',
      venta: ''
    })
  }
}
