import { Injectable } from '@angular/core';
import { Headers, Http, RequestOptions,Response } from '@angular/http';
import { ConfigFile } from "../config";

import 'rxjs/add/operator/map';

@Injectable()
export class KanbanService {

  constructor(private http : Http) { }

  //header
  token= sessionStorage.getItem("token");
  headers = new Headers({'Content-Type':'application/json','Authorization':'Bearer '+this.token});
  options = new RequestOptions({ headers: this.headers});


  getProjectDetails(projectId:number){

    //this method is to bring all the details related to the particular project
    return this.http.get(ConfigFile.KanBanUrls.getProjectData+projectId)
                    .map(Response=>Response.json());
  }


  getSprintDetails(projectId:number){
  //this method is to fetch the details of sprints
    return this.http.get(ConfigFile.KanBanUrls.GetSprintDetails+projectId)
                    .map(Response => Response.json());
  }

  getVelocityDetails(projectId:number){
    //this method is to fetch the details of sprints velocity
        return this.http.get(ConfigFile.KanBanUrls.GetVelocityDetails+projectId)
                        .map(Response => Response.json());
      }

}
