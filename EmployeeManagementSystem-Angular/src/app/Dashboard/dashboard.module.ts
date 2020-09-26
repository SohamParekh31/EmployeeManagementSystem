import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmployeeListComponent } from './employee-list/employee-list.component';
import { FormsModule } from '@angular/forms';
import { DepartmentComponent } from './department/department.component';
import { DashboardComponent } from './dashboard.component';
import { RouterModule } from '@angular/router';
import { EmployeeEditComponent } from './employee-edit/employee-edit.component';
import { EmployeeAddComponent } from './employee-add/employee-add.component';
import { DepartmentEditComponent } from './department-edit/department-edit.component';
import { DepartmentAddComponent } from './department-add/department-add.component';
import { AuthGuard } from '../auth/auth.guard';



@NgModule({
  declarations: [
    EmployeeListComponent,
    DepartmentComponent,
    DashboardComponent,
    EmployeeEditComponent,
    EmployeeAddComponent,
    DepartmentEditComponent,
    DepartmentAddComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    RouterModule.forChild([
      { path: '',redirectTo: '/deptList', pathMatch: 'full',canActivate:[AuthGuard]},
      { path: 'empList', component: EmployeeListComponent,canActivate:[AuthGuard] },
      { path: 'empAdd', component: EmployeeAddComponent,canActivate:[AuthGuard] },
      { path: 'empEdit/:id', component: EmployeeEditComponent ,canActivate:[AuthGuard]},
      { path: 'deptList', component: DepartmentComponent ,canActivate:[AuthGuard]},
      { path: 'deptAdd', component: DepartmentAddComponent ,canActivate:[AuthGuard]},
      { path: 'deptEdit/:id', component: DepartmentEditComponent ,canActivate:[AuthGuard]},
    ])
  ]
})
export class DashboardModule { }
