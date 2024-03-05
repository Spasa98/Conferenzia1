import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ZahtevSlusalacTabelaComponent } from './zahtev-slusalac-tabela.component';

describe('ZahtevSlusalacTabelaComponent', () => {
  let component: ZahtevSlusalacTabelaComponent;
  let fixture: ComponentFixture<ZahtevSlusalacTabelaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ZahtevSlusalacTabelaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ZahtevSlusalacTabelaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
