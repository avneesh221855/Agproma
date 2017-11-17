import { async, ComponentFixture, TestBed ,fakeAsync,tick} from '@angular/core/testing';
import { DebugElement ,NO_ERRORS_SCHEMA}    from '@angular/core';
import {By} from '@angular/platform-browser';
import { TaskAssignComponent } from './taskAssign.component';
import { FormsModule } from '@angular/forms';
import { HttpModule,Http } from '@angular/http';
import {Router} from '@angular/router';
import { Observable } from 'rxjs'
import { RouterTestingModule } from '@angular/router/testing';
import { ActivatedRoute} from '@angular/router';

describe('UserProfileComponent', () => {
 let component: TaskAssignComponent;
 let fixture: ComponentFixture<TaskAssignComponent>;
 let spy: jasmine.Spy;
 let spyTransaction:jasmine.Spy;
 let de:DebugElement;
 let el:HTMLElement;
 let MOCKMEMBER = {"TeamId":1,"MemberId":1,"MemberName":"aabhaas"}

 beforeEach(async(() => {
   TestBed.configureTestingModule({
     imports:[FormsModule,HttpModule,RouterTestingModule],
     declarations: [TaskAssignComponent],
     providers: [ {
       provide: ActivatedRoute,
       useValue: {
         params: Observable.from([{id: 1}]),
       },
     }],
     schemas:[NO_ERRORS_SCHEMA]
   })
     .compileComponents();
 }));

 beforeEach(() => {
   fixture = TestBed.createComponent(TaskAssignComponent);
   component = fixture.componentInstance;
  
 
   fixture.detectChanges();
 });

 it('should be created', () => {
   expect(component).toBeTruthy();
 });
 it('should return value true when projectId equals memberId', fakeAsync(() => {
   fixture.detectChanges();
   var el =component.compareTask(1,1);
   fixture.detectChanges();
    tick();  
   expect(el).toBe(true);
   //  expect(component.newPassword).toBe("");
 
 }));
 it('should return value true when projectId equals memberId', fakeAsync(() => {
   fixture.detectChanges();
   var el =component.compareTask(1,2);
   fixture.detectChanges();
    tick();  
   expect(el).toBe(false);
   //  expect(component.newPassword).toBe("");
 
 }));
 it('Display title your info', fakeAsync(() => {
   //query the id head of form
   tick();
   fixture.detectChanges();
   let de = fixture.debugElement.query(By.css('.text-center'));
   let el = de.nativeElement;
   expect(el.textContent).toContain("TASKS");
 }));
})