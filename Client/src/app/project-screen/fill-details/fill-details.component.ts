import { Component, OnInit } from '@angular/core';
import { ProjectMaster } from '../../shared/model/ProjectMaster';
import { ProjectScreenService } from './../../shared/services/project-screen.service';
import { Router,ActivatedRoute } from '@angular/router';
import { ConfigFile } from './../../shared/config';

@Component({
  selector: 'app-fill-details',
  templateUrl: './fill-details.component.html',
  styleUrls: ['./fill-details.component.css']
})
export class FillDetailsComponent implements OnInit {
  updateId:number;
  condition:string;
  token:boolean;
  projectname:string;
  projectdescription:string;
  technologies:string;
  modeOfOperation:string;
  modeOfOperations:string[] = ['SCRUM'];
  leaderId:number;
  projectData:ProjectMaster = new ProjectMaster("",1,"","","",0,0,"","","refer to the sprints");

  constructor(private projectscrservice:ProjectScreenService,private route:ActivatedRoute,private router:Router) { }

  ngOnInit() {
    this.route.params.subscribe((param) => {
                                      this.updateId = +param.id;
                                      this.condition = param.id;
    });
     if(isNaN(this.updateId)){
     }
     else{
       this.projectscrservice.getProjectData(this.updateId)
                             .then(data => {this.projectData = data.json()});
     }
    var session = sessionStorage.getItem("id");
    this.leaderId = parseInt(session);
  }

  //this method is to go back on previous page
  backOnPrevious(){
    this.router.navigateByUrl(ConfigFile.FillDetailsUrls.backOnPrevious);
  }

  //this method is used to add new project
  AddProject()
  {
    this.projectData["modeOfOperation"] = this.modeOfOperation;
    if(this.projectData["modeOfOperation"] !=null && this.projectData["velocity"] != 0 && this.projectData["rhythm"] != 0
     &&  this.projectData["projectDescription"] != "" && this.projectData["technologyUsed"] != "" && this.projectData["name"] != "")
    {

      this.projectscrservice.addNewProject(this.projectData)
                            .then(data => this.router.navigate([ConfigFile.FillDetailsUrls.dashboardNavigation]));
    }
    else
    {
      window.alert("please fill all the details, Thank you!!!");
    }
  }

  //method is calling the service method to update project
  updateProject()
  {
    this.projectData["modeOfOperation"] = this.modeOfOperation;
    if(this.projectData["modeOfOperation"] !=null && this.projectData["velocity"] != 0 && this.projectData["rhythm"] != 0
    &&  this.projectData["projectDescription"] != "" && this.projectData["technologyUsed"] != "" && this.projectData["name"] != "")
  {
      this.projectscrservice.updateProject(this.updateId,this.projectData)
                            .then(data => this.router.navigate([ConfigFile.FillDetailsUrls.dashboardNavigation]));
    }
    else
    {
      window.alert("please fill all the details, Thank you!!!");
    }
  }
}
