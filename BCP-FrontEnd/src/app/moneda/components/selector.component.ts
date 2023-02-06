import { Component, Input } from '@angular/core'
import { FormGroup } from '@angular/forms'
import { MonedaDTO } from 'src/app/core'

@Component({
  selector: 'app-selector',
  templateUrl: './selector.component.html',
  styleUrls: ['./selector.component.css']
})
export class SelectorComponent {
  @Input() label: string
  @Input() monedas: MonedaDTO[]
  @Input() parentGroup: FormGroup
  @Input() control: string
}
