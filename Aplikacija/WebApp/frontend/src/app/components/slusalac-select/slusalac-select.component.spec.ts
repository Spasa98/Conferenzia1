import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SlusalacSelectComponent } from './slusalac-select.component';

describe('SlusalacSelectComponent', () => {
  let component: SlusalacSelectComponent;
  let fixture: ComponentFixture<SlusalacSelectComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SlusalacSelectComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SlusalacSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
