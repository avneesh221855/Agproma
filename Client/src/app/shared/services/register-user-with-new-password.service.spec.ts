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
import { RegisterUserWithNewPasswordService } from './register-user-with-new-password.service';
import { Master } from "../model/master";
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
import { Router } from "@angular/router/router";
const mockResponse =                              //Response being mocked for testing
  [
    { "id": 29, "firstName": "Lakshya", "lastName": "Lakshya1", "email": "Lakshya5", "password": "Lakshya9", "organization": "Niit", "department": "Hr" },
    { "id": 30, "firstName": "Lakshya", "lastName": "Lakshya2", "email": "Lakshya6", "password": "Lakshya10", "organization": "Niit", "department": "Hr" },
    { "id": 31, "firstName": "Lakshya", "lastName": "Lakshya3", "email": "Lakshya7", "password": "Lakshya11", "organization": "Niit", "department": "Hr" },
    { "id": 32, "firstName": "Lakshya", "lastName": "Lakshya4", "email": "Lakshya8", "password": "Lakshya12", "organization": "Niit", "department": "Hr" }
  ];
describe('RegisterUserWithNewPasswordService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({                              /* creates a module that overrides the actual dependencies with testing dependencies*/
      imports: [HttpModule, RouterTestingModule, FormsModule],    //imports being declared before setting the testing environment
      providers: [
        RegisterUserWithNewPasswordService,                                      //provider tells the service on which testing is being performed
        { provide: XHRBackend, useClass: MockBackend },
      ]
    });
  });
  //start of second describe
  describe('register', () => {
    let service: RegisterUserWithNewPasswordService;
    beforeEach(inject([Http, XHRBackend], (http: Http, back: MockBackend,router: Router) => {    /* injecting service and backend dependencies*/
      service = new RegisterUserWithNewPasswordService(http,router);
    }));                                               //end of first before each
    it('can instantiate service when inject service',       //First test case begins
      inject([RegisterUserWithNewPasswordService], (service: RegisterUserWithNewPasswordService) => {
        expect(service instanceof RegisterUserWithNewPasswordService).toBe(true);
      }));                                                      //First test case ends
    it('can instantiate service with "new"', inject([Http], (http: Http,router: Router) => {      //Second test case begins
      expect(http).not.toBeNull('http should be provided');
      let service = new RegisterUserWithNewPasswordService(http,router);
      expect(service instanceof RegisterUserWithNewPasswordService).toBe(true, 'new service should be ok');
    }));                                                                            //Second test case ends
    it('can provide the mockBackend as XHRBackend',                   //Third test case begins
      inject([XHRBackend], (backend: MockBackend) => {
        expect(backend).not.toBeNull('backend should be provided');
      }));
    it('getUserDetails',             //First test case Begins
      inject([RegisterUserWithNewPasswordService, XHRBackend], (RegisterUserWithNewPasswordService, mockBackend) => {      //injecting the service and backend dependencies
        mockBackend.connections.subscribe((connection) => {          /* setting up connections to Http whenever someone subcribes 
                                                               to an http call */
          connection.mockRespond(new Response(new ResponseOptions({
            body: JSON.stringify(mockResponse)
          })));
        });
        RegisterUserWithNewPasswordService.getUserDetails(29).subscribe((data) => {
          let response: Array<any> = data.json();
          expect(response.length).toBe(4);
          expect(response[0].id).toEqual(29);
          expect(response[0].firstName).toEqual("Lakshya");
          expect(response[0].lastName).toEqual("Lakshya1");
          expect(response[0].email).toEqual("Lakshya5");
          expect(response[0].password).toEqual("Lakshya9");
          expect(response[0].organization).toEqual("Niit");
          expect(response[0].department).toEqual("Hr");
        });
      }));
    it('updatePassword',                                  //Second test case begins
      inject([RegisterUserWithNewPasswordService, XHRBackend], (registerUserWithNewPasswordService, mockBackend) => {
        mockBackend.connections.subscribe((connection) => {
          connection.mockRespond(new Response(new ResponseOptions({
            status: 200
          })
          ));
          expect(connection.request.method).toEqual(RequestMethod.Put, 'should return with correct method');
        });
        registerUserWithNewPasswordService.updatePassword({ "id": 29, "firstName": "Lakshya", "lastName": "Lakshya1", "email": "Lakshya5", "password": "Lakshya9", "organization": "Niit", "department": "Hr" })
          .then((res) => { expect(res.status).toBe(200); })
      }));

      
  })
});