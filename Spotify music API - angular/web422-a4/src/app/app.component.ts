/*********************************************************************************
* WEB422 – Assignment 06
* I declare that this assignment is my own work in accordance with Seneca Academic Policy. No part of this
* assignment has been copied manually or electronically from any other source (including web sites) or 
* distributed to other students.
* 
* Name: __Heebin Lee__ Student ID: _130464191   Date: _2021.08.13__
*
* Online Link to Music App: __https://github.com/HeebinLee/HeebinLee.github.io/tree/master/Assignment6
*
* Online Link to User Api: https://ancient-tundra-11136.herokuapp.com/
*
********************************************************************************/

import { Component, OnInit } from '@angular/core';
import { NavigationStart, Router } from '@angular/router';
import { AuthService } from './auth.service';
import { User } from './User';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'web422-a4'
  searchString:string;
  token: User;

  constructor(private router: Router, private auth:AuthService){}

  handleSearch(){
    this.router.navigate(['/search'],{queryParams:{q: this.searchString}})
    this.searchString="";
  }
  ngOnInit(): void {
    this.searchString = "";
    this.router.events.subscribe((event:any) => {
      if (event instanceof NavigationStart) {
        this.token = this.auth.readToken();
      }
    });
  }

  logout(): void {
    localStorage.clear();
    this.router.navigate(['login']);
  }

}
