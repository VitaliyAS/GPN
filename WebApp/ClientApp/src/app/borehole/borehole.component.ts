import { TemplateRef, ViewChild } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Borehole } from './borehole'
import { BoreholeService } from './borehole.service'
import { Company } from '../company/company';
import { CompanyService } from '../company/company.service';
import { Department } from '../department/department';
import { DepartmentService } from '../department/department.service';
import { Field } from '../field/field';
import { FieldService } from '../field/field.service';

@Component({
  selector: 'app-borehole',
  templateUrl: './borehole.component.html',
  providers: [BoreholeService, FieldService, DepartmentService, CompanyService]
})

export class BoreholeComponent {

  //типы шаблонов
  @ViewChild('readOnlyTemplate', { }) readOnlyTemplate: TemplateRef<any>;
  @ViewChild('editTemplate', { }) editTemplate: TemplateRef<any>;

  editedBorehole: Borehole;
  boreholes: Array<Borehole>;
  fields: Array<Field>;
  departments: Array<Department>;
  companies: Array<Company>;
  isNewRecord: boolean;
  statusMessage: string;

  constructor(
    private boreholeService: BoreholeService,
    private fieldService: FieldService,
    private departmentService: DepartmentService,
    private companyService: CompanyService
  ) {
    this.boreholes = new Array<Borehole>();
    this.fields = new Array<Field>();
    this.departments = new Array<Department>();
    this.companies = new Array<Company>();
  }

  ngOnInit() {
    this.getAll();
  }

  //загрузка
  private getAll() {
    this.boreholeService.getAll().subscribe((data: Borehole[]) => {
      this.boreholes = data;
    });
  }
  private getAllChilds() {
    this.fieldService.getAll().subscribe((data: Field[]) => {
      this.fields = data;
    });
    this.departmentService.getAll().subscribe((data: Department[]) => {
      this.departments = data;
    });
    this.companyService.getAll().subscribe((data: Company[]) => {
      this.companies = data;
    });
  }
  // добавление
  add() {
    this.getAllChilds();
    this.editedBorehole = new Borehole(0, "", 0, 0, 0, 0);
    this.boreholes.push(this.editedBorehole);
    this.isNewRecord = true;
  }
  // редактирование
  edit(borehole: Borehole) {
    this.editedBorehole = new Borehole(borehole.id, borehole.name, borehole.depth, borehole.fieldId, borehole.departmentId, borehole.companyId);
    this.getAllChilds();
  }
  // загружаем один из двух шаблонов
  loadTemplate(borehole: Borehole) {
    if (this.editedBorehole && this.editedBorehole.id == borehole.id) {
      return this.editTemplate;
    } else {
      return this.readOnlyTemplate;
    }
  }
  // сохраняем
  save() {
    if (this.isNewRecord) {
      // добавляем
      this.boreholeService.add(this.editedBorehole).subscribe(data => {
        this.statusMessage = 'Данные успешно добавлены';
        this.getAll();
      });
      this.isNewRecord = false;
      this.editedBorehole = null;
    } else {
      // изменяем
      this.boreholeService.update(this.editedBorehole).subscribe(data => {
        this.statusMessage = 'Данные успешно изменены';
        this.getAll();
      });
      this.editedBorehole = null;
    }
  }
  // отмена редактирования
  cancel() {
    // если отмена при добавлении, удаляем последнюю запись
    if (this.isNewRecord) {
      this.departments.pop();
      this.isNewRecord = false;
    }
    this.editedBorehole = null;
  }
  // удаление
  delete(borehole: Borehole) {
    this.boreholeService.delete(borehole.id).subscribe(data => {
      this.statusMessage = 'Запись успешно удалена';
      this.getAll();
    });
  }
  
}
