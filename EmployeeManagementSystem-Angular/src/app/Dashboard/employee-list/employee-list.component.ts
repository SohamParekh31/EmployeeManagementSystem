import { Component, OnInit } from '@angular/core';
import { Employee } from 'src/app/models/Employee';
import { DataService } from 'src/app/shared/data.service';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css']
})
export class EmployeeListComponent implements OnInit {

  employees:Employee[];
  emp:Employee= {
    id:0,
    name:null,
    surname:null,
    address:null,
    qualification:null,
    contact_Number:null,
    email:null,
    departmentId: 0,
  };
  employssWithSameDept:Employee[];
  constructor(public dataService:DataService) { }

  ngOnInit(): void {
    var empEmail = localStorage.getItem('email');
    this.dataService.getEmployees().subscribe(
      emp => {
        this.employees = emp;
        if(localStorage.getItem('role')=='Employee'){
          this.emp = this.employees.find(e => e.email == empEmail);
          this.employssWithSameDept = emp.filter(e => e.departmentId == this.emp.departmentId);
        }
        else{
          this.employssWithSameDept = emp
        }
      }
    );
  }
  deleteEmployee(employee:Employee){
    if(confirm(`Are you sure you want to delete ${employee.name} Employee?`)){
      this.dataService.deleteEmployee(employee).subscribe(
        ()=>{
          console.log("Employee Deleted");
          window.location.reload();
        }
      )
    }

  }

}
