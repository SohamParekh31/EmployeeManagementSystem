import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Department } from 'src/app/models/Department';
import { DataService } from 'src/app/shared/data.service';

@Component({
  selector: 'app-department-edit',
  templateUrl: './department-edit.component.html',
  styleUrls: ['./department-edit.component.css']
})
export class DepartmentEditComponent implements OnInit {

  department:Department = {
    departmentId: 0,
    name: null,
  };
  ID:number;
  constructor(private dataService:DataService,private activateRoute:ActivatedRoute,private route:Router) { }

  ngOnInit(): void {
    this.activateRoute.paramMap.subscribe(
      params => {
        this.ID = +params.get('id');
      this.getDepartmentById(this.ID);
      }
    );
  }
  getDepartmentById(id){
    this.dataService.getDepartmentById(id).subscribe(
      dept => {
        this.department = dept;
      }
    );
  }
  updateDepartment(){
    this.dataService.editDepartment(this.department).subscribe(
      ()=>{
        console.log("Department Updates Successfully!!");
        this.route.navigate(['/dashboard/deptList']);
      }
    );

  }
}
