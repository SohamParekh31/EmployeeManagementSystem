import { Component, OnInit } from '@angular/core';
import { NgForm } from "@angular/forms";
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Department } from 'src/app/models/Department';
import { Employee } from 'src/app/models/Employee';
import { DataService } from 'src/app/shared/data.service';

@Component({
  selector: 'app-employee-edit',
  templateUrl: './employee-edit.component.html',
  styleUrls: ['./employee-edit.component.css']
})
export class EmployeeEditComponent implements OnInit {
  department:Department[];
  employee:Employee = {
    id:0,
    name:null,
    surname:null,
    email:null,
    address:null,
    qualification:null,
    contact_Number:null,
    departmentId: 0,
  };
  ID:number;
  constructor(private dataService:DataService,private activateRoute:ActivatedRoute,private route:Router,
              private toastr:ToastrService) { }

  ngOnInit(): void {
    this.activateRoute.paramMap.subscribe(
      params => {
        this.ID = +params.get('id');
      this.getEmployeeById(this.ID);
      }
    );
    this.dataService.getDepartments().subscribe(
      depart => {
        this.department = depart;
      }
    );
  }

  getEmployeeById(id){
    this.dataService.getEmployeeById(id).subscribe(
      emp => {
        this.employee = emp;
      }
    );
  }

  updateEmployee(){
    this.dataService.editEmployee(this.employee).subscribe(
      () => {
        this.toastr.success(`${this.employee.name} Employee Edit`);
      },
      (err)=>{console.log(err)}
    );
    this.route.navigate(['/dashboard/empList']);
  }
}
