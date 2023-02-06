import { Component, OnInit } from '@angular/core'
import { UsuarioService } from './core'
import { LoadingService } from './core/services/loading.service'

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'BCP'

  loading$ = this.loader.loading$

  constructor (
    private readonly usuarioService: UsuarioService,
    private readonly loader: LoadingService
  ) {}

  ngOnInit (): void {
    this.usuarioService.populate()
  }
}
