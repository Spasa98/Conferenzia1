import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PredavaciPocetnaComponent } from './predavaci-pocetna.component';

describe('PredavaciPocetnaComponent', () => {
  let component: PredavaciPocetnaComponent;
  let fixture: ComponentFixture<PredavaciPocetnaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PredavaciPocetnaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PredavaciPocetnaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
