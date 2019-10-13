import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Field } from './field';

@Injectable()
export class FieldService {
  private url: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.url = baseUrl + "api/Field/";
  }

  getAll() {
    return this.http.get(this.url);
  }

  get(id: number) {
    return this.http.get(this.url + '/' + id.toString());
  }

  add(field: Field) {
    return this.http.post(this.url, field);
  }

  update(field: Field) {
    return this.http.put(this.url, field);
  }

  delete(id: number) {
    return this.http.delete(this.url + '/' + id.toString());
  }
}
