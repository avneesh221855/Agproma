import { Component, OnInit, ViewChild, ElementRef  } from '@angular/core';
import { KanbanService } from "../shared/services/kanban.service";
import { TaskBackLog } from "../shared/model/TaskBacklog";
import { Sprint } from "../shared/model/Sprint";
import { ActivatedRoute, ParamMap } from '@angular/router';
import Chart from 'chart.js';

@Component({
  selector: 'kanban',
  templateUrl: './kanban-board.component.html',
  styleUrls: ['./kanban-board.component.css']
})
export class KanbanBoardComponent implements OnInit {

  //local variable used in backend
  isDataAvailable = false;
  projectData:Array<any> = [];
  releaseData:Array<any> = [];
  sprintData:Array<any> = [];
  taskList:Array<TaskBackLog> = [];
  sprintList:Array<Sprint> =[];
  releaseList:Array<any> = [];
  showList:Array<any> = [];
  projectId:number;
  sprintItem:any;
  releaseItem:any;

  @ViewChild('deploymentVelocity') deploymentVelocity: ElementRef;
  public sprintPlannedWork=[10, 20, 30, 40];
  public sprintRealizedWork=[10, 40, 10, 50];
  public sprintsArray= ['Sprint 1', 'Sprint 2', 'Sprint 3', 'Sprint 4'];
  public velocity:any={};
  public sprints:any=[];


  constructor(private kanbanService : KanbanService, private route : ActivatedRoute) { }

  ngOnInit() {

    // getting sprint id from route 
    this.route.params.subscribe((param) =>{this.projectId = +param['id']});
    //get project related data for a project
    this.kanbanService.getProjectDetails(this.projectId)
                      .subscribe(projectData=>{ this.isDataAvailable= true;
                              this.projectData = projectData;
                              //call initialize method
                              this.initialize();
                              //call fulldetail for first landing view
                              this.fulldetail();
                              });

    this.kanbanService.getSprintDetails(this.projectId)
                      .subscribe(data => {
                        this.sprints = data;
                      });

    this.kanbanService.getVelocityDetails(this.projectId)
                      .subscribe(data => {
                        this.velocity = data;
                      });
    // Dashboard charts code start //       
    
    let deploymentVelocityCtx = this.deploymentVelocity.nativeElement.getContext('2d');
    var data = {
type: 'bar',
data: {
datasets: [{
      label: 'Average Realized',
      data: [40, 40, 40, 40],
      borderColor: '#00b33c',
      borderWidth:1,
      fill: false,
      type: 'line'
    },
    {
      label: 'Average Last 4',
      data: [30, 30, 30, 30],
      borderColor: '#e6e600',
      borderWidth:1,
      fill: false,
      type: 'line'
    },
    {
      label: 'Average worst 3 in last 4',
      data: [20, 20, 20, 20],
      borderColor: '#ff1a1a',
      borderWidth:1,
      fill: false,
      type: 'line'
    },{
      label: 'Planned Work',
      data: this.sprintPlannedWork,
      borderWidth:1,
      borderColor: '#ff9933',
      backgroundColor: '#ffb366',
      type: 'bar'
    }, 
    {
      label: 'Realized Work',
      data: this.sprintRealizedWork,
      borderWidth:1,
      borderColor: '#3377ff',
      backgroundColor: '#80aaff',
      type: 'bar'
    },],
labels: this.sprintsArray
},
options: {
    scales: {
        yAxes: [{
              ticks: {
                beginAtZero:true
            }
        }],
        xAxes: [{
                 barPercentage:1.0,
                 categoryPercentage:0.8,
                 gridLines: {
                       },
        ticks: {
                beginAtZero:true
            }
             }]
    },
}
}
    var chart = new Chart(
        deploymentVelocityCtx,
        data
    );

    // Dashboard charts code end // 

  }
  
  //this method is to fill the list according to the project sprint and task and release plan
  initialize()
  {
   this.projectData["sprint"].forEach((p,i) =>  this.sprintData.push(p));
   this.projectData["release"].forEach((p,i) => this.releaseData.push(p));
  }

  //this method is to assign the value to the main list so that it can be fill according to release plan status
  fulldetail(){
    this.showList =[];
  //  this.projectData.forEach((p,i) => this.showList.push(p));
  this.showList = this.projectData["release"];
  }
  
  //this method is to assign the value to the main list so that it can be fill according to sprint status
  releaseDetail(){
    this.showList =[];
    this.projectData["sprint"].forEach((p,i) => {
                                        if(p["increment"] == this.releaseItem["increment"])
                                          {
                                             this.showList.push(p);
                                          }});
  }

  //this method is to assign the value to the main list so that it can be fill according to task status
  sprintDetail(){
    this.showList =[];
    this.projectData["stories"].forEach((p,i) =>p["sprint_UserStory"].forEach((q,i) => {
                                      if(q["sprintId"] == this.sprintItem["sprintId"])
                                        {
                                          p.tasks.forEach((r,i) => this.showList.push(r));
                                        }
    }));
    console.log(this.showList);
  }



}
