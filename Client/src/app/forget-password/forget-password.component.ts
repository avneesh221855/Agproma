import { Component, OnInit } from '@angular/core';
import swal from 'sweetalert2';

import { ForgetServiceService } from "../shared/services/forget-service.service";


@Component({
  selector: 'app-forget-password',
  templateUrl: './forget-password.component.html',
  styleUrls: ['./forget-password.component.css']
})
export class ForgetPasswordComponent implements OnInit {

  //local variable used in this component
  forgetData : any;
  email:string;

  constructor( private forget : ForgetServiceService ) { }

  ngOnInit() {
 
  }

  generateEmail()
  {
    //this method will check wheather the user is register with the portal or not
    //if yes then the user will get an email has to Re-register itself through mail
      this.forget.emailto(this.email)
               .then(data => 
                {
                  if(data.json() == true) //if email is correct and register with AgProMa
                  { 
                    swal('E-mail Sent!','Please check your email and verify yourself','success')
                  } 
                  else{ //if email is not register with the AgProMa
                    swal('E-mail Not Register!','Sorry! Email is not Register with us','error')
                  }
                });
  }
}
