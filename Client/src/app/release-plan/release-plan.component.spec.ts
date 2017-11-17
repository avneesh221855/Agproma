import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReleasePlanComponent } from './release-plan.component';

describe('ReleasePlanComponent', () => {
  let component: ReleasePlanComponent;
  let fixture: ComponentFixture<ReleasePlanComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReleasePlanComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReleasePlanComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
