import { Component, OnInit } from '@angular/core';
import { Department } from 'src/app/models/Department';
import { DataService } from 'src/app/shared/data.service';

@Component({
  selector: 'app-department',
  templateUrl: './department.component.html',
  styleUrls: ['./department.component.css']
})
export class DepartmentComponent implements OnInit {
  departments:Department[];
  constructor(private dataService:DataService) {
    this.dataService.getDepartments().subscribe(
      depart => {
        this.departments = depart,
        console.log(depart);
      }
    );
  }

  ngOnInit(): void {

  }
  deleteDepartment(department:Department){
    if(confirm(`Are you sure you want to delete ${department.name} Department?`)){
      this.dataService.deleteDepartment(department).subscribe(
        ()=>{
          console.log("Department Deleted");
        }
      )
    }
    window.location.reload();
  }

}
