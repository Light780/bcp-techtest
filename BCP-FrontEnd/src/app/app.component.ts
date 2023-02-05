import { Component, OnInit } from '@angular/core'
import { UsuarioService } from './core'

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'BCP'

  constructor (
    private readonly usuarioService: UsuarioService
  ) {}

  ngOnInit (): void {
    this.usuarioService.populate()
  }
}
