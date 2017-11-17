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
import { ProjectMaster } from "../model/ProjectMaster";
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
import { ProjectScreenService } from './project-screen.service';

const mockResponse =                              //Response being mocked for testing
  [
    { "leaderId": 29, "name": "Testing", "projectDescription": "hello", "technologyUsed": "ANGULAR" },
    { "leaderId": 29, "name": "Testing", "projectDescription": "hello", "technologyUsed": "ANGULAR" },
    { "leaderId": 29, "name": "Testing", "projectDescription": "hello", "technologyUsed": "ANGULAR" },
    { "leaderId": 29, "name": "Testing", "projectDescription": "hello", "technologyUsed": "ANGULAR" }
  ];
describe('ProjectScreenService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({                              /* creates a module that overrides the actual dependencies with testing dependencies*/
      imports: [HttpModule, RouterTestingModule, FormsModule],    //imports being declared before setting the testing environment
      providers: [
        ProjectScreenService,                                      //provider tells the service on which testing is being performed
        { provide: XHRBackend, useClass: MockBackend },
      ]
    });
  });                                                    //end of first before each
  describe('projectscreen', () => {              //start of second describe
    let service: ProjectScreenService;
    beforeEach(inject([Http, XHRBackend], (http: Http, back: MockBackend) => {    /* injecting service and backend dependencies*/
      service = new ProjectScreenService(http);
    }));
    it('can instantiate service when inject service',       //First test case begins
      inject([ProjectScreenService], (service: ProjectScreenService) => {
        expect(service instanceof ProjectScreenService).toBe(true);
      }));                                                      //First test case ends
    it('can instantiate service with "new"', inject([Http], (http: Http) => {      //Second test case begins
      expect(http).not.toBeNull('http should be provided');
      let service = new ProjectScreenService(http);
      expect(service instanceof ProjectScreenService).toBe(true, 'new service should be ok');
    }));                                                                            //Second test case ends
    it('can provide the mockBackend as XHRBackend',                   //Third test case begins
      inject([XHRBackend], (backend: MockBackend) => {
        expect(backend).not.toBeNull('backend should be provided');
      }));
    //Third test case ends

    it('getAllProjectOfEmployee',             //Fourth test case Begins
      inject([ProjectScreenService, XHRBackend], (ProjectScreenService, mockBackend) => {      //injecting the service and backend dependencies
        mockBackend.connections.subscribe((connection) => {          /* setting up connections to Http whenever someone subcribes 
                                                           to an http call */
          connection.mockRespond(new Response(new ResponseOptions({
            body: JSON.stringify(mockResponse)
          })));
        });
        ProjectScreenService.getAllProjectOfEmployee(1).then((data) => {
          expect(data.length).toBe(4);
          expect(data[0].leaderId).toEqual(29);
          expect(data[1].name).toEqual("Testing");
          expect(data[2].projectDescription).toEqual("hello");
          expect(data[3].technologyUsed).toEqual("ANGULAR");
        });
      }));

    it('getProject',             //Fourth test case Begins
      inject([ProjectScreenService, XHRBackend], (ProjectScreenService, mockBackend) => {      //injecting the service and backend dependencies
        mockBackend.connections.subscribe((connection) => {          /* setting up connections to Http whenever someone subcribes 
                                                           to an http call */
          connection.mockRespond(new Response(new ResponseOptions({
            body: JSON.stringify(mockResponse)
          })));
        });
        ProjectScreenService.getProject(1).then((data) => {
          let response: Array<any> = data.json();
          expect(response.length).toBe(4);
          expect(response[0].leaderId).toEqual(29);
          expect(response[0].name).toEqual("Testing");
          expect(response[0].projectDescription).toEqual("hello");
          expect(response[0].technologyUsed).toEqual("ANGULAR");
        });
      }));

    it('addNewProject',                                  //Eigth test case begins
      inject([ProjectScreenService, XHRBackend], (projectScreenService, mockBackend) => {
        mockBackend.connections.subscribe((connection) => {
          connection.mockRespond(new Response(new ResponseOptions({
            status: 200
          })
          ));
          expect(connection.request.method).toEqual(RequestMethod.Post, 'should return with correct method');
        });
        projectScreenService.addNewProject({ "leaderId": 29, "name": "Testing", "projectDescription": "hello", "technologyUsed": "ANGULAR" })
          .then((res) => { expect(res.status).toBe(200); })
      }));

    it('deleteProject',                                  //Ninth test case begins
      inject([ProjectScreenService, XHRBackend], (projectScreenService, mockBackend) => {
        mockBackend.connections.subscribe((connection) => {
          connection.mockRespond(new Response(new ResponseOptions({
            status: 200
          })
          ));
          expect(connection.request.method).toEqual(RequestMethod.Delete, 'should return with correct method');
        });
        projectScreenService.deleteProject(18)
          .then((res) => { expect(res.status).toBe(200); })
      }));

    it('updateProject',                                  //SIXTH test case begins
      inject([ProjectScreenService, XHRBackend], (projectScreenService, mockBackend) => {
        mockBackend.connections.subscribe((connection) => {
          connection.mockRespond(new Response(new ResponseOptions({
            status: 200
          })
          ));
          expect(connection.request.method).toEqual(RequestMethod.Put, 'should return with correct method');
        });
        projectScreenService.updateProject(18, { "leaderId": 29, "name": "Testing", "projectDescription": "hello", "technologyUsed": "ANGULAR" })
          .then((res) => { expect(res.status).toBe(200); })
      }));
  })
});
