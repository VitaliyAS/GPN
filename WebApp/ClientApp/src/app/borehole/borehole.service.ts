import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Borehole } from './borehole';

@Injectable()
export class BoreholeService {
  private url: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.url = baseUrl + "api/Borehole/";
  }

  getAll() {
    return this.http.get(this.url);
  }

  get(id: number) {
    return this.http.get(this.url + '/' + id.toString());
  }

  add(borehole: Borehole) {
    return this.http.post(this.url, borehole);
  }

  update(borehole: Borehole) {
    return this.http.put(this.url, borehole);
  }

  delete(id: number) {
    //const urlParams = new HttpParams().set("id", id.toString());
    return this.http.delete(this.url + '/' + id.toString());
  }
}
