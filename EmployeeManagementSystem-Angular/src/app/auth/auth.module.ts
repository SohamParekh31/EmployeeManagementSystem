import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { FormsModule } from '@angular/forms';
import { ForgetResetPasswordComponent } from './forget-reset-password/forget-reset-password.component';



@NgModule({
  declarations: [
    LoginComponent,
    ForgetResetPasswordComponent,
  ],
  imports: [
    RouterModule.forChild([
      { path: '', component: LoginComponent },
      { path: 'forget-reset-password', component: ForgetResetPasswordComponent},
    ]),
    FormsModule
  ]
})
export class AuthModule { }
