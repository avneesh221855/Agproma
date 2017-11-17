import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HubConnection } from "@aspnet/signalr-client";
import swal from 'sweetalert2';
import { ProductBacklog } from '../shared/model/productBacklog';
import { ConfigFile } from './../shared/config';
@Component({
  selector: 'app-backlog',
  templateUrl: './backlog.component.html',
  styleUrls: ['./backlog.component.css']
})
export class BacklogComponent implements OnInit {
  projectId: number;//for getting particular project
  stories: Array<ProductBacklog>;//for storing user stories
  userId: number;
  length:number;//for storingthe length of array
  model: ProductBacklog = new ProductBacklog(null,'','',null,null,null,null, null);  //model for adding new user story
  connection: HubConnection; //for connection
  errorMsg:string;//for storing error message

  constructor(private route: ActivatedRoute,private router:Router) { }

  ngOnInit() {
    //get the id for the user.
    var session = sessionStorage.getItem("id");
    this.userId = parseInt(session);
    //getting project id from route
    this.route.params.subscribe((param) => this.projectId = +param['id']);
    //register and invoke connection
    this.connectBacklogHub();
  }
  connectBacklogHub() {
    this.connection = new HubConnection(ConfigFile.ProductBacklog.connection);
    //register to get Backlogs from the backend.
    this.connection.on("getbacklog", backlogs => {
      this.stories = backlogs;
      this.length=this.stories.length;
      this.stories.sort(function (a, b) {
        return a.priority - b.priority;
      });
    });

    //get the added backlog return data with socket
    this.connection.on("postBacklog", data => {
      this.stories.push(data);
    });

    //get the updated backlog
    this.connection.on("updateBacklog", backlog => {
    });

    //get the deleted backlog items
    this.connection.on("deleteBacklog", storyId => {
    });

    //establish the socket connection
    this.connection.start().then(() => {

      //set connection id and update status and get notified for changes
      this.connection.invoke("setConnectionId", 1); //member ID need to be passed

      //invoke the get backlog method
      this.connection.invoke("GetBacklog", this.projectId);
    }).catch(err=>{
      
            this.errorMsg=err;
            this.router.navigate(['/app-error/']);
          });;
  }

  //Add a new user story
  addBacklog(story: any, comment: any, priority,size:number) {
    if (story == "" ||priority==null||size==null||comment=="") {
      swal('Please fill user story', '', 'error');
    } else {
      this.model.storyName = story;
        this.model.comments = comment
        this.model.projectId = this.projectId;
        this.model.status = 0;
        this.model.priority = priority;
        this.model.plannedSize = size;
        this.model.actualSize=0;
        
        console.log("add function",this.model);
        //invoke backend post method
        this.connection.invoke("PostBacklog", this.model)
                      .then(data=>{swal('User Story Added', '', 'success')
                      .catch(err=>{                        
                        this.errorMsg=err;
                        this.router.navigate(['/app-error/']);
                      });
      
                      });
      this.connection.invoke("GetBacklog", this.projectId)
                     .then(data=>{this.stories.sort(function (a, b) {
                        return a.priority - b.priority;
                        });
                      });
      }
  }

  //for updating a particular user story
  updateBacklog(content:string, comment: any, priority: any, item,size:number) {
    if (content == "") {
      swal('Please fill user story', '', 'error');
    }
    else {
      item.storyName = content;
      item.comments = comment;
      item.priority = priority;
      item.plannedSize=size;
      this.connection.invoke("UpdateBacklog", item)
                    .then(data=>{swal('User Story Updated', '', 'success')})
                    .catch(err=>{                        
                      this.errorMsg=err;
                      this.router.navigate(['/app-error/']);
                    });
      this.connection.invoke("GetBacklog", this.projectId)
                    .then(data=>{this.stories.sort(function (a, b) {
                      return a.priority - b.priority;
                      });
                    });
    }
  }

  //delete a backlog
  deleteBacklog(item: any) {
      swal('Are you sure?','Once deleted, you will not be able to recover this User story!','warning')
      .then((willDelete) => {
      if (willDelete) {
         this.connection.invoke("DeleteBacklog", item.storyId, this.projectId)
        .then(data=>{ swal('User Story Deleted', '', 'success')})
        .catch(err=>{                        
          this.errorMsg=err;
          this.router.navigate(['/app-error/']);
        });
        this.connection.invoke("GetBacklog", this.projectId)
                .then(data=>{ this.stories.sort(function (a, b) {
                    return a.priority - b.priority;
                  });
                });
       
      }
      else {
        swal("Your user story is safe!");
      }
    });
  }
}
