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
      { path: '',redirectTo: '/deptList', pathMatch: 'full'},
      { path: 'empList', component: EmployeeListComponent },
      { path: 'empAdd', component: EmployeeAddComponent },
      { path: 'empEdit/:id', component: EmployeeEditComponent },
      { path: 'deptList', component: DepartmentComponent },
      { path: 'deptAdd', component: DepartmentAddComponent },
      { path: 'deptEdit/:id', component: DepartmentEditComponent },
    ])
  ]
})
export class DashboardModule { }
