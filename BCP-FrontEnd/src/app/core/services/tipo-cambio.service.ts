import { HttpParams } from '@angular/common/http'
import { Injectable } from '@angular/core'
import { Observable } from 'rxjs'
import {
  ConvertTipoCambioRequest,
  ConvertTipoCambioResponse,
  CreateTipoCambioRequest,
  DeleteTipoCambioRequest,
  GetTipoCambioRequest,
  TipoCambioDTO,
  UpdateTipoCambioRequest
} from '../models'
import { ApiService } from './'

@Injectable()
export class TipoCambioService {
  constructor (
    private readonly apiService: ApiService
  ) {
  }

  get (request: GetTipoCambioRequest): Observable<TipoCambioDTO[]> {
    const params = new HttpParams({ fromObject: { ...request } })
    return this.apiService.get<TipoCambioDTO[]>('tipoCambio', params)
  }

  convertAmount (request: ConvertTipoCambioRequest): Observable<ConvertTipoCambioResponse> {
    const params = new HttpParams({ fromObject: { ...request } })
    return this.apiService.get<ConvertTipoCambioResponse>('tipoCambio/convertAmount', params)
  }

  create (request: CreateTipoCambioRequest): Observable<TipoCambioDTO> {
    return this.apiService.post<TipoCambioDTO>('tipoCambio', request)
  }

  update (request: UpdateTipoCambioRequest): Observable<TipoCambioDTO> {
    return this.apiService.put<TipoCambioDTO>('tipoCambio', request)
  }

  delete (request: DeleteTipoCambioRequest): Observable<string> {
    return this.apiService.delete(`tipoCambio?id=${request.id}`)
  }
}
