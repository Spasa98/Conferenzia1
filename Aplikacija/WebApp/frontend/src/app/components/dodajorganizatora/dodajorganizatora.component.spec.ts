import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DodajorganizatoraComponent } from './dodajorganizatora.component';

describe('DodajorganizatoraComponent', () => {
  let component: DodajorganizatoraComponent;
  let fixture: ComponentFixture<DodajorganizatoraComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DodajorganizatoraComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DodajorganizatoraComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
