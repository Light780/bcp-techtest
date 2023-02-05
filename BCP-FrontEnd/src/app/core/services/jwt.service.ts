import { Injectable } from '@angular/core'

@Injectable()
export class JwtService {
  getToken (): string | undefined {
    return window.localStorage['jwtToken']
  }

  setToken (token: string): void {
    window.localStorage['jwtToken'] = token
  }

  deleteToken (): void {
    window.localStorage.removeItem('jwtToken')
  }
}
