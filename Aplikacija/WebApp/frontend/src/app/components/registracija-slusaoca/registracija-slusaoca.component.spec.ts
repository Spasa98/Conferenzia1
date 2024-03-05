import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegistracijaSlusaocaComponent } from './registracija-slusaoca.component';

describe('RegistracijaSlusaocaComponent', () => {
  let component: RegistracijaSlusaocaComponent;
  let fixture: ComponentFixture<RegistracijaSlusaocaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RegistracijaSlusaocaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RegistracijaSlusaocaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
