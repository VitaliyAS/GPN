import { TemplateRef, ViewChild } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { Field } from './field';
import { FieldService } from './field.service';
import { Observable } from 'rxjs';
import { CompanyService } from '../company/company.service';
import { Company } from '../company/company';

@Component({
  selector: 'app-field',
  templateUrl: './field.component.html',
  providers: [FieldService, CompanyService]
})

export class FieldComponent {

  //типы шаблонов
  @ViewChild('readOnlyTemplate', { }) readOnlyTemplate: TemplateRef<any>;
  @ViewChild('editTemplate', { }) editTemplate: TemplateRef<any>;

  editedField: Field;
  fields: Array<Field>;
  companies: Array<Company>;
  isNewRecord: boolean;
  statusMessage: string;

  constructor(private fieldService: FieldService, private companyService: CompanyService) {
    this.fields = new Array<Field>();
    this.companies = new Array<Company>();
  }

  ngOnInit() {
    this.getAll();
  }

  //загрузка
  private getAll() {
    this.fieldService.getAll().subscribe((data: Field[]) => {
      this.fields = data;
    });
  }
  private getAllChilds() {
    this.companyService.getAll().subscribe((data: Company[]) => {
      this.companies = data;
    });
  }
  // добавление
  add() {
    this.getAllChilds();
    this.editedField = new Field(0, "", 0, 0);
    this.fields.push(this.editedField);
    this.isNewRecord = true;
  }
  // редактирование
  edit(field: Field) {
    this.getAllChilds();
    this.editedField = new Field(field.id, field.name, field.reserve, field.companyId);
  }
  // загружаем один из двух шаблонов
  loadTemplate(field: Field) {
    if (this.editedField && this.editedField.id == field.id) {
      return this.editTemplate;
    } else {
      return this.readOnlyTemplate;
    }
  }
  // сохраняем
  save() {
    if (this.isNewRecord) {
      // добавляем
      this.fieldService.add(this.editedField).subscribe(data => {
        this.statusMessage = 'Данные успешно добавлены';
        this.getAll();
      });
      this.isNewRecord = false;
      this.editedField = null;
    } else {
      // изменяем
      this.fieldService.update(this.editedField).subscribe(data => {
        this.statusMessage = 'Данные успешно изменены';
        this.getAll();
      });
      this.editedField = null;
    }
  }
  // отмена редактирования
  cancel() {
    // если отмена при добавлении, удаляем последнюю запись
    if (this.isNewRecord) {
      this.fields.pop();
      this.isNewRecord = false;
    }
    this.editedField = null;
  }
  // удаление
  delete(field: Field) {
    this.fieldService.delete(field.id).subscribe(data => {
      this.statusMessage = 'Запись успешно удалена';
      this.getAll();
    });
  }
  
}
