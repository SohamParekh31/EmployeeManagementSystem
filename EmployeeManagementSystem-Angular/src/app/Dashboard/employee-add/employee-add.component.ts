import { Component, OnInit } from '@angular/core';
import { NgForm } from "@angular/forms";
import { Employee } from 'src/app/models/Employee';

@Component({
  selector: 'app-employee-add',
  templateUrl: './employee-add.component.html',
  styleUrls: ['./employee-add.component.css']
})
export class EmployeeAddComponent implements OnInit {
  employee:Employee = {
    id:1,
    name:null,
    surname:null,
    address:null,
    qualification:null,
    contact_Number:null,
    departmentId: 1,
    email:null,
  };
  constructor() { }

  ngOnInit(): void {
  }
  saveEmployee(){
    console.log(this.employee);
  }
}
