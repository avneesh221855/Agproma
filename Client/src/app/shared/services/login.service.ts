//import all the dependencies and packages 
import { Injectable } from '@angular/core'
import { Http, Headers, Response,RequestOptions } from '@angular/http';
import { Login } from '../model/login';
import { Observable } from "rxjs";
import { ProjectMember } from "../model/projectMember";
import { ConfigFile } from "../config";
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/toPromise';
import { IdPassword } from '../model/idpassword';
import { authentication } from "../model/Authentication";
import { Credential } from "../model/credential";
import { SocialUser } from "angular4-social-login";
import { SocialUserLogin } from "../model/socialLogin";
import { Router } from "@angular/router";

@Injectable()
export class LoginService {

  //local variable used in login service
  user: SocialUser;
  errorMsg : any;

  constructor(private http: Http, private router : Router) { }

  token = sessionStorage.getItem("token");
  headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + this.token });
  options = new RequestOptions({ headers: this.headers });
 
 //get all the details of user
 getAll(){
   return this.http
   .get(ConfigFile.LoginServiceUrl.url)
   .map(response=>response.json())
   .catch((error: any) => {
    return Observable.throw(this.router.navigate(['/app-error/']));
  });
 }


//this function is to logout from the system
 logOut(userId:number){
  return this.http.post(ConfigFile.LoginServiceUrl.logouturl+userId,"",this.options)
                  .map((response)=>response.json())
                  .toPromise()
                  .catch(this.handleError);
 }

 
  //get the userid on basis of email
  get(email:string){
    return this.http.get(ConfigFile.LoginServiceUrl.url+"/"+email)
                    .map(data => data)
                    .catch((error: any) => {
                      return Observable.throw(this.router.navigate(['/app-error/']));
                    });;
  }

 

 //check details of a particular user by emailid and password
  check(userData:IdPassword){
   return this.http
              .post(ConfigFile.LoginServiceUrl.checkurl,userData,this.options)
              .map((response)=>response.json())
              .toPromise()
              .catch(this.handleError);

 }

  //get userdata by id for view profile
  getById(id:any){
    return this.http.get(ConfigFile.LoginServiceUrl.detailUrl+id,this.options)
                    .map(data=>data.json())
                    .catch((error: any) => {
                      return Observable.throw(this.router.navigate(['/app-error/']));
                    });;
  }

  //this method is used to get the token
  getToken(auth:Credential)
  {
    let headers = new Headers({ 'Content-Type': 'application/json'});
    let options = new RequestOptions({ headers: this.headers });
    return this.http.post(ConfigFile.LoginServiceUrl.getToken,auth,options)
                      .toPromise();
  }

  //to get the token for facebook and gmail from API
  getTokenForFbandGoogle(user : SocialUser)
  {
    let headers = new Headers({ 'Content-Type': 'application/json'});
    let options = new RequestOptions({ headers: this.headers });
    return this.http.post(ConfigFile.LoginServiceUrl.getTokenForFbandGoogle+user.email,options)
                      .toPromise()
                      .catch(
                        error=>{
                          this.errorMsg = error; this.router.navigate(['/app-error/'])
                        }
                      );

  }

  //post the details of a new user 
    postLoginDetails(logindetails:Login){
      return this.http
                  .post(ConfigFile.LoginServiceUrl.url,logindetails,this.options)
                  .toPromise()
                  .catch(this.handleError);
    
    }

    //post the details of member with team id
    postMemberDetails(memberdetails:ProjectMember){
      return this.http.post(ConfigFile.LoginServiceUrl.memberUrl,memberdetails,this.options).toPromise().catch(this.handleError);
    }
    

    //handling the error
    private handleError(error: any): Promise<any> {
 
      return Promise.reject(this.router.navigate(['/app-error/']));
    }
    //this is to get the existing member in the project
    getUserData(projectId:number){
      return this.http.get(ConfigFile.LoginServiceUrl.invite_url+projectId)
                      .map(Response=>Response)
                      .catch((error: any) => {
                        return Observable.throw(this.router.navigate(['/app-error/']));
                      });
    
    }

    
    //update details of a user
    updatePassword(id:number,user:any) {
      this.http.put(ConfigFile.LoginServiceUrl.updatePasswordUrl+id,user,{headers:this.headers})
              .subscribe();
    }
  }
