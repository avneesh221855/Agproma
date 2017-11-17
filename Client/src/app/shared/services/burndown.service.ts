import { Injectable } from '@angular/core';
import { Http, Headers, Response} from '@angular/http';
import { ConfigFile } from "../config";
import 'rxjs/add/operator/map'

@Injectable()
export class BurndownService {

  constructor(private http:Http) { }
  
  private headers = new Headers({ 'Content-Type': 'application/json' });
  

  getTaskTimes(userId:number) {
    return this.http.get(ConfigFile.BurndownServiceUrl.burndownUrl+userId)
                      .map(Response=>Response.json());
  }

}
