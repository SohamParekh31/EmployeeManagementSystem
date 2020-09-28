import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
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
    private router: Router,private dataService:DataService
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
        console.log("Login Successfull");
        //console.log(res.role);

        localStorage.setItem('role',res.role);
        if(localStorage.getItem('role')=='Employee'){
          var emp = this.employee.find(e => e.email === this.login.email);
          localStorage.setItem('email',emp.email);
        }
        this.router.navigate(['/dashboard/empList']);
        localStorage.setItem('token','login_successfull');
      },
      err => {
        console.log("Invalid UserId or Password");
      }
    );
  }

}
