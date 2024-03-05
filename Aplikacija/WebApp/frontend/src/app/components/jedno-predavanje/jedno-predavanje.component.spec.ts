import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JednoPredavanjeComponent } from './jedno-predavanje.component';

describe('JednoPredavanjeComponent', () => {
  let component: JednoPredavanjeComponent;
  let fixture: ComponentFixture<JednoPredavanjeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ JednoPredavanjeComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(JednoPredavanjeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
