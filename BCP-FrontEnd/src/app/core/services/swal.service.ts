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
    void Swal.fire({
      title: 'Ocurrio un error',
      text: httpError.error.message,
      icon: 'error'
    })

    return throwError(() => httpError.message)
  }

  public showMessage (response: Response<any>): void {
    if (response.message === null) { return }

    void Swal.fire({
      title: 'Operacion exitosa',
      text: response.message,
      icon: 'success'
    })
  }
}
