import { Injectable } from '@angular/core'
import { Response } from '../models'
import { Observable, throwError } from 'rxjs'
import Swal from 'sweetalert2'
import { HttpErrorResponse } from '@angular/common/http'

@Injectable({
  providedIn: 'root'
})
export class SwalService {
  public showErrors (httpError: HttpErrorResponse): Observable<never> {
    let message: string = httpError.error.message
    const errors: string[] = httpError.error.errors ?? []
    if (errors.length !== 0) {
      httpError.error.errors.forEach((error: string) => {
        message += `<br> ${error}`
      })
    }

    void Swal.fire({
      customClass: {
        confirmButton: 'background-warn'
      },
      title: 'Ocurrio un error',
      html: message,
      icon: 'error'
    })

    return throwError(() => httpError.message)
  }

  public showMessage (response: Response<any>): void {
    if (response.message === null) { return }

    void Swal.fire({
      customClass: {
        confirmButton: 'background-warn'
      },
      title: 'Operacion exitosa',
      text: response.message,
      icon: 'success',
      background: 'mat-accent'
    })
  }

  public showConfirmation (title: string, callback: Function): void {
    Swal.fire({
      customClass: {
        confirmButton: 'background-warn',
        cancelButton: 'background-primary'
      },
      title,
      icon: 'question',
      background: 'mat-accent',
      showCancelButton: true,
      confirmButtonText: 'Confirmar',
      cancelButtonText: 'Cancelar'
    }).then((result) => {
      if (result.isConfirmed) {
        callback()
      }
    }).catch(() => {})
  }
}
