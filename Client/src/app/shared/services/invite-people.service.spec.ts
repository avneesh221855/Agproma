import { TestBed, async, inject } from '@angular/core/testing';
import { } from '@angular/core/testing';
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
import { InvitePeopleService } from './invite-people.service';
import { Members } from "../model/members";
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
import { Router } from "@angular/router/router";
const mockResponse =                              //Response being mocked for testing
  [
    { "TeamId": 29, "MemberId": 33, "Id": 37, "MemberName": "Lakshya" },
    { "TeamId": 30, "MemberId": 34, "Id": 38, "MemberName": "Param" },
    { "TeamId": 31, "MemberId": 35, "Id": 39, "MemberName": "Akash" },
    { "TeamId": 32, "MemberId": 36, "Id": 40, "MemberName": "Akshay" }
  ];
describe('InvitePeopleService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({                              /* creates a module that overrides the actual dependencies with testing dependencies*/
      imports: [HttpModule, RouterTestingModule, FormsModule],    //imports being declared before setting the testing environment
      providers: [
        InvitePeopleService,                                      //provider tells the service on which testing is being performed
        { provide: XHRBackend, useClass: MockBackend },
      ]
    });
  });
  //start of second describe
  describe('invite', () => {
    let service: InvitePeopleService;
    beforeEach(inject([Http, XHRBackend], (http: Http, back: MockBackend, router: Router) => {    /* injecting service and backend dependencies*/
      service = new InvitePeopleService(http, router);
    }));                                               //end of first before each
    it('can instantiate service when inject service',       //First test case begins
      inject([InvitePeopleService], (service: InvitePeopleService) => {
        expect(service instanceof InvitePeopleService).toBe(true);
      }));                                                      //First test case ends
    it('can instantiate service with "new"', inject([Http], (http: Http, router: Router) => {      //Second test case begins
      expect(http).not.toBeNull('http should be provided');
      let service = new InvitePeopleService(http, router);
      expect(service instanceof InvitePeopleService).toBe(true, 'new service should be ok');
    }));                                                                            //Second test case ends
    it('can provide the mockBackend as XHRBackend',                   //Third test case begins
      inject([XHRBackend], (backend: MockBackend) => {
        expect(backend).not.toBeNull('backend should be provided');
      }));
    it('emailto',                                  //Fourth test case begins
      inject([InvitePeopleService, XHRBackend], (invitePeopleService, mockBackend) => {
        mockBackend.connections.subscribe((connection) => {
          connection.mockRespond(new Response(new ResponseOptions({
            status: 200
          })
          ));
          expect(connection.request.method).toEqual(RequestMethod.Post, 'should return with correct method');
        });
        invitePeopleService.emailto({ "TeamId": 29, "MemberId": 33, "Id": 37, "MemberName": "Lakshya" })
          .then((res) => { expect(res.status).toBe(200); })
      }));

    it('getAll',             //Fourth test case Begins
      inject([InvitePeopleService, XHRBackend], (InvitePeopleService, mockBackend) => {      //injecting the service and backend dependencies
        mockBackend.connections.subscribe((connection) => {          /* setting up connections to Http whenever someone subcribes 
                                                           to an http call */
          connection.mockRespond(new Response(new ResponseOptions({
            body: JSON.stringify(mockResponse)
          })));
        });
        InvitePeopleService.getAll(1).subscribe((data) => {
          expect(data.length).toBe(4);
          expect(data[0].TeamId).toEqual(29);
          expect(data[1].MemberId).toEqual(34);
          expect(data[2].Id).toEqual(39);
          expect(data[3].MemberName).toEqual("Akshay");
        });
      }));
  })
});
