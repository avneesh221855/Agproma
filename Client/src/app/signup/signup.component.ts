import { Component, OnInit, Input } from '@angular/core';
import { AuthService } from 'angular4-social-login';
import { SocialUser } from 'angular4-social-login';
import { Router, ActivatedRoute } from '@angular/router';
import { GoogleLoginProvider, FacebookLoginProvider } from 'angular4-social-login';
import swal from 'sweetalert2';

import { LoginService } from '../shared/services/login.service'
import { IdPassword } from '../shared/model/idpassword';
import { Credential } from '../shared/model/credential';
import { authentication } from "../shared/model/Authentication";
import { ProjectMember } from "../shared/model/projectMember";
import { SocialUserLogin } from "../shared/model/socialLogin";
import { Login } from "../shared/model/login";

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {

  //local variable used in this component
  userdata:any;
  user: SocialUser;
  email: string = '';
  password: string = '';
  userCred: Credential;
  tokenData: any;
  userId:number = 0;
  projectmember: ProjectMember = {               //object type of Projectmember
    ProjectId: null,
    MemberId: null,
    ActAs: null
  }

  constructor(private authService: AuthService, private router: Router, private loginservice: LoginService,private route:ActivatedRoute) { }

  ngOnInit() {
    //this method will run when the page is loaded
    //this method will check wheather the user is logged in with social account or not then the user can be moved to according screens
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
                              this.router.navigateByUrl('app-dashboard');
                            }
                            else
                            { 
                              //if not then redirect to register page
                              this.router.navigate(["app-register/:id"]); 
                            }
                          });
                })          

          }});
      });
  }

  signInWithFB(): void {

    try{
    //this method is used for social login(facebook)
    this.authService.signIn(FacebookLoginProvider.PROVIDER_ID)
      .then(data => {
        this.authService.authState.subscribe((user) => {
          this.user = user; // data is assigned from facebook to local variable

          if (this.user != null)
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
                                this.router.navigateByUrl('app-dashboard');
                              }
                              else
                                {
                                     //if not then redirect to register page
                                  this.router.navigate(["app-register/:id"]);
                                }
                             })
                            })  
          } 
        })
      });
    }
    catch(error)
    {
      
    }  
    
  }


  signOut(): void {
    //this method is used to signout with AgProMa project
    this.authService.signOut();
  }


  login() {
    let auth: authentication = new authentication("", "");
    //this method is used to verify user's credentials
    if (this.email == '' || this.password == '') //if username or password is empty
    {
      swal('', "Enter the proper details", "error");
    }

    else {

      let model: IdPassword = new IdPassword(this.email, this.password);

      this.loginservice.check(model).then(data => {
        this.userCred = data;
        if (this.userCred["status"] == "success") // if user is register with AgProMa
        {
          //call method for token generation
          this.loginservice.getToken(this.userCred).then(data => {
            this.tokenData = JSON.parse(data["_body"]).token;
            sessionStorage.setItem("id", this.userCred["userId"].toString());
            sessionStorage.setItem("token", this.tokenData);
        
          if (this.tokenData)
          { 
            if(this.projectmember.ProjectId){
              this.projectmember.ActAs=1;
              this.projectmember.MemberId=this.userCred.userId;
              this.loginservice.postMemberDetails(this.projectmember).then(()=>this.loginservice.getUserData(this.projectmember.ProjectId).subscribe(userdata=>this.userdata=userdata))
            .then(()=>this.router.navigate(["app-dashboard"]))
           
          }
           
            else{
            this.router.navigate(["app-dashboard"]);} } //if user's credentials are correct then user will br redirected to dashboard
        });
        }

        else if (this.userCred["status"] == "email") {
          swal('', "Enter Valid Id or Password", "error");
        }
        else {
          //if username or password is incorrect
          swal('', "you are not registered", "error");
        }
      });
    }

  }


}