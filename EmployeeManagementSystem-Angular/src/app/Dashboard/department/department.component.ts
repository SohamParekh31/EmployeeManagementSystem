import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Department } from 'src/app/models/Department';
import { DataService } from 'src/app/shared/data.service';

@Component({
  selector: 'app-department',
  templateUrl: './department.component.html',
  styleUrls: ['./department.component.css']
})
export class DepartmentComponent implements OnInit {
  p:number = 1;
  departments:Department[];
  constructor(public dataService:DataService,private toastr:ToastrService) {
    this.dataService.getDepartments().subscribe(
      depart => {
        this.departments = depart;
      }
    );
  }

  ngOnInit(): void {

  }
  deleteDepartment(department:Department){
    if(confirm(`Are you sure you want to delete ${department.name} Department?`)){
      this.dataService.deleteDepartment(department).subscribe(
        ()=>{
          this.toastr.success(`${department.name} Department Deleted`);
          //window.location.reload();
        }
      )
    }

  }

}
