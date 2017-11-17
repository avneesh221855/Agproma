import { TestBed, async, inject } from '@angular/core/testing';
import { Router } from '@angular/router';
import {
  HttpModule,
  Http, RequestMethod,
  Response,
  ResponseOptions,
  XHRBackend
} from '@angular/http';
import { RouterTestingModule } from '@angular/router/testing';
import { MockBackend } from '@angular/http/testing';
import { FormsModule } from '@angular/forms';
import { ChecklistService } from './checklist.service';
import { Checklist } from "../model/checklist";
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
const mockResponse =                              //Response being mocked for testing
  [
    { "taskId": 29, "checklistId": 18, "checklistName": "CSO UI", "status": false, "startDate": "2017-09-11", "endDate": "2017-10-11", "taskBacklog": null },
    { "taskId": 30, "checklistId": 19, "checklistName": "CSO UI", "status": false, "startDate": "2017-09-11", "endDate": "2017-10-11", "taskBacklog": null },
    { "taskId": 31, "checklistId": 20, "checklistName": "CSO UI", "status": false, "startDate": "2017-09-11", "endDate": "2017-10-11", "taskBacklog": null },
    { "taskId": 32, "checklistId": 21, "checklistName": "CSO UI", "status": false, "startDate": "2017-09-11", "endDate": "2017-10-11", "taskBacklog": null }
  ];
describe('ChecklistService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({                              /* creates a module that overrides the actual dependencies with testing dependencies*/
      imports: [HttpModule, RouterTestingModule, FormsModule],    //imports being declared before setting the testing environment
      providers: [
        ChecklistService,                                      //provider tells the service on which testing is being performed
        { provide: XHRBackend, useClass: MockBackend },
      ]
    });
  });                                                    //end of first before each
  describe('checking', () => {              //start of second describe
    let service: ChecklistService;
    beforeEach(inject([Http, XHRBackend], (http: Http, back: MockBackend,router:Router) => {    /* injecting service and backend dependencies*/
      service = new ChecklistService(http,router);
    }));
    it('can instantiate service when inject service',       //First test case begins
      inject([ChecklistService], (service: ChecklistService) => {
        expect(service instanceof ChecklistService).toBe(true);
      }));                                                      //First test case ends
    it('can instantiate service with "new"', inject([Http], (http: Http,router:Router) => {      //Second test case begins
      expect(http).not.toBeNull('http should be provided');
      let service = new ChecklistService(http,router);
      expect(service instanceof ChecklistService).toBe(true, 'new service should be ok');
    }));                                                                            //Second test case ends
    it('can provide the mockBackend as XHRBackend',                   //Third test case begins
      inject([XHRBackend], (backend: MockBackend) => {
        expect(backend).not.toBeNull('backend should be provided');
      }));
    //Third test case ends
    it('getById',             //Fourth test case Begins
      inject([ChecklistService, XHRBackend], (ChecklistService, mockBackend) => {      //injecting the service and backend dependencies
        mockBackend.connections.subscribe((connection) => {          /* setting up connections to Http whenever someone subcribes 
                                                               to an http call */
          connection.mockRespond(new Response(new ResponseOptions({
            body: JSON.stringify(mockResponse)
          })));
        });
        ChecklistService.getById(1).subscribe((data) => {
          expect(data.length).toBe(4);
          expect(data[0].taskId).toEqual(29);
          expect(data[1].checklistId).toEqual(19);
          expect(data[2].checklistName).toEqual("CSO UI");
          expect(data[3].status).toEqual(false);
        });
      }));
    it('getCheckList',          //Fifth test case
      inject([ChecklistService, XHRBackend], (ChecklistService, mockBackend) => {      //injecting the service and backend dependencies
        mockBackend.connections.subscribe((connection) => {          /* setting up connections to Http whenever someone subcribes 
                                                               to an http call */
          connection.mockRespond(new Response(new ResponseOptions({
            body: JSON.stringify(mockResponse)
          })));
        });
        ChecklistService.getCheckList(1).subscribe((data) => {
          expect(data.length).toBe(4);
          expect(data[0].taskId).toEqual(29);
          expect(data[1].checklistId).toEqual(19);
          expect(data[2].checklistName).toEqual("CSO UI");
          expect(data[3].status).toEqual(false);
        });
      }));
    it('completionStatus',                                  //SIXTH test case begins
      inject([ChecklistService, XHRBackend], (checklistService, mockBackend) => {
        mockBackend.connections.subscribe((connection) => {
          connection.mockRespond(new Response(new ResponseOptions({
            status: 200
          })
          ));
          expect(connection.request.method).toEqual(RequestMethod.Put, 'should return with correct method');
        });
        checklistService.completionStatus(18, { "taskId": 29, "checklistId": 18, "checklistName": "CSO UI", "status": false })
          .subscribe((res) => { expect(res.status).toBe(200); })
      }));
    it('completionStatuswitherror',                                  //Seventh test case begins
      inject([ChecklistService, XHRBackend], (checklistService, mockBackend) => {
        mockBackend.connections.subscribe((connection) => {
          connection.mockRespond(new Response(new ResponseOptions({
            status: 500
          })
          ));
          expect(connection.request.method).toEqual(RequestMethod.Put, 'should return with correct method');
        });
        checklistService.completionStatus().catch(res => { }, error => {
          expect(error.status).toBe(500);
        })
      }));
    it('addCheckList',                                  //Eigth test case begins
      inject([ChecklistService, XHRBackend], (checklistService, mockBackend) => {
        mockBackend.connections.subscribe((connection) => {
          connection.mockRespond(new Response(new ResponseOptions({
            status: 200
          })
          ));
          expect(connection.request.method).toEqual(RequestMethod.Post, 'should return with correct method');
        });
        checklistService.addCheckList({ "taskId": 29, "checklistId": 18, "checklistName": "CSO UI", "status": false })
          .subscribe((res) => { expect(res.status).toBe(200); })
      }));
    it('deleteChecklists',                                  //Ninth test case begins
      inject([ChecklistService, XHRBackend], (checklistService, mockBackend) => {
        mockBackend.connections.subscribe((connection) => {
          connection.mockRespond(new Response(new ResponseOptions({
            status: 200
          })
          ));
          expect(connection.request.method).toEqual(RequestMethod.Delete, 'should return with correct method');
        });
        checklistService.deleteChecklists(18)
          .subscribe((res) => { expect(res.status).toBe(200); })
      }));
    it('deleteChecklistswitherror',                                  //Tenth test case begins
      inject([ChecklistService, XHRBackend], (checklistService, mockBackend) => {
        mockBackend.connections.subscribe((connection) => {
          connection.mockRespond(new Response(new ResponseOptions({
            status: 500
          })
          ));
          expect(connection.request.method).toEqual(RequestMethod.Delete, 'should return with correct method');
        });
        checklistService.deleteChecklists().catch(res => { }, error => {
          expect(error.status).toBe(500);
        })
      }));
  });
});