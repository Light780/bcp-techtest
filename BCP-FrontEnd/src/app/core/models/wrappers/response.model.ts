export interface Response<T> {
  succeeded: boolean
  message?: string
  errors?: string[]
  data: T
}
