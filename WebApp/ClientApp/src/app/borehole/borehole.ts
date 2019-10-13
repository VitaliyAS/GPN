import { Company } from '../company/company';
import { Department } from '../department/department';
import { Field } from '../field/field';

export class Borehole {
  public companyEntity: Company;
  public departmentEntity: Department;
  public fieldEntity: Field;

  constructor(
    public id: number,
    public name: string,
    public depth: number,
    public fieldId: number,
    public departmentId: number,
    public companyId: number
  ) {
    this.companyEntity = new Company(0, "", "");
    this.departmentEntity = new Department(0, "", "", 0);
    this.fieldEntity = new Field(0, "", 0, 0);
  }
}
