import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DataService } from '../shared/data.service';
import * as signalR from '@aspnet/signalr';


@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  notification = new Array();
  constructor(public dataService:DataService,private route:Router,private http:HttpClient) { }

  ngOnInit(): void {
    var token = localStorage.getItem('token');
    var connection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:44318/chatHub',{ accessTokenFactory: () => token })
      .build();
    connection.start().then(function () {
      console.log("Connected!");
      connection.on('departmentAdded',function(message){
        console.log(message);
      });
      connection.on('departmentDelete',function(message){
        console.log(message);
      });
      connection.on('employeeUpdate',function(message){
        console.log(message);
      });
      connection.on('addEmployee',function(message){
        console.log(message);
      });
    });
  }
  logout(){
    localStorage.removeItem('token');
    localStorage.removeItem('email');
    localStorage.removeItem('role');
    window.location.href = '/login';
  }
}
