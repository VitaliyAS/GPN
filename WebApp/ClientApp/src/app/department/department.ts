import { Company } from '../company/company';

export class Department {
  public companyEntity: Company;

  constructor(
    public id: number,
    public name: string,
    public boss: string,
    public companyId: number
  ) { this.companyEntity = new Company(0, "", ""); }
}
