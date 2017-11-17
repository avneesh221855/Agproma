// import { async, ComponentFixture, TestBed,inject,fakeAsync,tick} from '@angular/core/testing';
// import { TeamsComponent } from './teams.component';
// import{ TeamsService} from '../shared/services/teams.service';
// import { Headers, HttpModule,  Http, Response,
//   ResponseOptions,
//   XHRBackend,
//   RequestMethod } from '@angular/http';
//   import { MockBackend,MockConnection } from '@angular/http/testing';
//   import { FormsModule } from '@angular/forms';
//   import { RouterTestingModule } from '@angular/router/testing';
//   import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
// describe('TeamsComponent', () => {
//   let component: TeamsComponent;
//   let fixture: ComponentFixture<TeamsComponent>;
//   let teamService:TeamsService;
//   let teamSpy: jasmine.Spy;


//   let teamMaster:any[]=[{TeamId:"1",ProjectId:"12",TeamName:"User Interface"},
//                         {TeamId:"2",ProjectId:"12",TeamName:"Mainframe"},
//                         {TeamId:"3",ProjectId:"12",TeamName:"Frontend"}];
//   let myTeamList:any[]=[{id:"1",MemberId:"1",TeamId:"1"},
//                         {id:"2",MemberId:"2",TeamId:"2"},
//                         {id:"3",MemberId:"3",TeamId:"3"}];

//   beforeEach(async(() => {
//     TestBed.configureTestingModule({
//       imports:[HttpModule,FormsModule, HttpModule, RouterTestingModule, BrowserAnimationsModule],
//       declarations: [ TeamsComponent ],
//       providers:[TeamsService,{ provide: XHRBackend, useClass: MockBackend }]
//     })
//     .compileComponents();
//   }));

//   beforeEach(() => {
//     fixture = TestBed.createComponent(TeamsComponent);
//     component = fixture.componentInstance;
//     teamService=fixture.debugElement.injector.get(TeamsService)
//     fixture.detectChanges();
//   });

//   //first test case
//   it('should create', () => {
//     expect(component).toBeTruthy();
//   });
//   //second test case
// it('should have called `getViewApprovedRequest`',fakeAsync(()=>{
//  teamSpy =spyOn(teamService,'getViewApprovedRequest').and.returnValue(Observable.of(myReqList));
 
//   tick();//simulates the passage of time until all pending asynchronous activities finish

//   fixture.detectChanges(); //update the dom
//   expect(csoSpy.calls.any()).toBe(true); //check that service is called
// }))
// });
