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
  constructor(private dataService:DataService) { }

  ngOnInit(): void {
    this.dataService.getEmployees().subscribe(
      emp => {
        this.employees = emp,
        console.log(emp);
      }
    );
  }

}
