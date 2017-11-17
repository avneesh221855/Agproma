import { Component, OnInit } from '@angular/core';
import { Sprint } from '../shared/model/sprint';
import { HubConnection } from "@aspnet/signalr-client/dist/src";
import { ActivatedRoute } from "@angular/router";
import swal from 'sweetalert2';
import { ConfigFile } from '../shared/config';
import { ProductBacklog } from '../shared/model/productBacklog';
import * as moment from 'moment';

@Component({
  selector: 'app-sprint',
  templateUrl: './sprint.component.html',
  styleUrls: ['./sprint.component.css']
})
export class SprintComponent implements OnInit {
  //variable declaration
  projectData: any;
  userId: number;
  projectId: number;
  sprints: Array<Sprint>;
  backlogs: Array<any>;
  unassigned:Array<ProductBacklog>;
  connection: HubConnection;
  newsprint: Sprint = new Sprint(null, '', 0, '', 0, null, null, 0, 0, 0);
  constructor(private route: ActivatedRoute) { }

  ngOnInit() {
    //getting member Id from session
    var session = sessionStorage.getItem("id");
    this.userId = parseInt(session);

    //getting project id from route
    this.route.params.subscribe((param) => this.projectId = +param['id']);

    //register connection methods and get all the sprints according to the project id.
    this.connectBacklogHub();
  }
  //this is for connection to hub socket
  connectBacklogHub() {
    this.connection = new HubConnection(ConfigFile.SprintUrls.connection);

    //get Project Related Details
    this.connection.on("projectDetails", projectData => {
      this.projectData = projectData;
    });

    //register getSprints method and get returned sprints from backend.
    this.connection.on("getSprints", data => {
      this.sprints = data
    });

    this.connection.on("updateSprint",(sprintId,story,unassigned)=>{
      let index= this.sprints.findIndex(s=>s.sprintId==sprintId);
      this.unassigned=unassigned;

    });
    //register getBacklogs and get returned backlogs from backend.
    this.connection.on("getBacklogs", (data,unassigned) => {
      this.backlogs = data;
      this.unassigned=unassigned;

      //sort the backlogs according to user story priority
      // this.backlogs.sort(function (a, b) {
      //   return a.priority - b.priority;
      // });
    });

    //register postSprints Method and push sprint to the sprints.
    this.connection.on("postSprints", data => {
      swal('Sprint Added', '', 'success');
      this.sprints.push(data);
    });

    //establish the connection and get sprints and backlogs specific to projectId
    this.connection.start()
      .then(() => {
        //updates the connection id for the user/
        this.connection.invoke("SetConnectionId", this.userId); //member Id

        //get the project details for a project.
        this.connection.invoke("GetProjectDetails",this.projectId);

        //get all the sprints for a particular projectID
        this.connection.invoke("GetSprints", this.projectId);

        //get all the backlogs
        this.connection.invoke("GetAllBacklogs", this.projectId);
      });
  }

  //compare whether story exist in sprint or not
  compareStory(sprintId, storysprintId) {
    if (sprintId == storysprintId) {
      return true;          //sprint are available for that particular sprint.
    }
    else {
      return false;         //sprint are not available for particular sprint.
    }
  }

  //assign task to particular members
  updateStoryInSprint($event, sprintNo: number) {
    let storyData: any = $event.dragData;
    //invoke method for updating story in sprint.
    this.connection.invoke("UpdateStoryInSprint", storyData, sprintNo, this.projectId);
  }

  updateEndDate(startDate: Date) {
    let currentDate: Date;
    let endDay: Date
    currentDate = new Date(startDate);
    endDay = new Date();
    endDay.setDate(currentDate.getDate() + this.projectData.rhythm);
    this.newsprint.setEndDate(moment(endDay).format('YYYY-MM-DD'))

  }

  //it will add sprint in the sprint container
  onSaveSprint() {
    //invoke Add Sprint method of Backend
    if((this.newsprint.sprintName.replace(/ /g , "") == "") || (this.newsprint.sprintGoal.replace(/ /g , "") == "") || (this.newsprint.startDate == null)) {
      swal('Please enter valid details', '', 'error');
    }
    else {
      swal('Sprint Added', '', 'success');
      this.newsprint.projectId = this.projectId;
      this.newsprint.totalDays = this.projectData.rhythm;
      this.connection.invoke("AddSprint", this.newsprint);
    }
  }
} 