import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddpredavaconpredavanjeComponent } from './addpredavaconpredavanje.component';

describe('AddpredavaconpredavanjeComponent', () => {
  let component: AddpredavaconpredavanjeComponent;
  let fixture: ComponentFixture<AddpredavaconpredavanjeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddpredavaconpredavanjeComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddpredavaconpredavanjeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
