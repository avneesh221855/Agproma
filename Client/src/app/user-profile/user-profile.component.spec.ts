import { async, ComponentFixture, TestBed ,fakeAsync,tick} from '@angular/core/testing';
import { DebugElement ,NO_ERRORS_SCHEMA}    from '@angular/core';
import {By} from '@angular/platform-browser';
import { UserProfileComponent } from './user-profile.component';
import { LoginService } from '.././shared/services/login.service';
import { FormsModule } from '@angular/forms';
import { HttpModule,Http } from '@angular/http';
import { ActivatedRoute} from '@angular/router';
import { Observable } from 'rxjs'
import { RouterTestingModule } from '@angular/router/testing';
import swal from 'sweetalert2';

describe('UserProfileComponent', () => {
  let component: UserProfileComponent;
  let fixture: ComponentFixture<UserProfileComponent>;
  let spy: jasmine.Spy;
  let spyTransaction:jasmine.Spy;
  let de:DebugElement;
  let el:HTMLElement;
  let MOCKMASTER = {"department":"Bootcamp","email":"aabhaas9413@gmail.com","firstName":"aabhaas","lastName":"malhotra","password":"abc"}
 
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports:[FormsModule,HttpModule,RouterTestingModule],
      declarations: [UserProfileComponent],
      providers: [LoginService,{
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
    fixture = TestBed.createComponent(UserProfileComponent);
    component = fixture.componentInstance;
    var login=fixture.debugElement.injector.get(LoginService);
 
    spy=spyOn(login,'getById').and.returnValue(Observable.of(MOCKMASTER));
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
  it('Display title your info', fakeAsync(() => {
    //query the id head of form
    tick();
    fixture.detectChanges();
    let de = fixture.debugElement.query(By.css('#YourInfo'));
    let el = de.nativeElement;
    expect(el.textContent).toContain("Your Info");
  }));
  it('should show the first name', fakeAsync(() => {
    // wait for fakeAsync getLocationName
    tick();
    fixture.detectChanges();
    let de = fixture.debugElement.query(By.css('#firstName'));
    let el = de.nativeElement;
    expect(el.textContent).toEqual(" AABHAAS");
  }));
  it('should show show the last name', fakeAsync(() => {
    // wait for fakeAsync getLocationName
    tick();
    fixture.detectChanges();
    let de = fixture.debugElement.query(By.css('#lastName'));
    let el = de.nativeElement;
    expect(el.textContent).toEqual(" MALHOTRA");
  }));
  it('should show the email ', fakeAsync(() => {
    // wait for fakeAsync getLocationName
    tick();
    fixture.detectChanges();
    let de = fixture.debugElement.query(By.css('#email'));
    let el = de.nativeElement;
    expect(el.textContent).toEqual(" aabhaas9413@gmail.com");
  }));
  it('should show the department', fakeAsync(() => {
    // wait for fakeAsync getLocationName
    tick();
    fixture.detectChanges();
    let de = fixture.debugElement.query(By.css('#department'));
    let el = de.nativeElement;
    expect(el.textContent).toEqual(" Bootcamp");
  }));
  it('should reset the value for current and new password whren click on close button', fakeAsync(() => {
    fixture.detectChanges();
    component.details.password="123";
    component.str="12345";
    component.newPassword="123"
    fixture.detectChanges();
    tick();  
    let element = fixture.debugElement.queryAll(By.css('#closeFunc'));
    component.resetOnClose();
     tick();
     fixture.detectChanges();
     expect(component.str).toBe("");
     expect(component.newPassword).toBe("");
  
  }));
  it('should reset the value for confirm password whren click on close button', fakeAsync(() => {
    fixture.detectChanges();
    component.details.password="123";
    component.confirmPassword="12345";
    fixture.detectChanges();
    tick();  
    let element = fixture.debugElement.queryAll(By.css('#closeFunc'));
    component.resetOnClose();
     tick();
     fixture.detectChanges();
     expect(component.confirmPassword).toBe("");
    
  }));
})