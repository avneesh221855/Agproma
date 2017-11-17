import { Component, OnInit } from '@angular/core';
import { ProjectMaster } from './../shared/model/ProjectMaster';
import { ProjectScreenService } from "./../shared/services/project-screen.service";
import { Router} from  '@angular/router';
import { HubConnection } from '@aspnet/signalr-client';

@Component({
  selector: 'app-project-screen',
  templateUrl: './project-screen.component.html',
  styleUrls: ['./project-screen.component.css']
})
export class ProjectScreenComponent implements OnInit {
  //variable that are used to manipulate the value in project
  projects:any=Array<ProjectMaster>();
  data:string;
  connection:HubConnection;
  userId:number;
  count:number = 0;
  constructor(private projectscrservice:ProjectScreenService,private router:Router) { }

  ngOnInit() {
    //this is to fetch the list of project of particular employee
      var session = sessionStorage.getItem("id");
      this.userId = parseInt(session);
      this.projectscrservice.getAllProjectOfEmployee(1)
                            .then(data =>{ 
                                            if(data != "There are no Porjects")
                                            {
                                              this.projects = data;
                                              this.projects.forEach(element => {
                                                this.count++;
                                              });
                                            }
                                            else{ this.data = "no project";}
                            });
  }
  
  //this is splice the list
  onDelete(Id:number)
  {
    var ele = this.projects.find(f => f.Id == Id);
    const index = this.projects.indexOf(ele);
    this.projects.splice(index, 1);
      this.count--;
  }
}
