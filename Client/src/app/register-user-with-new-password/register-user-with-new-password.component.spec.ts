import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisterUserWithNewPasswordComponent } from './register-user-with-new-password.component';

describe('RegisterUserWithNewPasswordComponent', () => {
  let component: RegisterUserWithNewPasswordComponent;
  let fixture: ComponentFixture<RegisterUserWithNewPasswordComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RegisterUserWithNewPasswordComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RegisterUserWithNewPasswordComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
