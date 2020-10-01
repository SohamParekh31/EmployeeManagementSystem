import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Department } from 'src/app/models/Department';
import { DataService } from 'src/app/shared/data.service';
import * as signalR from '@aspnet/signalr';

@Component({
  selector: 'app-department-add',
  templateUrl: './department-add.component.html',
  styleUrls: ['./department-add.component.css'],
})
export class DepartmentAddComponent implements OnInit {
  department: Department = {
    departmentId: 0,
    name: null,
  };
  constructor(
    private dataService: DataService,
    private route: Router,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    var connection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:44318/chatHub')
      .build();
    connection.start().then(function () {
      console.log('connected');
      connection.on('departmentAdded',function(message){
        console.log(message);
      });
    });
  }

  saveDepartment() {
    this.dataService.addDepartment(this.department).subscribe(() => {
      this.toastr.success(`${this.department.name} Department Added`);
      this.route.navigate(['/dashboard/deptList']);
    });
  }
}
