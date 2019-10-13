import { TemplateRef, ViewChild } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { Department } from './department';
import { DepartmentService } from './department.service';
import { Observable } from 'rxjs';
import { CompanyService } from '../company/company.service';
import { Company } from '../company/company';

@Component({
  selector: 'app-department',
  templateUrl: './department.component.html',
  providers: [DepartmentService, CompanyService]
})

export class DepartmentComponent {

  //типы шаблонов
  @ViewChild('readOnlyTemplate', { }) readOnlyTemplate: TemplateRef<any>;
  @ViewChild('editTemplate', { }) editTemplate: TemplateRef<any>;

  editedDepartment: Department;
  departments: Array<Department>;
  companies: Array<Company>;
  isNewRecord: boolean;
  statusMessage: string;

  constructor(private departmentService: DepartmentService, private companyService: CompanyService) {
    this.departments = new Array<Department>();
    this.companies = new Array<Company>();
  }

  ngOnInit() {
    this.getAll();
  }

  //загрузка
  private getAll() {
    this.departmentService.getAll().subscribe((data: Department[]) => {
      this.departments = data;
    });
  }
  private getChilds() {
    this.companyService.getAll().subscribe((data: Company[]) => {
      this.companies = data;
    });
  }
  // добавление
  add() {
    this.getChilds();
    this.editedDepartment = new Department(0, "", "", 0);
    this.departments.push(this.editedDepartment);
    this.isNewRecord = true;
  }
  // редактирование
  edit(department: Department) {
    this.getChilds();
    this.editedDepartment = new Department(department.id, department.name, department.boss, department.companyId);
  }
  // загружаем один из двух шаблонов
  loadTemplate(department: Department) {
    if (this.editedDepartment && this.editedDepartment.id == department.id) {
      return this.editTemplate;
    } else {
      return this.readOnlyTemplate;
    }
  }
  // сохраняем
  save() {
    if (this.isNewRecord) {
      // добавляем
      this.departmentService.add(this.editedDepartment).subscribe(data => {
        this.statusMessage = 'Данные успешно добавлены';
        this.getAll();
      });
      this.isNewRecord = false;
      this.editedDepartment = null;
    } else {
      // изменяем
      this.departmentService.update(this.editedDepartment).subscribe(data => {
        this.statusMessage = 'Данные успешно изменены';
        this.getAll();
      });
      this.editedDepartment = null;
    }
  }
  // отмена редактирования
  cancel() {
    // если отмена при добавлении, удаляем последнюю запись
    if (this.isNewRecord) {
      this.departments.pop();
      this.isNewRecord = false;
    }
    this.editedDepartment = null;
  }
  // удаление
  delete(department: Department) {
    this.departmentService.delete(department.id).subscribe(data => {
      this.statusMessage = 'Запись успешно удалена';
      this.getAll();
    });
  }
  
}
