import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PredavacSelectComponent } from './predavac-select.component';

describe('PredavacSelectComponent', () => {
  let component: PredavacSelectComponent;
  let fixture: ComponentFixture<PredavacSelectComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PredavacSelectComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PredavacSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
