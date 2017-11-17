import { Component, OnInit } from '@angular/core';
import { AuthService } from 'angular4-social-login';
import { ActivatedRoute, Router } from '@angular/router';
import { LoginService } from '../shared/services/login.service';
import { ConfigFile } from '../shared/config';
@Component({
  selector: 'app-dashboard-role',
  templateUrl: './dashboard-role.component.html',
  styleUrls: ['./dashboard-role.component.scss']
})
export class DashboardRoleComponent implements OnInit {
  pushRightClass: string = 'push-right';
  session: string;
  isvalid:string;
  userId:number;
  projectId:number;
  actAs:number;

  constructor(private authService: AuthService,private route:ActivatedRoute,private loginservice: LoginService,private router:Router) { }
  
  ngOnInit() {
    this.route.params.subscribe((param) => this.projectId= +param['id']);   
    var session = sessionStorage.getItem("id");
    this.userId = parseInt(session);
    var temp = sessionStorage.getItem("role");
    this.actAs = parseInt(temp);
  }
 
  //this is used for toggling the side bar
  toggleSidebar() {
    const dom: any = document.querySelector('body');
    dom.classList.toggle(this.pushRightClass);
  }

  onLoggedout():void{
    this.authService.signOut();
    this.loginservice.logOut(this.userId)
                     .then(data => {this.router.navigate([ConfigFile.DashboardRoleUrls.onLoggedOut]);})
  }
}