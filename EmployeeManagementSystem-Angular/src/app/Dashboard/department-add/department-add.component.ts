import { Component, OnInit } from '@angular/core';
import { NgForm } from "@angular/forms";
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Department } from 'src/app/models/Department';
import { DataService } from 'src/app/shared/data.service';

@Component({
  selector: 'app-department-add',
  templateUrl: './department-add.component.html',
  styleUrls: ['./department-add.component.css']
})
export class DepartmentAddComponent implements OnInit {

  department:Department = {
    departmentId: 0,
    name: null,
  };
  constructor(private dataService:DataService,private route:Router,private toastr:ToastrService) { }

  ngOnInit(): void {

  }
  saveDepartment(){
    this.dataService.addDepartment(this.department).subscribe(
      () => {
        this.toastr.success(`${this.department.name} Department Added`);
        this.route.navigate(['/dashboard/deptList']);
      }
      );

  }
}
