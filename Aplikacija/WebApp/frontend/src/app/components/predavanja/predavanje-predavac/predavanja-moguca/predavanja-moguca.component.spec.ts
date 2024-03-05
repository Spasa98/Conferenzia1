import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PredavanjaMogucaComponent } from './predavanja-moguca.component';

describe('PredavanjaMogucaComponent', () => {
  let component: PredavanjaMogucaComponent;
  let fixture: ComponentFixture<PredavanjaMogucaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PredavanjaMogucaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PredavanjaMogucaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
