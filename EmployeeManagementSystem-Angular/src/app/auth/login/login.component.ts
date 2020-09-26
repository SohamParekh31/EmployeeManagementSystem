import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { DataService } from 'src/app/shared/data.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  login: any;
  constructor(
    private router: Router,private dataService:DataService
  ) { }

  ngOnInit(): void {
  }

  loginUser(form: NgForm): void {
    this.login = form.value;
    this.dataService.login(this.login).subscribe(
      () => {
        console.log("Login Successfull");
        this.router.navigate(['/dashboard/empList']);
        localStorage.setItem('token','login_successfull');
      },
      err => {
        console.log("Invalid UserId or Password");
      }
    );
    // localStorage.setItem('token','admin');
    // if(localStorage.getItem('token')!=null){
    //   this.router.navigate(['/dashboard/empList']);
    // }
  }

}
