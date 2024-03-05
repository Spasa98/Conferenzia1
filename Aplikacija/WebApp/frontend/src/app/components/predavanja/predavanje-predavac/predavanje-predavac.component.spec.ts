import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PredavanjePredavacComponent } from './predavanje-predavac.component';

describe('PredavanjePredavacComponent', () => {
  let component: PredavanjePredavacComponent;
  let fixture: ComponentFixture<PredavanjePredavacComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PredavanjePredavacComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PredavanjePredavacComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
