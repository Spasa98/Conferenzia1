import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PredavanjaTabelaComponent } from './predavanja-tabela.component';

describe('PredavanjaTabelaComponent', () => {
  let component: PredavanjaTabelaComponent;
  let fixture: ComponentFixture<PredavanjaTabelaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PredavanjaTabelaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PredavanjaTabelaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
