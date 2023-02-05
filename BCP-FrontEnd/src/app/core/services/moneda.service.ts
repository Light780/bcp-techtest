import { Injectable } from '@angular/core'
import { Observable } from 'rxjs'
import { MonedaDTO } from '../models'
import { ApiService } from './'

@Injectable()
export class MonedaService {
  constructor (
    private readonly apiService: ApiService
  ) {
  }

  getAll (): Observable<MonedaDTO[]> {
    return this.apiService.get<MonedaDTO[]>('moneda')
  }
}
