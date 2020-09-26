import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  login: any;
  constructor(
    private router: Router
  ) { }

  ngOnInit(): void {
  }

  loginUser(form: NgForm): void {
    this.login = form.value;
    localStorage.setItem('token','admin');
    if(localStorage.getItem('token')!=null){
      this.router.navigate(['/dashboard/empList']);
    }
  }

}
