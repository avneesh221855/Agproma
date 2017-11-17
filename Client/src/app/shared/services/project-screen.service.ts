import { Injectable } from '@angular/core';
import { Http,Headers,Response} from '@angular/http';
import { ProjectMaster } from './../model/ProjectMaster';
import { ConfigFile } from './../config';

import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/toPromise';

@Injectable()
export class ProjectScreenService {
  constructor(private http:Http) { }
 //this method is used to fire request for all project related to one employee
  getAllProjectOfEmployee(Id:number):Promise<any>{

  return this.http.get(ConfigFile.ProjectMasterUrls.getAllProjectOfEmployee+Id)
                  .toPromise()
                  .then(Response => Response.json());

  }
 //this method is used to fire request to add new project
  addNewProject(projectitem:ProjectMaster):Promise<any>{
   return  this.http.post(ConfigFile.ProjectMasterUrls.addNewProject,projectitem,{headers: new Headers({ 'Content-Type': 'application/json'})})
              .toPromise()
              .then(Response => Response);
    }

 //this is to delete the project
 deleteProject(Id:number):Promise<any>{
   return this.http.delete(ConfigFile.ProjectMasterUrls.deleteProject+Id,{headers: new Headers({ 'Content-Type': 'application/json'})})
                  .toPromise()
                  .then(Response => Response);
                  
  }
//this is to update the project detail in database 
 updateProject(Id:number,projectitem:ProjectMaster):Promise<any>{
   return this.http.put(ConfigFile.ProjectMasterUrls.updateProject+Id,projectitem,{headers: new Headers({ 'Content-Type': 'application/json'})})
                  .toPromise()
                  .then(Response => Response);
  }

 //this method is to get particular project data 
 getProjectData(projectId:number):Promise<any>{
              return this.http.get(ConfigFile.ProjectMasterUrls.getProjectData+projectId)
                              .toPromise()
                              .then(Response => Response);
 }
}
