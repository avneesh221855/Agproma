import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DashboardRoleComponent } from './dashboard-role.component';

describe('DashboardRoleComponent', () => {
  let component: DashboardRoleComponent;
  let fixture: ComponentFixture<DashboardRoleComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DashboardRoleComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DashboardRoleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
