import { Injectable } from '@angular/core'
import { environment } from '../../../environments/enviroment'
import { HttpClient, HttpParams } from '@angular/common/http'
import { catchError, map, Observable } from 'rxjs'
import { Response } from '../models'
import { SwalService } from './swal.service'

@Injectable()
export class ApiService {
  constructor (
    private readonly http: HttpClient,
    private readonly swal: SwalService
  ) {
  }

  get<T>(path: string, params: HttpParams = new HttpParams()): Observable<T> {
    return this.http
      .get<Response<T>>(`${environment.api_url}${path}`, { params })
      .pipe(catchError(this.swal.showErrors))
      .pipe(map(response => {
        this.swal.showMessage(response)
        return response.data
      }))
  }

  put<T>(path: string, body: Object = {}): Observable<T> {
    return this.http.put<Response<T>>(
      `${environment.api_url}${path}`,
      body)
      .pipe(catchError(this.swal.showErrors))
      .pipe(map(response => {
        this.swal.showMessage(response)
        return response.data
      }))
  }

  post<T>(path: string, body: Object = {}): Observable<T> {
    return this.http.post<Response<T>>(
      `${environment.api_url}${path}`,
      body)
      .pipe(catchError(this.swal.showErrors))
      .pipe(map(response => {
        this.swal.showMessage(response)
        return response.data
      }))
  }

  delete (path: string): Observable<string> {
    return this.http.delete<Response<string>>(`${environment.api_url}${path}`)
      .pipe(catchError(this.swal.showErrors))
      .pipe(map(response => {
        this.swal.showMessage(response)
        return response.data
      }))
  }
}
