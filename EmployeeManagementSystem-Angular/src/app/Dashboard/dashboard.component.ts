import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DataService } from '../shared/data.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  constructor(public dataService:DataService,private route:Router,private http:HttpClient) { }

  ngOnInit(): void {
  }
  logout(){
    localStorage.removeItem('token');
    localStorage.removeItem('role');
    window.location.href = '/login';

  }
}
