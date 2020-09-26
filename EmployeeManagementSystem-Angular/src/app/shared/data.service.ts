import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Department } from '../models/Department';
import { Observable, throwError, of } from 'rxjs';
import { map, tap, catchError } from 'rxjs/operators';
import { Employee } from '../models/Employee';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  url = 'https://localhost:44318';
  constructor(private http:HttpClient) { }

  getDepartments(): Observable<Department[]> {
    return this.http.get<Department[]>(this.url+'/Departments')
      .pipe(
        catchError(err => this.handleError(err))
      );
  }
  getDepartmentById(id:number):Observable<Department>{
    return this.http.get<Department>(this.url+`/Departments/${id}`);
  }
  addDepartment(department:Department): Observable<Department>{
    return this.http.post<Department>(this.url+'/Departments',department,{
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    });
  }
  editDepartment(department:Department): Observable<Department>{
    return this.http.put<Department>(this.url+`/Departments/${department.departmentId}`,department,{
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    });
  }
  deleteDepartment(department:Department):Observable<Department>{
    return this.http.delete<Department>(this.url+`/Departments/${department.departmentId}`,{
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    });
  }
  getEmployees(): Observable<Employee[]> {
    return this.http.get<Employee[]>(this.url+'/Employees')
      .pipe(
        catchError(err => this.handleError(err))
      );
  }
  private handleError(err) {
    // in a real world app, we may send the server to some remote logging infrastructure
    // instead of just logging it to the console
    let errorMessage: string;
    if (err.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      errorMessage = `An error occurred: ${err.error.message}`;
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      errorMessage = `Backend returned code ${err.status}: ${err.body.error}`;
    }
    console.error(err);
    return throwError(errorMessage);
  }
}
