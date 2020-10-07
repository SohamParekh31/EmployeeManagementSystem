import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Employee } from 'src/app/models/Employee';
import { Login } from 'src/app/models/login';
import { DataService } from 'src/app/shared/data.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  login: Login = {
    email:null,
    password:null
  };
  employee:Employee[];
  constructor(
    private router: Router,private dataService:DataService,private toastr:ToastrService
  ) { }

  ngOnInit(): void {
    this.dataService.getEmployees().subscribe(
      emp => {
        this.employee = emp;
      }
    );
  }

  loginUser(form: NgForm): void {
    this.login = form.value;
    //console.log(this.login.email);

    //console.log(emp);
    this.dataService.login(this.login).subscribe(
      (res:any) => {
        localStorage.setItem('token',res.token);
        var token = localStorage.getItem('token');
        const payLoad = JSON.parse(window.atob(token.split('.')[1]));
        console.log(payLoad);
        localStorage.setItem('UserID',payLoad['UserID']);
        localStorage.setItem('role',payLoad['role']);
        if(localStorage.getItem('role')=='Employee'){
          var emp = this.employee.find(e => e.email === this.login.email);
          localStorage.setItem('email',emp.email);
        }
        this.router.navigate(['/dashboard/empList']);
      },
      err => {
        if(err.status === 400){
          this.toastr.warning("Invalid UserId or Password");
        }

      }
    );
  }

}
