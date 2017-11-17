import { Component, OnInit } from '@angular/core';
import { routerTransition } from './../router.animations';
import { AuthService } from 'angular4-social-login';
import { LoginService } from '../shared/services/login.service';
import { Router, ActivatedRoute } from '@angular/router';
import { ConfigFile } from '../shared/config';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
  animations: [routerTransition()]
})
export class DashboardComponent implements OnInit {
  pushRightClass: string = 'push-right';
  session: string;
  isvalid:string;
  userId:number;

  constructor(private authService: AuthService,private route:ActivatedRoute,private loginservice: LoginService,private router:Router) { }
  
  ngOnInit() {    
    var session = sessionStorage.getItem("id");
    this.userId = parseInt(session);
  }

  //this is used for toggling the side bar
  toggleSidebar() {
    const dom: any = document.querySelector('body');
    dom.classList.toggle(this.pushRightClass);
  }

  onLoggedout():void{
   // this.authService.signOut();
    this.loginservice.logOut(this.userId)
                     .then(data => {this.router.navigate([ConfigFile.DashboardUrls.onLoggedOut]);})
  }
}