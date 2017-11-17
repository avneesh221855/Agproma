import { Component } from '@angular/core';
import { TaskBackLog } from "../shared/model/TaskBacklog";
import { TeamMaster } from "../shared/model/teamMaster";
import { ActivatedRoute, Router } from "@angular/router";
import { Members } from "../shared/model/members";
import { HubConnection } from '@aspnet/signalr-client';
import swal from 'sweetalert2';
import { ConfigFile } from '../shared/config'

@Component({
    selector: 'multi',
    templateUrl: './taskAssign.component.html',
    styleUrls: ['./taskAssign.component.css']
})
export class TaskAssignComponent {
    //available task
    AvailTask:TaskBackLog[]; 
    AvailTeam:TeamMaster[]; 
    TeamMemberList:Members[];
    sprintId:number;
    userId:number;
    projectId:number;
    names:Members[];
    data:TaskBackLog;
    connection:HubConnection;
    myId : number;

    constructor(private route:ActivatedRoute,private router:Router) { } //inject TaskAssignservices
    
    ngOnInit(){
        this.route.params.subscribe(param =>this.sprintId = +param['id']);
        var session = sessionStorage.getItem("id");
        this.userId = parseInt(session);

        //this is call to connect the hub
        this.connectToHub();
    }

    //this is to set connection from hub
    connectToHub(){
        // for connecting with hub 
        this.connection = new HubConnection(ConfigFile.TaskAssignUrls.connection);
        // when this component reload ,it will call this method
        // registering event handlers
        this.connection.on("getAllTaskDetail",data =>{ this.AvailTask = data;console.log(this.AvailTask); });//this will return list of teams
        this.connection.on("getTeamList",data =>{ this.AvailTeam = data;   });//this will return list of available member
        this.connection.on("getTeamMember",data => { this.TeamMemberList = data; console.log(this.TeamMemberList);});
        this.connection.on("getName",data => {this.names= data;});
        this.connection.on("getProjectId",data => { this.projectId = data ;})
        //sweet alert when task happens following 3
        this.connection.on("whenAssigned",data => { swal('Member Assigned To task', '', 'success') }); 
        this.connection.start().then(() => { 
        this.connection.invoke("SetConnectionId",this.userId);
        this.connection.invoke("GetAllTaskDetail",this.sprintId);//get the list of the tasks in a particular project
        this.connection.invoke("GetTeamList",this.sprintId);//get the team according to the the sprint id
        this.connection.invoke("GetName",this.sprintId);
        this.connection.invoke("GetProjectId",this.sprintId);
        });
      }

    //get the list of the team members according to their teams in a particular project
    getTeamMember(){
        this.connection.invoke("GetTeamMember",this.myId);
    }

    //after adding the member to the list the list must be updated with the member name
    teamListupdate($event,id:number){  
       let teamMember:any  = $event.dragData;
       this.connection.invoke("AssignTask",id,teamMember)//assign task to particular members
                      .then(data => {  this.connection.invoke("GetAllTaskDetail",this.sprintId); });//return updated list in json
     
    }

    //method to bring member name from member id
    getName(task:TaskBackLog):string{
        return this.TeamMemberList.filter(t=>t["MemberId"]==task.PersonId)[0]["MemberName"];//filter members from member id and bring member name
     }

     //this method is to compare wether a person exist in team or not
     compareTask(personId:number,memberId:number){

      if(personId == memberId){return true;}
      else return false;
     }

     //this method is to check wether person who was assigned is accessing the task to do work
    checkPerson(task:any){
        if(task["personId"] == this.userId)
        { this.router.navigate(["role-dashboard",this.projectId,"app-checklist",task.taskId]); }
        else
            swal('This task is not assigned to you!!!!','   ','error')
    }
}  