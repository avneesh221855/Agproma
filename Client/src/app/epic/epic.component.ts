import { Component, OnInit } from '@angular/core';
import { ProductBacklog } from '../shared/model/productBacklog';
import swal from 'sweetalert2';
import { Epic } from "../shared/model/epic";
import { HubConnection } from '@aspnet/signalr-client';
import { ActivatedRoute,Router} from '@angular/router';
import { ConfigFile } from '../shared/config';

@Component({
  selector: 'app-epic',
  templateUrl: './epic.component.html',
  styleUrls: ['./epic.component.css']
})
export class EpicComponent implements OnInit {

  projectId: number;
  connection: HubConnection;
  data: Array<any> //all epics will store in this variable
  userId:number;
  errorMsg:string;

  model = new Epic(null, ''); //model for adding new epic
  constructor(private route:ActivatedRoute,private router:Router) { }

  ngOnInit() {
    this.route.params.subscribe((param) =>
    this.projectId = +param['id']);
    var session = sessionStorage.getItem("id");
    this.userId = parseInt(session);
    this.connection = new HubConnection(ConfigFile.EpicUrls.connection);//for connecting with hub // when this component reload ,it will call this method
    //registering event handlers
    this.connection.on("getBacklog",data =>{this.data = data }); //for gettting all epics based on project id
    this.connection.on("whenDeleted",data => { swal('Brainstorming deleted', '', 'success') });  //sweet alerts
    this.connection.on("whenUpdated",data => { swal('Brainstorming updated', '', 'success') }); 
    this.connection.on("whenAdded",data => { swal('Brainstorming Added', '', 'success') });   
    this.connection.start().then(() => { 
    this.connection.invoke("SetConnectId",this.userId);
    this.connection.invoke("Get",this.projectId);
    })
    .catch(err=>{                        
      this.errorMsg=err;
      this.router.navigate(['/app-error/']);
    });
  }

  addBacklog(story:any, comment:any)//for adding new  epic
  {
    if (story == "") {
      swal('Please fill user story','','error');
    } 
    else {
      this.model.description = story;
      this.model.projectId = this.projectId;
      this.connection.invoke("Post",this.model)
      .catch(err=>{                        
        this.errorMsg=err;
        this.router.navigate(['/app-error/']);
      });
      this.connection.invoke("Get",this.projectId);
    }
  }

  updateBacklog(content:any, item){ //for updating  particular epic 
    if (content == "") {
      swal('Please fill user story', '', 'error');
    }
    else{
      item.description = content;
      this.connection.invoke("put",item.epicId,item)
      .catch(err=>{                        
        this.errorMsg=err;
        this.router.navigate(['/app-error/']);
      });;
    } 
  }

  deleteBacklog(item:any){       //for deleting the epic 
    swal('Are you sure?','Once deleted, you will not be able to recover this brainstorming!','warning')
    .then((willDelete) => {
      if (willDelete) {
        this.connection.invoke("Delete",item.epicId)
        .catch(err=>{                        
          this.errorMsg=err;
          this.router.navigate(['/app-error/']);
        });;
        this.connection.invoke("Get",this.projectId);
      }
      else {
        swal("Your brainstorming is safe!");
      }
    });
  }
}
