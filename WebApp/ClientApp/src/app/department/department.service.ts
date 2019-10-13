import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Department } from './department';

@Injectable()
export class DepartmentService {
  private url: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.url = baseUrl + "api/Department/";
  }

  getAll() {
    return this.http.get(this.url);
  }

  get(id: number) {
    return this.http.get(this.url + '/' + id.toString());
  }

  add(department: Department) {
    return this.http.post(this.url, department);
  }

  update(department: Department) {
    return this.http.put(this.url, department);
  }

  delete(id: number) {
    //const urlParams = new HttpParams().set("id", id.toString());
    return this.http.delete(this.url + '/' + id.toString());
  }
}
