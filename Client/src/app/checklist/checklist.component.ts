// imports here
import { Component, OnInit, trigger, state, animate, transition, style, ViewChild, ElementRef } from '@angular/core';
import { NgStyle } from '@angular/common';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';
import { Checklist } from './../shared/model/checklist';
import { ChecklistService } from './../shared/services/checklist.service';
import { ConfigFile } from '../shared/config';
import swal from 'sweetalert2';
// import ends
@Component({
  selector: 'app-checklist',
  templateUrl: './checklist.component.html',
  styleUrls: ['./checklist.component.css']
})
export class ChecklistComponent implements OnInit {
  // variable declarations
  @ViewChild('remainSize') remainSize: ElementRef;
  @ViewChild('actualSize') actualSize: ElementRef;
  @ViewChild('completedSize') completedSize: ElementRef;
  @ViewChild('checkListDetail') checkListDetail: ElementRef;
  
  
  StatusStyle;
  totalCount:number;
  check: any;
  task: any;
  checklistStatus: number = 0;
  statusInPer;
  completedStatus:number;
  countChecklist: number = 0;
  details: Checklist[];
  actualSizeMax: number;
  model: Checklist = 
  {
    checklistId:0,
    taskId:0,
    actualSize:0,
    checklistName:null,
    status:false,
    plannedSize:0,
    remainingSize:0,
    completedSize:0
  };
  dailyStatus:Checklist;
  Event:any;
  checkListSelectedIndex: number;
  msg: string;
  constructor(private checkListService: ChecklistService,private route: ActivatedRoute) { }
  // for taking data from backend
  ngOnInit() {
    this.route.params.subscribe((param) => this.model.taskId = +param['id']);
    this.onStartComponent();
  }
  //this method is for filling data on start up of project
  onStartComponent(){
    this.checkListService.getById(this.model.taskId).subscribe((tasks => {
      this.task = tasks;
    }));
    this.checkListService.getCheckList(this.model.taskId)
      .subscribe(data => {
        this.details = data; 
        console.log(this.details);
        this.totalCount = 0;
        this.countChecklist=0;
        for (let i in this.details) {
          if (this.details[i]["status"]) {
            this.countChecklist++;
          }
        }
        for (var i in this.details) { if (i != null) { this.totalCount++; } }
        this.checklistStatus = (((this.countChecklist) / (this.totalCount)) * 100);
        this.statusInPer = (this.checklistStatus + '%')
        this.StatusStyle = { 'width': this.statusInPer };
      });
  }
  // for adding checklist of user
  addCheckList() {
    this.model.remainingSize=this.model.plannedSize;
    let totalCompletedSize=0;
    for(var checklistSize in this.details ){
      totalCompletedSize += this.details[checklistSize].plannedSize + this.model.remainingSize;
    }
    if(totalCompletedSize > this.task.plannedSize){
      swal('Sorry','You have entered size greater than the task size','error');
    }
    else{
    this.checkListService.addCheckList(this.model)
      .subscribe(result => {
        this.onStartComponent();
      },
      error => {
        this.msg = "Something Went Wrong, Please Try Again Later";
      });
    }
  }
  // task completed logic
  markCompleted(status: any, item) {
    if (status == false) {
      item.status = true;
      this.countChecklist++;
      this.checkListService.completionStatus(item.checklistId, item)
        .subscribe(result => {
          this.msg = "Successfully Completed"
        })
    }
    // task incomplete logic
    else {
      item.status = false;
      this.countChecklist--;
      this.check = status;
      this.checkListService.completionStatus(item.checklistId, item)
        .subscribe(result => {
          this.msg = "Incomplete"
        })
    }
    this.checklistStatus = (((this.countChecklist) / (this.totalCount)) * 100);
    this.statusInPer = (this.checklistStatus + '%')
    this.StatusStyle = { 'width': this.statusInPer };
  }
  
  //delete checklist
  deleteChecklist(item) {
    this.checkListService.deleteChecklists(item.checklistId)
      .subscribe(result => {
        this.totalCount--;
        if (item.status == true) {
          this.countChecklist--;
        }
        this.onStartComponent();
        this.msg = "deleted";
      });
  }
  updateRemainingTime(i,checklistName){
    this.checkListDetail.nativeElement.value = checklistName;
    this.actualSizeMax = 0;
    this.actualSize.nativeElement.value = 0;
    this.remainSize.nativeElement.textContent = 0;
    this.completedSize.nativeElement.value = 0;
    this.checkListSelectedIndex = i
    this.remainSize.nativeElement.textContent=this.details[i].remainingSize;
    this.actualSizeMax = this.details[i].remainingSize;
    this.actualSize.nativeElement.value = this.details[i].actualSize;
    console.log('actual Size',this.actualSizeMax);
  }
  calculateActual(event){
    this.completedSize.nativeElement.value = 0;
    this.actualSizeMax = this.details[this.checkListSelectedIndex].remainingSize;
    
    if(event.target.value === undefined || event.target.value ==='')
    {
      this.actualSize.nativeElement.value = this.details[this.checkListSelectedIndex].actualSize;
      console.log(this.actualSize.nativeElement.value);
    }
    else
    {
      if(event.target.value >= this.details[this.checkListSelectedIndex].actualSize)
      {
        let calculateDiff = event.target.value - this.details[this.checkListSelectedIndex].actualSize
        this.actualSizeMax = this.actualSizeMax + calculateDiff; 
        this.remainSize.nativeElement.textContent = this.actualSizeMax;
      }
      else
      {
        
        let calculateDiff = this.details[this.checkListSelectedIndex].actualSize - event.target.value;
        this.actualSizeMax = this.actualSizeMax - calculateDiff;
        console.log(this.actualSizeMax, calculateDiff);
        this.remainSize.nativeElement.textContent = this.actualSizeMax;
      }
    }
  }
  calculateRemaining(event)
  {
    
    if(event.target.value === undefined || event.target.value ==='')
    {
      this.remainSize.nativeElement.textContent = this.actualSizeMax-0;
      this.model.completedSize = event.target.value;
    }
    else
    {
      this.remainSize.nativeElement.textContent = this.actualSizeMax-parseInt(event.target.value);
      this.model.completedSize = event.target.value;
    }
  }
  updateDailyStatus()
  {
    
    this.model.remainingSize=this.remainSize.nativeElement.textContent;
    this.model.checklistId=this.details[this.checkListSelectedIndex].checklistId;
    this.model.taskId=this.details[this.checkListSelectedIndex].taskId;
    this.model.checklistName = this.checkListDetail.nativeElement.value;
    if(this.model.remainingSize == 0){
      this.model.status=true;
      this.checkListService.updateDailyStatusofTask(this.model).then(()=>this.onStartComponent());
    }
    else if(this.model.remainingSize < 0){
      swal('','Your Completed size is greater than the remaining Size ','error');
    }
    else{
        this.checkListService.updateDailyStatusofTask(this.model).then(()=>this.onStartComponent());
    }
    
  }
}