import {HttpParams} from '@angular/common/http'
import {Injectable} from '@angular/core'
import {Observable} from 'rxjs'
import {
  ConvertTipoCambioRequest,
  ConvertTipoCambioResponse,
  CreateTipoCambioRequest,
  DeleteTipoCambioRequest,
  GetTipoCambioRequest,
  GetTipoCambioResponse,
  UpdateTipoCambioRequest
} from '../models'
import {ApiService} from './'

@Injectable()
export class TipoCambioService {
  constructor(
    private readonly apiService: ApiService
  ) {
  }

  get(request: GetTipoCambioRequest): Observable<GetTipoCambioResponse[]> {
    const params = new HttpParams({fromObject: {...request}})
    return this.apiService.get<GetTipoCambioResponse[]>('tipoCambio', params)
  }

  convertAmount(request: ConvertTipoCambioRequest): Observable<ConvertTipoCambioResponse> {
    const params = new HttpParams({fromObject: {...request}})
    return this.apiService.get<ConvertTipoCambioResponse>('tipoCambio/convertAmount', params)
  }

  create(request: CreateTipoCambioRequest): Observable<GetTipoCambioResponse> {
    return this.apiService.post<GetTipoCambioResponse>('tipoCambio', request)
  }

  update(request: UpdateTipoCambioRequest): Observable<GetTipoCambioResponse> {
    return this.apiService.put<GetTipoCambioResponse>('tipoCambio', request)
  }

  delete(request: DeleteTipoCambioRequest): Observable<string> {
    return this.apiService.delete(`tipoCambio?id=${request.id}`)
  }
}
