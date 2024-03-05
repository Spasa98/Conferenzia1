import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PredavanjaSlusalacMogucaComponent } from './predavanja-slusalac-moguca.component';

describe('PredavanjaSlusalacMogucaComponent', () => {
  let component: PredavanjaSlusalacMogucaComponent;
  let fixture: ComponentFixture<PredavanjaSlusalacMogucaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PredavanjaSlusalacMogucaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PredavanjaSlusalacMogucaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
