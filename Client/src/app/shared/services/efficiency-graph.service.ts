import { Injectable } from '@angular/core';
import { Http } from "@angular/http";
import { ConfigFile } from "../config";


@Injectable()
export class EfficiencyGraphService {

  constructor(private http : Http) { }
  

  getEfficiencyDetail(userId : number){
    //This method will get the details for Efficiency
    return this.http.get(ConfigFile.EfficiencyService.efficiencyurl+userId)
                    .map(response => response.json());
              
  }

}
