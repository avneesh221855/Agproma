import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthService } from 'angular4-social-login';
import { SocialUser } from 'angular4-social-login';
import { GoogleLoginProvider, FacebookLoginProvider } from 'angular4-social-login';
import { Login } from './../shared/model/login';
import swal from 'sweetalert2';

import { LoginService } from '../shared/services/login.service'
import { ProjectMember } from "../shared/model/projectMember";
import { IdPassword } from '../shared/model/idpassword';
import { ConfigFile } from './../shared/config';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  user: SocialUser;
  tokenData: any;
  userId: number;
  details: any[] = [];
  index: Login;
  private model = {             //Binding with html 
    FirstName: '',
    LastName: '',
    Organization: '',
    Department: '',
    Email: '',
    Password: ''
  };
  ConfirmPassword: string;
  projectmember: ProjectMember = {               //object type of Projectmember
    ProjectId: null,
    MemberId: null,
    ActAs: null
  }

  //inject sevices on which this component depends
  constructor(private authService: AuthService, private router: Router, private loginservice: LoginService, private route: ActivatedRoute) { }

  ngOnInit() {
 
    this.loginservice.getAll().subscribe(details => {         //calling getall function of loginservice 
      this.details = details;                                 //and return details of all users
    })
    this.route.params.subscribe((param) =>
      this.projectmember.ProjectId = +param['id']);               //getting project id from route
  }

  signInWithGoogle(): void {
    //this method is used for social login(gmail)
    this.authService.signIn(GoogleLoginProvider.PROVIDER_ID)
      .then(data => {
        this.authService.authState.subscribe((user) => {
          this.user = user; // data is assigned from gmail to local variable

          if (this.user != null) //user will be redirected to dashboard screen
          {
            //to generate token for user who is logged in with social login
            this.loginservice.getTokenForFbandGoogle(this.user).then(data => {
                          //storing data in session storage
                          this.tokenData = JSON.parse(data["_body"]).token;
                          sessionStorage.setItem("id", this.user["id"].toString());
                          sessionStorage.setItem("token", this.tokenData);
            
          //to get the details of user with the help of email
          this.loginservice.get(this.user.email)
                           .subscribe(data => {this.userId=data.json();

                            
                            if(this.userId!=null)
                            { 
                              //if data is present then redirect to dashboard
                              sessionStorage.setItem("id",this.userId.toString());
                              this.router.navigateByUrl(ConfigFile.RegisterUrls.dashboardNavigation);
                            }
                            else
                            { this.router.navigate([ConfigFile.RegisterUrls.registerNavigationById]); }
                          });
                })          

          }});
      });
  }

  signInWithFB(): void {
    //this method is used for social login(facebook)
    this.authService.signIn(FacebookLoginProvider.PROVIDER_ID)
      .then(data => {
        this.authService.authState.subscribe((user) => {
          this.user = user; // data is assigned from facebook to local variable

          if (this.user != null)
          { 
             //to generate token for user who is logged in with social login
            this.loginservice.getTokenForFbandGoogle(this.user).then(data => {
              this.tokenData = JSON.parse(data["_body"]).token;
              sessionStorage.setItem("id", this.user["id"].toString());
              sessionStorage.setItem("token", this.tokenData);

              this.loginservice.get(this.user.email)
                               .subscribe(data => {this.userId=data.json(); console.log(this.userId)
                               if(this.userId!=null)
                                {
                                  sessionStorage.setItem("id",this.userId.toString());
                                  this.router.navigateByUrl(ConfigFile.RegisterUrls.dashboardNavigation);
                                }
                                else
                                {
                                  this.router.navigate([ConfigFile.RegisterUrls.registerNavigationById]);
                                }
                              })
                            })  
          } 
        })
      });
  }



  CreateAccount() {  //registering the user

    // this.index = this.details.find((m) => m.email == this.model.Email);  //find the details of a particular user 

    if (this.model.Department == '' || this.model.Email == '' || this.model.FirstName == '' || this.model.LastName == '' || this.model.Organization == '' || this.model.Password == '') {
      swal('', 'Enter the Required fields', 'error');             //if any entry is empty then show the alert 
    }
    else if (!this.model.Email.includes("@" && ".")) {
      swal('', 'Enter valid email-address', 'error');               //if emailid does not contains @ and . then show the alert
    }
    else if (this.model.Password != this.ConfirmPassword) {
      swal('', 'confirm password does not match the password', 'error')    //if confirm and password is not equal then show password does not match
    }

    else if (this.model.Password == this.ConfirmPassword) {
      if (this.projectmember.ProjectId) {
        this.projectmember.ActAs = 1;
        this.loginservice.postLoginDetails(this.model)
          .then(data => {
            let response = data.json();
            if (response == "success") {
              this.loginservice.get(this.model.Email)
                  .subscribe(detail => {  //posting the details of user 
                  this.projectmember.MemberId = detail.json();  //then calling the get method to find the user unique id 
                  this.loginservice.postMemberDetails(this.projectmember) //then posting the user id and team id
                  swal('', 'Your account has been created', 'success');
                  this.router.navigateByUrl(ConfigFile.RegisterUrls.signupNavigationById);     //navigate to the signup page
                }
                )
            }
            else {
              swal('', 'Email Already Exists', 'error')    //if enter id matches with the existing id in database 
            }
          }
          );
      }
      else {
        this.loginservice.postLoginDetails(this.model)     //calling post method to register the details  
          .then(data => {
            if (data.json() == "success") {
              swal('', 'Your account has been created', 'success');
              this.router.navigateByUrl(ConfigFile.RegisterUrls.signupNavigationById);      //navigate to the signup page
            }
            else {
              swal('', 'Email Already Exists', 'error')      //if enter id matches with the existing id in database 
            }
          })
      }
    }
  }

}

