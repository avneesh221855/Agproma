
import { Component, OnInit } from '@angular/core';
import { Task } from '../shared/model/task';
import swal from 'sweetalert2';
import { HubConnection } from '@aspnet/signalr-client';
import { ActivatedRoute } from '@angular/router';
import { ConfigFile } from './../shared/config';

@Component({
  selector: 'app-taskadd',
  templateUrl: './taskadd.component.html',
  styleUrls: ['./taskadd.component.css']
})
export class TaskAddComponent implements OnInit {
  //variable declaration
  sprintId:number;
  sub: string = "";
  data: Task[]=[];
  connection: HubConnection;
  userId:number;

  constructor(private route:ActivatedRoute) {

  }
  //this will get the task backlog list
  ngOnInit() {
    this.route.params.subscribe((param) =>{ this.sprintId = +param['id'];});
    this.connectToHub();
  }

  //this is to make connection with the hub
  connectToHub(){
    var session = sessionStorage.getItem("id");
    this.userId = parseInt(session);
    this.connection = new HubConnection(ConfigFile.TaskAddUrls.connection);//for connecting with hub // when this component reload ,it will call this method
    //registering event handlers
    this.connection.on("gettask",data=>{this.data = data;});//this will return task backlogs
    this.connection.on("whenUpdated",data => { swal('Task Updated', '', 'success') }); //sweet alert when task happens
    this.connection.on("whenAdded",data => { swal('Task Added', '', 'success') });   
    this.connection.start().then(() => { 
    this.connection.invoke("SetConnectionId",this.userId);
    this.connection.invoke("GetTaskBacklogs",this.sprintId);
    });
  }

  //this will add new task to the backlog
  addBacklog(taskName: string, startDate: any, endDate: any) {
    //this will give alert if nothing is entered as task
    if ((taskName == "")) {
      swal('Task Cannot Be Empty ', '', 'warning');
    }
    //to validate start date greater than enddate
    //this will work if task name is entered and  add new task to backlog
    if (taskName && startDate < endDate) {
      let model = new Task(0,this.sprintId,taskName,0,startDate,endDate,new Date(ConfigFile.ActualEndDate));
      this.connection.invoke("PostTask",model,this.sprintId)
      this.connection.invoke("GetTaskBacklogs",this.sprintId);
    }
    else{
      swal('startDate cannot be greater than enddate','', 'warning');
    }
  }
  //this will uddate task backlog values
  updateBacklog(content: any, item: any, startDate: any, endDate: any) {
    //this will give alert if no task is entered for updation(empty value)
    if (content == "") {
      swal('Enter Some Task', '', 'warning')
    }
    if(content && startDate < endDate) {
      item.taskName = content;
      item.startDate = startDate;
      item.endDate = endDate;
      //this will give alert if task is successfully upadated
      this.connection.invoke("UpdateTask",this.sprintId,item);
    }
    else{
      swal('startDate cannot be greater than enddate','', 'warning');
    }
  }
}