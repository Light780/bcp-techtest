import { Injectable } from '@angular/core'
import { delay, ReplaySubject } from 'rxjs'

@Injectable()
export class LoadingService {
  private readonly _loading = new ReplaySubject<boolean>(1)
  public readonly loading$ = this._loading.asObservable().pipe(delay(1))

  show (): void {
    this._loading.next(true)
  }

  hide (): void {
    this._loading.next(false)
  }
}
