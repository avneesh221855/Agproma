import { TestBed, async, inject } from '@angular/core/testing';
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
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
import { Router } from "@angular/router/router";
import { BurndownService } from './burndown.service';
import { Burndown } from '../model/burndown';
const mockResponse =                              //Response being mocked for testing
  [
    { "taskId": 29, "actualDate": 12/12/2017, "expectedDate": 12/12/2017, "taskName": "ANGULAR" },
    { "taskId": 29, "actualDate": 12/12/2017, "expectedDate": 12/12/2017, "taskName": "ANGULAR" },
    { "taskId": 29, "actualDate": 12/12/2017, "expectedDate": 12/12/2017, "taskName": "ANGULAR" },
    { "taskId": 29, "actualDate": 12/12/2017, "expectedDate": 12/12/2017, "taskName": "ANGULAR" }
  ];
describe('BurndownService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({                              /* creates a module that overrides the actual dependencies with testing dependencies*/
      imports: [HttpModule, RouterTestingModule, FormsModule],    //imports being declared before setting the testing environment
      providers: [
        BurndownService,                                      //provider tells the service on which testing is being performed
        { provide: XHRBackend, useClass: MockBackend },
      ]
    });
  });                                                    //end of first before each
  describe('burndown', () => {              //start of second describe
    let service: BurndownService;
    beforeEach(inject([Http, XHRBackend], (http: Http, back: MockBackend) => {    /* injecting service and backend dependencies*/
      service = new BurndownService(http);
    }));
    it('can instantiate service when inject service',       //First test case begins
      inject([BurndownService], (service: BurndownService) => {
        expect(service instanceof BurndownService).toBe(true);
      }));                                                      //First test case ends
    it('can instantiate service with "new"', inject([Http], (http: Http) => {      //Second test case begins
      expect(http).not.toBeNull('http should be provided');
      let service = new BurndownService(http);
      expect(service instanceof BurndownService).toBe(true, 'new service should be ok');
    }));                                                                            //Second test case ends
    it('can provide the mockBackend as XHRBackend',                   //Third test case begins
      inject([XHRBackend], (backend: MockBackend) => {
        expect(backend).not.toBeNull('backend should be provided');
      }));
    //Third test case ends
    
    it('getTaskTimes',             //Fourth test case Begins
    inject([BurndownService, XHRBackend], (BurndownService, mockBackend) => {      //injecting the service and backend dependencies
      mockBackend.connections.subscribe((connection) => {          /* setting up connections to Http whenever someone subcribes 
                                                         to an http call */
        connection.mockRespond(new Response(new ResponseOptions({
          body: JSON.stringify(mockResponse)
        })));
      });
      BurndownService.getTaskTimes(1).subscribe((data) => {
        expect(data.length).toBe(4);
        expect(data[0].taskId).toEqual(29);
        expect(data[1].actualDate).toEqual(12/12/2017);
        expect(data[2].expectedDate).toEqual(12/12/2017);
        expect(data[3].taskName).toEqual("ANGULAR");
      });
    }));
  });
});
