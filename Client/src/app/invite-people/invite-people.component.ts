import { Component, OnInit } from '@angular/core';
import { InvitePeopleService } from "../shared/services/invite-people.service";
import swal from 'sweetalert2';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { LoginService } from '../shared/services/login.service';
import { Members } from '../shared/model/members';
import { Master } from "../shared/model/master";

@Component({
  selector: 'app-invite-people',
  templateUrl: './invite-people.component.html',
  styleUrls: ['./invite-people.component.css']
})
export class InvitePeopleComponent implements OnInit {


    //local variable used in this component
  data:Master[];
  memberDetail:Members[]=[];
  inviteList: Members[];
  letter: any;

  private model = {
    projectId: 0,
    email: '',
  };
private userDetail={
  memberName:''
}  

  constructor(private invitePeople : InvitePeopleService,private loginservice:LoginService, private route : ActivatedRoute, private router : Router) { }

  ngOnInit() {
    //this is method is get the id from project screen component and show it
    this.loginservice.getAll().subscribe(data=>{this.data=data});
    this.route.params.subscribe((param) =>
      this.model.projectId = +param['id']);
    this.loginservice.getUserData(this.model.projectId).subscribe(data => { this.memberDetail = data.json(), this.inviteList = this.memberDetail });

  }
  //method for dropping members in appropriate order
  filterByName(event: Event) {
    this.letter = (<HTMLInputElement>event.target).value;
    this.inviteList = this.memberDetail.filter(t => t["memberName"].toLowerCase().startsWith(this.letter.toLowerCase()));
  }

  inviteMember() {
    //this method will first check whether the user has accounnt with AgProMa
    //if not then an email will be trigged to that user
    if (!this.model.email.includes('@' && '.')) {
      swal('', "please enter valid email address", "error");
    }
    else {
      this.invitePeople.emailto(this.model)
        .then(data => {
          console.log("hellooooo",data);
          if(data.json()=="Already Exist"){
          swal('', 'Team Member Already Exist', 'error')}
          else if(data.json()=="Mail Sent"){
          swal('E-mail Sent!', 'Please check your email and verify yourself', 'success')}},error=>
          this.router.navigate(['/app-error/'])
        );
        
        }
  }
}
