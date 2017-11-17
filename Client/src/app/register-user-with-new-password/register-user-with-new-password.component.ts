//import all the dependencies

import { Component, OnInit } from '@angular/core';

import { ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/switchMap';
import { Master } from "./../shared/model/master";
import swal from 'sweetalert2';
import { Router } from '@angular/router';
import { RegisterUserWithNewPasswordService } from "../shared/services/register-user-with-new-password.service";
import { ConfigFile } from './../shared/config';

@Component({
  selector: 'app-register-user-with-new-password',
  templateUrl: './register-user-with-new-password.component.html',
  styleUrls: ['./register-user-with-new-password.component.css']
})
export class RegisterUserWithNewPasswordComponent implements OnInit {
  //inject sevices on which this component depends
  constructor(private registerUser : RegisterUserWithNewPasswordService, private route:ActivatedRoute, private router : Router ) { }

  userId : string;
  userDetail : Master;
  data : Master;
  password:string;
  confirmpassword:string;
  

  ngOnInit() {
    this.route.paramMap                                            //finding the id from the url
    .switchMap((params:ParamMap)=>this.registerUser.getUserDetails(params.get('id')))
    .subscribe(data =>{ this.userDetail = data.json()});      
  }

  resetPassword()
  {
    this.userDetail.password=this.password;                
    if(this.password==this.confirmpassword){
    this.registerUser.updatePassword(this.userDetail);             //updating the password by calling the update
      swal('',"Password updated successfully","success");           //password function ofbRegisterwithnew password service 
      this.router.navigateByUrl(ConfigFile.RegisterUserWithNewPasswordUrls.signupNavigation);                 //navigate to the signup page
  }
  else
    {
      swal('',"Password does not match with confirm password","error");  //showing message if password does not match
    }
  }

}
