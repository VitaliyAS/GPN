import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
//import { Company } from './company';

@Injectable()
export class BaseService<T> {
  private url: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string, path: string ) {
    this.url = baseUrl + "api/" + path;
  }

  getAll() {
    return this.http.get(this.url);
  }

  get(id: number) {
    return this.http.get(this.url + '/' + id.toString());
  }

  create(t: T) {
    return this.http.post(this.url, t);
  }

  update(t: T) {
    return this.http.put(this.url, t);
  }

  deleteCompany(id: number) {
    return this.http.delete(this.url + '/' + id.toString());
  }
}
