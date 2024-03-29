import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UserModel } from './_models/usermodel';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {

  title = 'Smart Base Accounting';
  users: any;

  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
    //this.getUsers();
    this.setCurrentUser();
  }

  setCurrentUser() {
    const user: UserModel = JSON.parse(localStorage.getItem('user'));
    this.accountService.setCurrentUser(user);
  }

  // getUsers() {
  //   this.http.get<ServiceResponseModel>('http://localhost:9000/api/v1/User/GetAll').subscribe(response => {
  //     this.users = response.data;
  //   }, error => {
  //     console.log(error);
  //   })
  // }

}