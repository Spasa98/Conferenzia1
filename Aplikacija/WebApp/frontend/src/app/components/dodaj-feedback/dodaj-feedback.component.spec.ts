import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DodajFeedbackComponent } from './dodaj-feedback.component';

describe('DodajFeedbackComponent', () => {
  let component: DodajFeedbackComponent;
  let fixture: ComponentFixture<DodajFeedbackComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DodajFeedbackComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DodajFeedbackComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
