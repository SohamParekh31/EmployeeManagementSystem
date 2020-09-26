import { Component, OnInit } from '@angular/core';
import { Department } from 'src/app/models/Department';
import { Employee } from 'src/app/models/Employee';
import { DataService } from 'src/app/shared/data.service';
import { NgForm } from "@angular/forms";
import { Router } from '@angular/router';

@Component({
  selector: 'app-employee-add',
  templateUrl: './employee-add.component.html',
  styleUrls: ['./employee-add.component.css']
})
export class EmployeeAddComponent implements OnInit {
  department:Department[];
  employee:Employee = {
    id:0,
    name:null,
    surname:null,
    address:null,
    qualification:null,
    contact_Number:null,
    departmentId: 0,
  };
  constructor(private dataService:DataService,private route:Router) { }

  ngOnInit(): void {
    this.dataService.getDepartments().subscribe(
      depart => {
        this.department = depart;
      }
    );
  }
  saveEmployee(){
    this.dataService.addEmployee(this.employee).subscribe(
      ()=>{
        console.log(`${this.employee.name} Employee Added`);
        this.route.navigate(['/dashboard/empList']);
      }
    );
  }
}
