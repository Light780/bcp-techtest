import { Component } from '@angular/core'
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms'
import { ConvertTipoCambioRequest, MonedaDTO, MonedaService, TipoCambioService } from 'src/app/core'

@Component({
  selector: 'app-convert-amount',
  templateUrl: './convert-amount.component.html',
  styleUrls: ['./convert-amount.component.css']
})
export class ConvertAmountComponent {
  monedasOrigen: MonedaDTO[] = []
  monedasDestino: MonedaDTO[] = []

  convertForm: FormGroup

  constructor (
    private readonly monedaService: MonedaService,
    private readonly tipoCambioService: TipoCambioService,
    private readonly fb: FormBuilder
  ) {
    this.getMonedas()
    this.convertForm = this.fb.group({
      monto: new FormControl(0, [Validators.required, Validators.min(1)]),
      monedaOrigen: new FormControl('', [Validators.required]),
      monedaDestino: new FormControl('', [Validators.required]),
      montoConvertido: new FormControl({ value: '', disabled: true })
    })
  }

  getMonedas (): void {
    this.monedaService.getAll().subscribe(response => {
      this.monedasOrigen = response.filter(m => m.codigoSunat === 'PEN')
      this.monedasDestino = response.filter(m => m.codigoSunat !== 'PEN')
      this.setDefaultValues()
    })
  }

  setDefaultValues (): void {
    this.convertForm.patchValue({
      monedaOrigen: this.monedasOrigen[0].codigoSunat,
      monedaDestino: this.monedasDestino.find(m => m.codigoSunat === 'USD')?.codigoSunat ?? ''
    })
    this.convertForm.controls['monedaOrigen'].disable()
  }

  submitForm (): void {
    if (this.convertForm.invalid) { return }

    const { monto, monedaOrigen, monedaDestino } = this.convertForm.getRawValue()

    const request: ConvertTipoCambioRequest = {
      monto,
      monedaOrigen,
      monedaDestino
    }

    this.tipoCambioService.convertAmount(request).subscribe(response => {
      this.convertForm.patchValue({
        montoConvertido: response.montoConvertido
      })
    })
  }

  invertirMoneda (): void {
    const aux = [...this.monedasOrigen]
    this.monedasOrigen = [...this.monedasDestino]
    this.monedasDestino = aux

    this.convertForm.patchValue({
      monedaOrigen: this.convertForm.get('monedaDestino')?.value,
      monedaDestino: this.convertForm.get('monedaOrigen')?.value
    })

    if (this.convertForm.get('monedaOrigen')?.value === 'PEN') {
      this.convertForm.controls['monedaOrigen'].disable()
      this.convertForm.controls['monedaDestino'].enable()
    } else {
      this.convertForm.controls['monedaDestino'].disable()
      this.convertForm.controls['monedaOrigen'].enable()
    }
  }
}
