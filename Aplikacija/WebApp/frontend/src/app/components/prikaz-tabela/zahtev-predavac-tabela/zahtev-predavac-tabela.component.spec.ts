import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ZahtevPredavacTabelaComponent } from './zahtev-predavac-tabela.component';

describe('ZahtevPredavacTabelaComponent', () => {
  let component: ZahtevPredavacTabelaComponent;
  let fixture: ComponentFixture<ZahtevPredavacTabelaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ZahtevPredavacTabelaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ZahtevPredavacTabelaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
