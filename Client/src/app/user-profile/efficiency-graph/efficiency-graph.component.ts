import { Component, OnInit,Input } from '@angular/core';
import { EfficiencyGraphService } from "./../../shared/services/efficiency-graph.service";
import { ActivatedRoute, ParamMap } from '@angular/router';

@Component({
  selector: 'efficiency',
  templateUrl: './efficiency-graph.component.html',
  styleUrls: ['./efficiency-graph.component.css']
})
export class EfficiencyGraphComponent implements OnInit {
@Input() userId:number;
  //local variable used in component
  data : any;
  efficient : number  ;

  //Initializing variable for doughnut chaty
  doughnutChartLabels:string[]=['Efficient', 'Remaining']; //setting names on graph
  doughnutChartData:number[]=[this.data, this.efficient ]; //setting the data to grap
  doughnutChartType:string="doughnut"; //defining type of chart 

  constructor(private efficiencyGraphService : EfficiencyGraphService) {
    
   }

  ngOnInit() {
    //this will get the data from 
    this.efficiencyGraphService.getEfficiencyDetail(this.userId)
                               .subscribe(data => {this.data = data;
                               //logic for douhgnut chart
                               if(this.data > 100)
                               {
                                 this.efficient=100;
                                 this.doughnutChartData = [  this.data, 0];
                               }
                               else{
                               this.efficient=100 -this.data;
                               this.doughnutChartData = [  this.data, this.efficient];
                               }
                              });                              
  }


  // event on click to graph
   public chartClicked(e:any):void {
  }

 // event on hovering on chart
 public chartHovered(e:any):void {
 }

}

