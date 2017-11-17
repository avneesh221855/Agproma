import { Component, OnInit, Input } from '@angular/core';
import { BurndownService } from '../../shared/services/burndown.service';
import { Burndown } from '../../shared/model/burndown';
import swal from 'sweetalert2';

@Component({
  selector: 'app-line-graph',
  templateUrl: './line-graph.component.html',
  styleUrls: ['./line-graph.component.css']
})
export class LineGraphComponent implements OnInit {
  @Input() userId: number;
  //declaration of variables
  taskData: Array<Burndown>;
  reversetaskData: Array<Burndown>;
  expectedTime: any[] = [];
  actualTime: any[] = [];
  tasks: any[] = [];
  lineChartLabels: Array<any> = [];
  //task lists
  lineChartData: Array<any> = [
    { data: [],label: 'Planned Hours' },
    { data: [], label: 'Actual Hours' },
  ];
  startindex: number = 0;
  endindex: number = 8;
  length: number;
  status:string;


  constructor(private burndownService: BurndownService) {

  }

  ngOnInit() {

    this.burndownService.getTaskTimes(this.userId)
      .subscribe(data => {
        this.taskData = data;
        this.reversetaskData = this.taskData.reverse();
        this.length = this.reversetaskData.length;
        this.fillData(this.startindex, this.endindex);
      });
  }





  public lineChartOptions: any = {
    responsive: true
  };

  //color scheme
  public lineChartColors: Array<any> = [
    { // grey
      backgroundColor: 'rgba(148,159,177,0.2)',
      borderColor: 'rgba(148,159,177,1)',
      pointBackgroundColor: 'rgba(148,159,177,1)',
      pointBorderColor: '#fff',
      pointHoverBackgroundColor: '#fff',
      pointHoverBorderColor: 'rgba(148,159,177,0.8)'
    },
    { // dark grey
      backgroundColor: 'rgba(77,83,96,0.2)',
      borderColor: 'rgba(77,83,96,1)',
      pointBackgroundColor: 'rgba(77,83,96,1)',
      pointBorderColor: '#fff',
      pointHoverBackgroundColor: '#fff',
      pointHoverBorderColor: 'rgba(77,83,96,1)'
    },
    { // grey
      backgroundColor: 'rgba(148,159,177,0.2)',
      borderColor: 'rgba(148,159,177,1)',
      pointBackgroundColor: 'rgba(148,159,177,1)',
      pointBorderColor: '#fff',
      pointHoverBackgroundColor: '#fff',
      pointHoverBorderColor: 'rgba(148,159,177,0.8)'
    }
  ];
  public lineChartLegend: boolean
  = true;
  public lineChartType: string = 'line';


  //this is to fill the data in chart forwarding manner
  forward() {
    this.status = '';
    this.startindex += 8;
    this.endindex += 8;
    if (this.length % 8 == 0) {
      if (this.startindex >= this.length) {
        this.startindex = 0;
        this.endindex = 8;
      }
    }
    else {
      if (this.startindex == this.length) {
        this.startindex = this.length;
        this.endindex = this.length;
        this.status = "last";
      }
      if (this.startindex > this.length) {
        this.startindex = 0;
        this.endindex = 8;
      }
    }
    this.fillData(this.startindex,this.endindex);
  }

  //this is to fill the data in chart backwarding manner
  backward() {
    this.status = '';
    this.startindex -= 8;
    this.endindex -= 8;
    if (this.length % 8 == 0) {
      if (this.startindex <= 0) {
        this.startindex = this.length - 8;
        this.endindex = this.length;
      }
    }
    else {
      if(this.endindex == 1 && this.startindex < 0)
      {
        this.status = 'first';
      }
      if(this.endindex > 0 && this.startindex < 0)
      {
        this.startindex = 0;
      }
      if(this.endindex <= 0 )
      {
        this.startindex = this.length - 8;
        this.endindex = this.length;
      }
    }
    this.fillData(this.startindex, this.endindex);
  }

  //this is to print the graph in chart
  fillData(starti: number, endi: number) {
    let _lineChartData: Array<any> = new Array(this.lineChartData.length);
    let expectedDate: Array<number> = [];
    let actualDate: Array<number> = [];
    let taskName: Array<string> = [];
    let projectName: Array<string> = [];
    let storyName: Array<string> = [];

    if (this.status =="last" && this.length%8 != 0) {
      expectedDate.push(this.reversetaskData[this.length - 1].expectedDate);
      actualDate.push(this.reversetaskData[this.length - 1].actualDate);
      taskName.push(this.reversetaskData[this.length - 1].taskName);
    }
    else if (this.status =="first" && this.length%8 != 0) {
      expectedDate.push(this.reversetaskData[0].expectedDate);
      actualDate.push(this.reversetaskData[0].actualDate);
      taskName.push(this.reversetaskData[0].taskId.toString());
      storyName.push(this.reversetaskData[0].storyName);
      projectName.push(this.reversetaskData[0].projectName);
    }
    else {
      this.reversetaskData.slice(starti, endi).forEach(p => expectedDate.push(p.expectedDate));
      this.reversetaskData.slice(starti, endi).forEach(p => actualDate.push(p.actualDate));
      this.reversetaskData.slice(starti, endi).forEach(p => taskName.push(p.taskId.toString()));
      this.reversetaskData.slice(starti, endi).forEach(p => projectName.push(p.projectName));
      this.reversetaskData.slice(starti, endi).forEach(p => storyName.push(p.storyName));

    }


    this.lineChartLabels = taskName;
    _lineChartData[0] = { data: expectedDate , label: this.lineChartData[0].label };
    _lineChartData[1] = { data: actualDate , label: this.lineChartData[1].label };
    this.lineChartData = _lineChartData;

  }

  // event on click to graph
  public chartClicked(e: any): void {
  }

  // event on hovering on chart
  public chartHovered(e: any): void {
  }

}
