import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Company } from './company';

@Injectable()
export class CompanyService {
  private url: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.url = baseUrl + "api/Company/";
  }

  getAll() {
    return this.http.get(this.url);
  }

  get(id: number) {
    return this.http.get(this.url + '/' + id.toString());
  }

  create(company: Company) {
    return this.http.post(this.url, company);
  }

  update(company: Company) {
    return this.http.put(this.url + '/' + company.id.toString(), company);
  }

  delete(id: number) {
    return this.http.delete(this.url + '/' + id.toString());
  }
}
