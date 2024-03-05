import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrganizatorReportFeedbackComponent } from './organizator-report-feedback.component';

describe('OrganizatorReportFeedbackComponent', () => {
  let component: OrganizatorReportFeedbackComponent;
  let fixture: ComponentFixture<OrganizatorReportFeedbackComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OrganizatorReportFeedbackComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OrganizatorReportFeedbackComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
