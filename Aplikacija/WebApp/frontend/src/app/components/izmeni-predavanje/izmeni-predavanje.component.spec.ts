import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IzmeniPredavanjeComponent } from './izmeni-predavanje.component';

describe('IzmeniPredavanjeComponent', () => {
  let component: IzmeniPredavanjeComponent;
  let fixture: ComponentFixture<IzmeniPredavanjeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IzmeniPredavanjeComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IzmeniPredavanjeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
