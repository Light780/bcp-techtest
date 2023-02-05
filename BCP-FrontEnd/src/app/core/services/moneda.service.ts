import {Injectable} from '@angular/core'
import {Observable} from 'rxjs'
import {GetMonedaResponse} from '../models'
import {ApiService} from './'

@Injectable()
export class MonedaService {
  constructor(
    private readonly apiService: ApiService
  ) {
  }

  getAll(): Observable<GetMonedaResponse[]> {
    return this.apiService.get<GetMonedaResponse[]>('moneda')
  }
}
