import { async, ComponentFixture, TestBed, fakeAsync, tick, inject } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { DebugElement } from '@angular/core';
import { By } from '@angular/platform-browser';
import { Observable } from 'rxjs/Observable';
import { Router } from '@angular/router';
import 'rxjs/add/Observable/of';
import { NgxPaginationModule } from 'ngx-pagination';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { HttpModule, Http, Response, ResponseOptions, XHRBackend } from '@angular/http';
import { FormsModule } from '@angular/forms';
import { MockBackend } from '@angular/http/testing';
import { ChecklistService } from '../shared/services/checklist.service';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { ChecklistComponent } from './checklist.component';
describe('ChecklistComponent', () => {
  let component: ChecklistComponent;
  let checklistService: ChecklistService;
  let route:Router;
  let spyaddCheckList: jasmine.Spy;
  let spyonStartComponent: jasmine.Spy;
  let spydeleteChecklist: jasmine.Spy;
  let spymarkCompleted, spymarkCompleted1, spyonStartComponent1: jasmine.Spy;
  let de: DebugElement;
  let el: HTMLElement;
  let fixture: ComponentFixture<ChecklistComponent>;
  beforeEach(async(() => {
    TestBed.configureTestingModule({   //creates a module that overrides the actual dependencies with testing dependencies
      imports: [HttpModule, FormsModule, NgxPaginationModule, //modules imported
        RouterTestingModule],
      declarations: [ChecklistComponent],    //declaring modules being imported
      schemas: [NO_ERRORS_SCHEMA],           //declaring schemas
      providers: [ChecklistService] //providing the real service
    })
      .compileComponents()
  }));
  beforeEach(() => {
    fixture = TestBed.createComponent(ChecklistComponent);
    component = fixture.componentInstance;
    checklistService = fixture.debugElement.injector.get(ChecklistService);  //injecting real service 
    spyaddCheckList = spyOn(checklistService, 'addCheckList') //spying on checklist service on addchecklist method
      .and.returnValue(Observable.of("{status:200}"));
    spyonStartComponent = spyOn(checklistService, 'getCheckList') //spying on checklist service on onstart method
      .and.returnValue(Observable.of(""));
    spyonStartComponent1 = spyOn(checklistService, 'getById') //spying on checklist service on onstart1 method
      .and.returnValue(Observable.of(""));
    spydeleteChecklist = spyOn(checklistService, 'deleteChecklists') //spying on checklist service on deletechecklist method
      .and.returnValue(Observable.of(""));
    spymarkCompleted = spyOn(checklistService, 'completionStatus') //spying on checklist service on markcompleted method
      .and.returnValue(Observable.of(""));
  });
  it('can instantiate service when inject service', //first test case
    inject([ChecklistService], (service: ChecklistService) => {
      expect(service instanceof ChecklistService).toBe(true);
    }));
  it('can instantiate service with "new"', inject([Http], (http: Http) => { //second test case
    expect(http).not.toBeNull('http should be provided');
    let service = new ChecklistService(http,route);
    expect(service instanceof ChecklistService).toBe(true, 'new service should be ok');
  }));
  it('can provide the mockBackend as XHRBackend', //third test case
    inject([XHRBackend], (backend: MockBackend) => {
      expect(backend).not.toBeNull('backend should be provided');
    }));
  it('addChecklist', async(() => { //fourth test case
    fixture.detectChanges();
    fixture.whenStable().then(() => {
      fixture.detectChanges();
      de = fixture.debugElement.query(By.css('.addChecklist'));
      el = de.nativeElement;
      el.click();
      expect(spyaddCheckList.calls.any()).toEqual(true);
    });
  }));
  it('onStartComponent', fakeAsync(() => { //fifth test case
    fixture.detectChanges();
    tick();
    component.onStartComponent();
    fixture.detectChanges();
    expect(spyonStartComponent1.calls.any()).toEqual(true);
    expect(spyonStartComponent.calls.any()).toEqual(true);
  }));
  it('markCompleted is returning true', fakeAsync(() => { //sixth test case
    const mockdata = {
      taskId: null,
      checklistName: '',
      status: false
    };
    fixture.detectChanges();
    fixture.detectChanges();
    component.markCompleted(true, mockdata);
    tick();
    fixture.detectChanges();
    expect(spymarkCompleted.calls.any()).toEqual(true);
  }));
  it('markCompleted is returning false', fakeAsync(() => { //seventh test case
    const mockdata = {
      taskId: null,
      checklistName: '',
      status: false
    };
    fixture.detectChanges();
    fixture.detectChanges();
    component.markCompleted(false, mockdata);
    tick();
    fixture.detectChanges();
    expect(spymarkCompleted.calls.any()).toEqual(true);
  }));
  it('delete is returning true', fakeAsync(() => { //eigth test case
    fixture.detectChanges();
    tick();
    fixture.detectChanges();
    component.deleteChecklist("");
    fixture.detectChanges();
    expect(spydeleteChecklist.calls.any()).toEqual(true);
  }));
});