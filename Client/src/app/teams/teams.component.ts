import { Component, OnInit } from '@angular/core';
import{ TeamMaster} from '../shared/model/teamMaster';


import { ActivatedRoute, Router } from "@angular/router";
import { Members } from "../shared/model/members";
import swal from 'sweetalert2';
import { TitleCasePipe } from '@angular/common';
import { HubConnection } from '@aspnet/signalr-client';
import { ConfigFile } from '../shared/config'
@Component({
  selector: 'app-teams',
  templateUrl: './teams.component.html',
  styleUrls: ['./teams.component.css']
})
export class TeamsComponent implements OnInit {
   projectId:number;
   teams:TeamMaster[];
   teamList:Members[];
   teamList1:Members[];
   errorMsg: any;
   val:string="";
   connection:HubConnection;
   userId:number;

   letter:string;
  constructor(private route:ActivatedRoute, private router : Router ) {   

   

  }
  ngOnInit() {
      this.route.params.subscribe(param =>this.projectId = +param['id']);
      var session = sessionStorage.getItem("id");
      this.userId = parseInt(session);
      this.connectToHub();
  }
  //this is to make connection with the hub
  connectToHub(){
    
    // for connecting with hub 
    this.connection = new HubConnection(ConfigFile.TeamUrls.getTeamUrl);
    // when this component reload ,it will call this method
    // registering event handlers
    this.connection.on("getTeams",data =>{ this.teams = data });//this will return list of teams
    this.connection.on("getAvailableMember",data =>{ this.teamList = data; this.teamList.sort(this.sortByName);this.teamList1=data; });//this will return list of available member
    this.connection.on("whenUpdated",data => {  }); 
    this.connection.on("whenAdded",data => { swal('Team Added', '', 'success') });
    this.connection.on("whenDeleted",data => { swal('Member Removed', '', 'success') });   
    this.connection.start().then(() => { 
    this.connection.invoke("SetConnectionId",this.userId);
    this.connection.invoke("GetTeams",this.projectId)
                   .then(data => {this.connection.invoke("GetAvailableMember",this.projectId);})
                   .catch(error=>{
                   this.errorMsg = error ;{console.log("error is "+this.errorMsg)} this.router.navigate(['/app-error/'+this.errorMsg])
                   });
    });
  }
  //method for dropping members in appropriate order
  filterByName(event:Event){
    this.letter=(<HTMLInputElement> event.target).value;
    this.teamList1=this.teamList1.sort();
    this.teamList1= this.teamList.filter(t=>t["memberName"].toLowerCase().startsWith(this.letter.toLowerCase()));
  }
  //this will add new team 
    addTeam(name:string){
      if(name==""){
        swal('Enter valid team name','','warning');
      }
      if(name){
        let mobject:TeamMaster = new TeamMaster(this.projectId,name);
        this.connection.invoke("AddTeam",mobject)
                       .then(data => {this.connection.invoke("GetTeams",this.projectId);})
                       .catch(error=>{
                          this.errorMsg = error ; this.router.navigate(['/app-error/'+this.errorMsg])
                        });
        }
    }
  //this will remove a particular team member
  removeMember(){
    this.connection.invoke("GetAvailableMember",this.projectId)
                   .catch(error=>{
                      this.errorMsg = error ; this.router.navigate(['/app-error/'+this.errorMsg])
                    });                  
  }
 //this will add member to a particular team
  teamListUpdate($event: any,teamId:number) {
    if(teamId){
    let teamMember:any  = $event.dragData;
    let Id:number=teamMember.memberId;
    let mobject:Members = new Members(teamId,Id);
    this.connection.invoke("UpdateteamMember",mobject,this.projectId)
                   .catch(error=>{
                      this.errorMsg = error ; this.router.navigate(['/app-error/'+this.errorMsg])
                    });       
    }
  }
   
  //this method will get team members 
  getTeamMebers(teamid:number){
    if(this.teamList)
      {
        return this.teamList.filter(t=>t["teamId"]==teamid);
      }
    else
        return null;
  }
  //this is for deleting a member from a team
  delete(id: number) {
    if (id) {
      this.connection.invoke("Delete",id,this.projectId)
                     .catch(error=>{
                        this.errorMsg = error ; this.router.navigate(['/app-error/'+this.errorMsg])
                      });
      this.connection.invoke("GetAvailableMember",this.projectId)
                     .catch(error=>{
                        this.errorMsg = error ; this.router.navigate(['/app-error/'+this.errorMsg])
                      }); 
    }
  }
  //compare whether member is in team or not
  compareMember(teamId,memteamId) {
    if (teamId == memteamId) {
      return true;          //memnber is present in team.
    }
    else {
      return false;         //member is not present in team.
    }
  }
  //this method will sort member names
  sortByName(nameA,nameB){
    if(nameA.memberName.toLowerCase()<nameB.memberName.toLowerCase()){
      return -1;             //nameA will be placed before nameB
    }
    if(nameA.memberName.toLowerCase()>nameB.memberName.toLowerCase()){
      return 1;             //nameA will be placed after nameB
    }
    return 0;             //unchanged
    
  }

  efficiency(Id:number){
  
    this.router.navigate(["role-dashboard",this.projectId,"userprofile",Id]);

  }
}
