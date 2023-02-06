import { Component } from '@angular/core'
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms'
import { MatDialog } from '@angular/material/dialog'
import { CreateTipoCambioRequest, TipoCambioDTO, TipoCambioService, UpdateTipoCambioRequest } from 'src/app/core'
import { FormDialogComponent } from '../components/form-dialog.component'

@Component({
  selector: 'app-management',
  templateUrl: './management.component.html',
  styleUrls: ['./management.component.css']
})
export class ManagementComponent {
  tiposCambio: TipoCambioDTO[]
  tcForm: FormGroup

  constructor (
    private readonly tipoCambioService: TipoCambioService,
    private readonly fb: FormBuilder,
    private readonly dialog: MatDialog
  ) {
    this.getTipoCambios()
    this.tcForm = this.fb.group({
      id: new FormControl(''),
      moneda: new FormControl('', [Validators.required]),
      fecha: new FormControl(new Date(), [Validators.required]),
      compra: new FormControl('', [Validators.required, Validators.min(1)]),
      venta: new FormControl('', [Validators.required, Validators.min(1)])
    })
  }

  getTipoCambios (): void {
    this.tipoCambioService.get({ moneda: '' }).subscribe(response => {
      this.tiposCambio = [...response]
    })
  }

  saveTipoCambio (): void {
    const { id, moneda, fecha, compra, venta } = this.tcForm.value

    if (id === '') {
      const request: CreateTipoCambioRequest = {
        moneda,
        fecha,
        compra,
        venta
      }
      this.tipoCambioService.create(request).subscribe(_ => this.getTipoCambios())
    } else {
      const request: UpdateTipoCambioRequest = {
        id,
        compra,
        venta
      }
      this.tipoCambioService.update(request).subscribe(_ => this.getTipoCambios())
    }

    this.tcForm.reset({
      id: '',
      moneda: '',
      fecha: new Date(),
      compra: '',
      venta: ''
    })
  }

  edit (tc: TipoCambioDTO): void {
    this.tcForm.patchValue({
      id: tc.id,
      moneda: tc.moneda.substring(0, 3),
      fecha: new Date(tc.fecha),
      compra: tc.compra,
      venta: tc.venta
    })
    this.tcForm.controls['moneda'].disable()
    this.tcForm.controls['fecha'].disable()
    this.openDialog(true)
  }

  new (): void {
    this.tcForm.controls['moneda'].enable()
    this.tcForm.controls['fecha'].enable()
    this.openDialog(false)
  }

  delete (id: string): void {
    this.tipoCambioService.delete({ id }).subscribe(() => this.getTipoCambios())
  }

  openDialog (nuevo: boolean): void {
    const dialogRef = this.dialog.open(FormDialogComponent, {
      width: '450px'
    })
    dialogRef.componentInstance.parentForm = this.tcForm
    dialogRef.componentInstance.isEdit = nuevo
    dialogRef.afterClosed().subscribe((res: boolean) => {
      if (res) {
        this.saveTipoCambio()
      }
    })
  }
}
