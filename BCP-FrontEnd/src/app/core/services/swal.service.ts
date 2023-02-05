import {Injectable} from '@angular/core';
import {Response} from "../models";
import {Observable, throwError} from "rxjs";
import Swal from "sweetalert2";
import {HttpErrorResponse} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class SwalService {

  constructor() {
  }

  public showErrors(httpError: HttpErrorResponse): Observable<never> {
    Swal.fire({
      title: 'Ocurrio un error',
      text: httpError.error.message,
      icon: 'error'
    }).then().catch(() => {
    })

    return throwError(() => httpError.message)
  }

  public showMessage(response: Response<any>): void {
    if (response.message === null)
      return

    Swal.fire({
      title: 'Operacion exitosa',
      text: response.message,
      icon: 'success'
    }).then().catch(() => {
    })
  }
}
