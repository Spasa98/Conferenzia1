import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SlusalacPredavanjaComponent } from './slusalac-predavanja.component';

describe('SlusalacPredavanjaComponent', () => {
  let component: SlusalacPredavanjaComponent;
  let fixture: ComponentFixture<SlusalacPredavanjaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SlusalacPredavanjaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SlusalacPredavanjaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
