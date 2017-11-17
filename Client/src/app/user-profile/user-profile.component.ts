import { Component, OnInit } from '@angular/core';
import { LoginService } from "../shared/services/login.service";
import swal from 'sweetalert2';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {
  id:number;
  details: any; //user details
  str = '';//current password entered
  confirmPassword = ''; //for password conformation
  newPassword = '';//to update new password
  userId:number;
  
  constructor(private loginservice: LoginService,private route:ActivatedRoute) { }

  ngOnInit() {
    this.route.params.subscribe((param) => { this.id = +param['id'] });
    var session = sessionStorage.getItem("id");
    this.userId = parseInt(session);//get the information of the user that has logged in
    this.loginservice.getById(this.id) //get the data of the user by the Mastrerid
                     .subscribe(data => { this.details = data;});
  }

  checkPassword() {
    if(this.str=='' || this.confirmPassword=='' || this.newPassword==''){
    swal('',"Please fill all the fields","error");
    }
    else{
    if (this.str == this.details.password) { //if the password entered of the user is correct
      this.updatePassword();
    }
    else {
    //  swal('','Enter Correct Current Password',"error") //if entered password does not matches
    alert("enter correct password");
    }
  }
  }
  //update the password
  updatePassword() { 
    //if confirm password field does not matches the new password
    if (this.confirmPassword != this.newPassword) {
      swal('','Confirm Password does not match with new Password',"error")
    }
     //if confirm password field matches the new password
    else {
      this.details.password=this.newPassword; //update with new password
      this.loginservice.updatePassword(this.id, this.details);//update the user information with the new password
      this.str=''
      this.confirmPassword='';
      this.newPassword='';
    }
  }
  resetOnClose(){
    this.str=''
    this.confirmPassword='';
    this.newPassword='';
  }
}
