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
import { Login } from "../model/login";
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
import { LoginService } from './login.service';
import { Router } from "@angular/router/router";

const mockResponse =                              //Response being mocked for testing
	[
		{ "Id": 29, "FirstName": "Testing", "LastName": "hello", "Organization": "ANGULAR" },
		{ "Id": 29, "FirstName": "Testing", "LastName": "hello", "Organization": "ANGULAR" },
		{ "Id": 29, "FirstName": "Testing", "LastName": "hello", "Organization": "ANGULAR" },
		{ "Id": 29, "FirstName": "Testing", "LastName": "hello", "Organization": "ANGULAR" }
	];
describe('LoginService', () => {
	beforeEach(() => {
		TestBed.configureTestingModule({                              /* creates a module that overrides the actual dependencies with testing dependencies*/
			imports: [HttpModule, RouterTestingModule, FormsModule],    //imports being declared before setting the testing environment
			providers: [
				LoginService,                                      //provider tells the service on which testing is being performed
				{ provide: XHRBackend, useClass: MockBackend },
			]
		});
	});                                                    //end of first before each
	describe('projectscreen', () => {              //start of second describe
		let service: LoginService;
		beforeEach(inject([Http, XHRBackend], (http: Http, back: MockBackend, router: Router) => {    /* injecting service and backend dependencies*/
			service = new LoginService(http, router);
		}));
		it('can instantiate service when inject service',       //First test case begins
			inject([LoginService], (service: LoginService) => {
				expect(service instanceof LoginService).toBe(true);
			}));                                                      //First test case ends
		it('can instantiate service with "new"', inject([Http], (http: Http, router: Router) => {      //Second test case begins
			expect(http).not.toBeNull('http should be provided');
			let service = new LoginService(http, router);
			expect(service instanceof LoginService).toBe(true, 'new service should be ok');
		}));                                                                            //Second test case ends
		it('can provide the mockBackend as XHRBackend',                   //Third test case begins
			inject([XHRBackend], (backend: MockBackend) => {
				expect(backend).not.toBeNull('backend should be provided');
			}));
		//Third test case ends
		it('getAll',             //Fourth test case Begins
			inject([LoginService, XHRBackend], (LoginService, mockBackend) => {      //injecting the service and backend dependencies
				mockBackend.connections.subscribe((connection) => {          /* setting up connections to Http whenever someone subcribes 
                                                       to an http call */
					connection.mockRespond(new Response(new ResponseOptions({
						body: JSON.stringify(mockResponse)
					})));
				});
				LoginService.getAll(1).subscribe((data) => {
					expect(data.length).toBe(4);
					expect(data[0].Id).toEqual(29);
					expect(data[1].FirstName).toEqual("Testing");
					expect(data[2].LastName).toEqual("hello");
					expect(data[3].Organization).toEqual("ANGULAR");
				});
			}));
		it('get',             //Fifth test case Begins
			inject([LoginService, XHRBackend], (LoginService, mockBackend) => {      //injecting the service and backend dependencies
				mockBackend.connections.subscribe((connection) => {          /* setting up connections to Http whenever someone subcribes 
                                                     to an http call */
					connection.mockRespond(new Response(new ResponseOptions({
						body: JSON.stringify(mockResponse)
					})));
				});
				LoginService.get(1).subscribe((data) => {
					let response: Array<any> = data.json();
					expect(response.length).toBe(4);
					expect(response[0].Id).toEqual(29);
					expect(response[0].FirstName).toEqual("Testing");
					expect(response[0].LastName).toEqual("hello");
					expect(response[0].Organization).toEqual("ANGULAR");
				});
			}));
		it('getById',             //Sixth test case Begins
			inject([LoginService, XHRBackend], (LoginService, mockBackend) => {      //injecting the service and backend dependencies
				mockBackend.connections.subscribe((connection) => {          /* setting up connections to Http whenever someone subcribes 
                                                         to an http call */
					connection.mockRespond(new Response(new ResponseOptions({
						body: JSON.stringify(mockResponse)
					})));
				});
				LoginService.getById(1).subscribe((data) => {
					expect(data.length).toBe(4);
					expect(data[0].Id).toEqual(29);
					expect(data[1].FirstName).toEqual("Testing");
					expect(data[2].LastName).toEqual("hello");
					expect(data[3].Organization).toEqual("ANGULAR");
				});
			}));
		it('getUserData',             //Seventh test case Begins
			inject([LoginService, XHRBackend], (LoginService, mockBackend) => {      //injecting the service and backend dependencies
				mockBackend.connections.subscribe((connection) => {          /* setting up connections to Http whenever someone subcribes 
                                                       to an http call */
					connection.mockRespond(new Response(new ResponseOptions({
						body: JSON.stringify(mockResponse)
					})));
				});
				LoginService.getUserData(1).subscribe((data) => {
					let response: Array<any> = data.json();
					expect(response.length).toBe(4);
					expect(response[0].Id).toEqual(29);
					expect(response[0].FirstName).toEqual("Testing");
					expect(response[0].LastName).toEqual("hello");
					expect(response[0].Organization).toEqual("ANGULAR");
				});
			}));

		it('logOut',                                  //Eigth test case begins
			inject([LoginService, XHRBackend], (loginService, mockBackend) => {
				mockBackend.connections.subscribe((connection) => {
					connection.mockRespond(new Response(new ResponseOptions({
						status: 200
					})
					));
					expect(connection.request.method).toEqual(RequestMethod.Post, 'should return with correct method');
				});
				loginService.logOut({ "Id": 29, "FirstName": "Testing", "LastName": "hello", "Organization": "ANGULAR" })
					.then((res) => { expect(res.status).toBe(200); })
			}));

		it('check',                                  //Ninth test case begins
			inject([LoginService, XHRBackend], (loginService, mockBackend) => {
				mockBackend.connections.subscribe((connection) => {
					connection.mockRespond(new Response(new ResponseOptions({
						status: 200
					})
					));
					expect(connection.request.method).toEqual(RequestMethod.Post, 'should return with correct method');
				});
				loginService.check({ "Id": 29, "FirstName": "Testing", "LastName": "hello", "Organization": "ANGULAR" })
					.then((res) => { expect(res.status).toBe(200); })
			}));

		it('getToken',                                  //Tenth test case begins
			inject([LoginService, XHRBackend], (loginService, mockBackend) => {
				mockBackend.connections.subscribe((connection) => {
					connection.mockRespond(new Response(new ResponseOptions({
						status: 200
					})
					));
					expect(connection.request.method).toEqual(RequestMethod.Post, 'should return with correct method');
				});
				loginService.getToken({ "Id": 29, "FirstName": "Testing", "LastName": "hello", "Organization": "ANGULAR" })
					.then((res) => { expect(res.status).toBe(200); })
			}));

		it('getTokenForFbandGoogle',                                  //Eleventh test case begins
			inject([LoginService, XHRBackend], (loginService, mockBackend) => {
				mockBackend.connections.subscribe((connection) => {
					connection.mockRespond(new Response(new ResponseOptions({
						status: 200
					})
					));
					expect(connection.request.method).toEqual(RequestMethod.Post, 'should return with correct method');
				});
				loginService.getTokenForFbandGoogle({ "Id": 29, "FirstName": "Testing", "LastName": "hello", "Organization": "ANGULAR" })
					.then((res) => { expect(res.status).toBe(200); })
			}));


		it('postLoginDetails',                                  //Twelfth test case begins
			inject([LoginService, XHRBackend], (loginService, mockBackend) => {
				mockBackend.connections.subscribe((connection) => {
					connection.mockRespond(new Response(new ResponseOptions({
						status: 200
					})
					));
					expect(connection.request.method).toEqual(RequestMethod.Post, 'should return with correct method');
				});
				loginService.postLoginDetails({ "Id": 29, "FirstName": "Testing", "LastName": "hello", "Organization": "ANGULAR" })
					.then((res) => { expect(res.status).toBe(200); })
			}));

		it('postMemberDetails',                                  //Thirteenth test case begins
			inject([LoginService, XHRBackend], (loginService, mockBackend) => {
				mockBackend.connections.subscribe((connection) => {
					connection.mockRespond(new Response(new ResponseOptions({
						status: 200
					})
					));
					expect(connection.request.method).toEqual(RequestMethod.Post, 'should return with correct method');
				});
				loginService.postMemberDetails({ "Id": 29, "FirstName": "Testing", "LastName": "hello", "Organization": "ANGULAR" })
					.then((res) => { expect(res.status).toBe(200); })
			}));

		it('updatePassword',                                  //Fourteenth test case begins
			inject([LoginService, XHRBackend], (loginService, mockBackend) => {
				mockBackend.connections.subscribe((connection) => {
					connection.mockRespond(new Response(new ResponseOptions({
						status: 200
					})
					));
					expect(connection.request.method).toEqual(RequestMethod.Put, 'should return with correct method');
				});
				loginService.updatePassword(18, { "leaderId": 29, "name": "Testing", "projectDescription": "hello", "technologyUsed": "ANGULAR" })
					.subscribe((res) => { expect(res.status).toBe(200); })
			}));
	});
});