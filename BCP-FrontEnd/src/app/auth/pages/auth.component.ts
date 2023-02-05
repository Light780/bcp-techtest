import { Component, OnInit } from '@angular/core'
import { UsuarioService } from '../../core'
import { ActivatedRoute, Router } from '@angular/router'
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms'

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css']
})
export class AuthComponent implements OnInit {
  authType: string = ''
  title: string = ''
  buttonText: string = ''
  authForm: FormGroup

  constructor (
    private readonly route: ActivatedRoute,
    private readonly router: Router,
    private readonly usuarioService: UsuarioService,
    private readonly fb: FormBuilder
  ) {
    this.authForm = this.fb.group({
      correo: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]]
    })
  }

  ngOnInit (): void {
    this.route.url.subscribe(data => {
      this.authType = data[data.length - 1].path
      this.title = this.authType === 'login' ? 'Login' : 'Crear Cuenta'
      this.buttonText = this.authType === 'login' ? 'Ingresar' : 'Crear Cuenta'
      if (this.authType === 'register') {
        this.authForm.controls['correo'].addValidators([
          Validators.minLength(10),
          Validators.maxLength(50)
        ])

        this.authForm.controls['password'].addValidators([
          Validators.minLength(8),
          Validators.maxLength(32)
        ])

        this.authForm.addControl('nombreCompleto', new FormControl('', [
          Validators.required,
          Validators.minLength(8),
          Validators.maxLength(50)
        ]))
      }
    })
  }

  submitForm (): void {
    const credentials = this.authForm.value
    if (this.authForm.invalid) { return }

    this.usuarioService
      .attemptAuth(this.authType, credentials)
      .subscribe({
        next: (response) => {
          void this.router.navigateByUrl('/')
        }
      })
  }
}
