import { Component, OnInit } from '@angular/core';
import { ForgetResetPassword } from 'src/app/models/ForgetResetPassword';
import { NgForm } from "@angular/forms";
import { DataService } from 'src/app/shared/data.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-forget-reset-password',
  templateUrl: './forget-reset-password.component.html',
  styleUrls: ['./forget-reset-password.component.css']
})
export class ForgetResetPasswordComponent implements OnInit {

  forgetpassword:ForgetResetPassword = {
    email:null,
    password:null,
    confirmPassword:null
  }
  constructor(private dataService:DataService,private toastr:ToastrService,private route:Router) { }

  ngOnInit(): void {
  }
  updatePassword(){
    //console.log(this.forgetpassword);
    this.dataService.forgetResetPassword(this.forgetpassword).subscribe(
      () => {
        this.toastr.success("Password Updated Successfully!!");
        this.route.navigate(['/login']);
      },
      (err) => {
        console.log(err);
      }
    );
  }
}
