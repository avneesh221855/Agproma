import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from "@angular/router";

@Component({
  selector: 'app-error',
  templateUrl: './error.component.html',
  styleUrls: ['./error.component.css']
})
export class ErrorComponent implements OnInit {

  error : any;
  constructor(private route : ActivatedRoute) { }

  ngOnInit() {
    this.route.params.subscribe(params=>this.error = params.id),{}

  }

}
