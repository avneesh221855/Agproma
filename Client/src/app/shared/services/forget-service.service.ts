import { Injectable } from '@angular/core';
import { Headers, Http, RequestOptions } from '@angular/http';
import { ConfigFile } from "../config";
import { Router } from "@angular/router";

@Injectable()
export class ForgetServiceService {

  errorMsg : any ;

  constructor(private http:Http, private router : Router) { }

  emailto(emailId : string) : Promise<any>
  {
  	//this method will get a string(email) and call api with that parameter
    let headers=new Headers({ 'Content-Type': 'application/json' });
    let options=new RequestOptions({headers:headers});
    return this.http.post(ConfigFile.ForgetServiceUrl.forgeturl+emailId,null,options)
    .toPromise().catch(
      error=>{
        this.errorMsg = error; this.router.navigate(['/app-error/'])
      }
    );
  }
}
