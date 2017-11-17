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

import { KanbanService } from './kanban.service';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';


import { Burndown } from '../model/burndown';
const mockResponse =                              //Response being mocked for testing
  [
    { "taskId": 29, "actualDate": 12 / 12 / 2017, "expectedDate": 12 / 12 / 2017, "taskName": "ANGULAR" },
    { "taskId": 29, "actualDate": 12 / 12 / 2017, "expectedDate": 12 / 12 / 2017, "taskName": "ANGULAR" },
    { "taskId": 29, "actualDate": 12 / 12 / 2017, "expectedDate": 12 / 12 / 2017, "taskName": "ANGULAR" },
    { "taskId": 29, "actualDate": 12 / 12 / 2017, "expectedDate": 12 / 12 / 2017, "taskName": "ANGULAR" }
  ];
describe('KanbanService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({                              /* creates a module that overrides the actual dependencies with testing dependencies*/
      imports: [HttpModule, RouterTestingModule, FormsModule],    //imports being declared before setting the testing environment
      providers: [
        KanbanService,                                      //provider tells the service on which testing is being performed
        { provide: XHRBackend, useClass: MockBackend },
      ]
    });
  });                                                    //end of first before each
  describe('kanban', () => {              //start of second describe
    let service: KanbanService;
    beforeEach(inject([Http, XHRBackend], (http: Http, back: MockBackend) => {    /* injecting service and backend dependencies*/
      service = new KanbanService(http);
    }));
    it('can instantiate service when inject service',       //First test case begins
      inject([KanbanService], (service: KanbanService) => {
        expect(service instanceof KanbanService).toBe(true);
      }));                                                      //First test case ends
    it('can instantiate service with "new"', inject([Http], (http: Http) => {      //Second test case begins
      expect(http).not.toBeNull('http should be provided');
      let service = new KanbanService(http);
      expect(service instanceof KanbanService).toBe(true, 'new service should be ok');
    }));                                                                            //Second test case ends
    it('can provide the mockBackend as XHRBackend',                   //Third test case begins
      inject([XHRBackend], (backend: MockBackend) => {
        expect(backend).not.toBeNull('backend should be provided');
      }));
    it('getProjectDetails',             //Fourth test case Begins
      inject([KanbanService, XHRBackend], (KanbanService, mockBackend) => {      //injecting the service and backend dependencies
        mockBackend.connections.subscribe((connection) => {          /* setting up connections to Http whenever someone subcribes 
                                                           to an http call */
          connection.mockRespond(new Response(new ResponseOptions({
            body: JSON.stringify(mockResponse)
          })));
        });
        KanbanService.getProjectDetails(1).subscribe((data) => {
          expect(data.length).toBe(4);
          expect(data[0].taskId).toEqual(29);
          expect(data[1].actualDate).toEqual(12 / 12 / 2017);
          expect(data[2].expectedDate).toEqual(12 / 12 / 2017);
          expect(data[3].taskName).toEqual("ANGULAR");
        });
      }));

  })
});
