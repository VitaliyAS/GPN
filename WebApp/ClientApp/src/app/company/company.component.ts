import { TemplateRef, ViewChild } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { Company } from './company';
import { CompanyService } from './company.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-company',
  templateUrl: './company.component.html',
  providers: [CompanyService]
})

export class CompanyComponent {

  //типы шаблонов
  @ViewChild('readOnlyTemplate', { }) readOnlyTemplate: TemplateRef<any>;
  @ViewChild('editTemplate', { }) editTemplate: TemplateRef<any>;

  editedCompany: Company;
  companies: Array<Company>;
  isNewRecord: boolean;
  statusMessage: string;

  constructor(private companyService: CompanyService) {
    this.companies = new Array<Company>();
  }

  ngOnInit() {
    this.getAll();
  }

  //загрузка
  private getAll() {
    this.companyService.getAll().subscribe((data: Company[]) => {
      this.companies = data;
    });
  }
  // добавление
  add() {
    this.editedCompany = new Company(0, "", "");
    this.companies.push(this.editedCompany);
    this.isNewRecord = true;
  }
  // редактирование
  editCompany(company: Company) {
    this.editedCompany = new Company(company.id, company.name, company.boss);
  }
  // загружаем один из двух шаблонов
  loadTemplate(company: Company) {
    if (this.editedCompany && this.editedCompany.id == company.id) {
      return this.editTemplate;
    } else {
      return this.readOnlyTemplate;
    }
  }
  // сохраняем
  save() {
    if (this.isNewRecord) {
      // добавляем
      this.companyService.create(this.editedCompany).subscribe(data => {
        this.statusMessage = 'Данные успешно добавлены';
        this.getAll();
      });
      this.isNewRecord = false;
      this.editedCompany = null;
    } else {
      // изменяем
      this.companyService.update(this.editedCompany).subscribe(data => {
        this.statusMessage = 'Данные успешно обновлены';
        this.getAll();
      });
      this.editedCompany = null;
    }
  }
  // отмена редактирования
  cancel() {
    // если отмена при добавлении, удаляем последнюю запись
    if (this.isNewRecord) {
      this.companies.pop();
      this.isNewRecord = false;
    }
    this.editedCompany = null;
  }
  // удаление пользователя
  delete(company: Company) {
    this.companyService.delete(company.id).subscribe(data => {
      this.statusMessage = 'Данные успешно удалены';
      this.getAll();
    });
  }
  
}
