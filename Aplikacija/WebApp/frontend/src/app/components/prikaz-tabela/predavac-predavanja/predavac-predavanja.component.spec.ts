import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PredavacPredavanjaComponent } from './predavac-predavanja.component';

describe('PredavacPredavanjaComponent', () => {
  let component: PredavacPredavanjaComponent;
  let fixture: ComponentFixture<PredavacPredavanjaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PredavacPredavanjaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PredavacPredavanjaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
