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
import { ForgetServiceService } from './forget-service.service';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
import { Router } from "@angular/router/router";
describe('ForgetServiceService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({                              /* creates a module that overrides the actual dependencies with testing dependencies*/
      imports: [HttpModule, RouterTestingModule, FormsModule],    //imports being declared before setting the testing environment
      providers: [
        ForgetServiceService,                                      //provider tells the service on which testing is being performed
        { provide: XHRBackend, useClass: MockBackend },
      ]
    });
  });
  describe('forget', () => {
    let service: ForgetServiceService;
    beforeEach(inject([Http, XHRBackend], (http: Http, back: MockBackend,router: Router) => {    /* injecting service and backend dependencies*/
      service = new ForgetServiceService(http,router);
    }));                                               //end of first before each
    it('can instantiate service when inject service',       //First test case begins
      inject([ForgetServiceService], (service: ForgetServiceService) => {
        expect(service instanceof ForgetServiceService).toBe(true);
      }));                                                      //First test case ends
    it('can instantiate service with "new"', inject([Http], (http: Http,router: Router) => {      //Second test case begins
      expect(http).not.toBeNull('http should be provided');
      let service = new ForgetServiceService(http,router);
      expect(service instanceof ForgetServiceService).toBe(true, 'new service should be ok');
    }));                                                                            //Second test case ends
    it('can provide the mockBackend as XHRBackend',                   //Third test case begins
      inject([XHRBackend], (backend: MockBackend) => {
        expect(backend).not.toBeNull('backend should be provided');
      }));
    it('emailto',                                  //Fourth test case begins
      inject([ForgetServiceService, XHRBackend], (forgetServiceService, mockBackend) => {
        mockBackend.connections.subscribe((connection) => {
          connection.mockRespond(new Response(new ResponseOptions({
            status: 200
          })
          ));
          expect(connection.request.method).toEqual(RequestMethod.Post, 'should return with correct method');
        });
        forgetServiceService.emailto()
          .then((res) => { expect(res.status).toBe(200); })
      }));
  })
});