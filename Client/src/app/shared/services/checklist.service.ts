//import all dependency
import { Injectable } from '@angular/core';
import { Http, Headers, Response } from '@angular/http';
import { Checklist } from '../model/checklist';
import { ConfigFile } from "../config";
import 'rxjs/add/operator/map'
import 'rxjs/add/operator/catch'
import 'rxjs/add/observable/throw';
import { Observable } from 'rxjs'
import { Router } from "@angular/router";

@Injectable()
export class ChecklistService {
  //intialize class with Http service
  constructor(private http: Http,private router:Router) { }

 
  private headers = new Headers({ 'Content-Type': 'application/json' });
  //method used to add checklist
  addCheckList(checklist: Checklist) {
    return this.http.post(ConfigFile.ChecklistServiceUrl.checkListUrl, checklist, { headers: this.headers })
                    .catch((error: any) => {
    return Observable.throw(this.router.navigate(['/app-error/']));
    }
  );

  }

  
//get the task object for particular Id
getById(id){
  return this.http.get(ConfigFile.ChecklistServiceUrl.getTaskUrl+id).map((res:Response)=>res.json())
                  .catch((error: any) => {
    return Observable.throw(this.router.navigate(['/app-error/']));
  });;
 }

  //method used to get checklist by task id
  getCheckList(id) {
    return this.http.get(ConfigFile.ChecklistServiceUrl.checkListUrl + id)
      .map(Response => Response.json())
      .catch((error: any) => {
        return Observable.throw(this.router.navigate(['/app-error/']));
      });;
  }

  //method used to update status of checklist
  completionStatus(id, checklist) {
    return this.http
      .put(ConfigFile.ChecklistServiceUrl.efficiencyUrl + id, checklist, { headers: this.headers })
      .map((Response) => Response)
      .catch((error: any) => {
        return Observable.throw(this.router.navigate(['/app-error/']));
      });
  }
  //method used to delete checklist
  deleteChecklists(checklistId) {
    return this.http
      .delete(ConfigFile.ChecklistServiceUrl.checkListUrl + checklistId, { headers: this.headers })
      .map((Response) => Response)
      .catch((error: any) => {
        return Observable.throw(this.router.navigate(['/app-error/']));
      });

  }
  updateDailyStatusofTask(dailyStatus:Checklist){
    console.log("hellooooooooo",dailyStatus);
    return this.http.put(ConfigFile.ChecklistServiceUrl.updateDailyStatus+dailyStatus.checklistId,dailyStatus,{headers : this.headers}).toPromise().
    catch((error: any) => {
      if(error==400){
      return Observable.throw(this.router.navigate(['/app-error/']));
     } });
  }

}
