import { Component, OnInit,Input,Output, EventEmitter } from '@angular/core';
import { ProjectMaster } from './../../shared/model/ProjectMaster';
import { ProjectScreenService } from "./../../shared/services/project-screen.service";
import { Router} from  '@angular/router';
import { TitleCasePipe } from '@angular/common';
import swal from 'sweetalert2';
import { ConfigFile } from './../../shared/config';

@Component({
  selector: 'app-project-detail',
  templateUrl: './project-detail.component.html',
  styleUrls: ['./project-detail.component.css']
})
export class ProjectDetailComponent implements OnInit {
  @Input() Data:ProjectMaster[];
  @Output() onDelete = new EventEmitter<number>();
  @Output() actAs = new EventEmitter<number>();
  projectname:string;
  projectdescription:string;
  technologies:string;

  constructor(private projectservice:ProjectScreenService,private router:Router) { }

  ngOnInit() {
    //used to remove the role from the session storage
    sessionStorage.removeItem('role');
  }
  
  //method is calling the service method to delete project
  delete(id:number):void{
    swal('Are you sure?','Once deleted, you will not be able to recover this project!','warning')
    .then((willDelete) => {
      if (willDelete) {
        this.projectservice.deleteProject(id)
        .then(data => { this.onDelete.emit(id);
                        swal("Poof! Your Project has been deleted!"," ","success");});
      }
      else {
        swal("Your imaginary file is safe!");
      }
    });
  }

  
  //this method is used for storing role
  enterProject(){
    sessionStorage.setItem("role",this.Data["actAs"]);
    this.router.navigate([ConfigFile.ProjectDetailUrls.navigation,this.Data["projectId"],"kanban",this.Data["projectId"]]);
  }
}