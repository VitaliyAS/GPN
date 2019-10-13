import { Company } from '../company/company';

export class Field {
  public companyEntity: Company;

  constructor(
    public id: number,
    public name: string,
    public reserve: number,
    public companyId: number
  ) { this.companyEntity = new Company(0, "", ""); }
}
