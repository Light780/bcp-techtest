import { Component, Input, OnInit } from '@angular/core'
import { MatSidenav } from '@angular/material/sidenav'
import { Router } from '@angular/router'
import { UsuarioDTO, UsuarioService } from 'src/app/core'

@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.css']
})
export class ToolbarComponent implements OnInit {
  @Input() sideNav: MatSidenav

  currentUser: UsuarioDTO

  constructor (
    private readonly usuarioService: UsuarioService,
    private readonly router: Router
  ) {}

  ngOnInit (): void {
    this.usuarioService.currentUser.subscribe(
      (usuario) => {
        this.currentUser = usuario
      }
    )
  }

  onLogout (): void {
    this.usuarioService.purgeAuth()
    void this.router.navigateByUrl('/auth/login')
  }
}
