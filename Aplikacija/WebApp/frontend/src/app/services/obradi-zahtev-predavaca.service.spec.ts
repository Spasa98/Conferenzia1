import { TestBed } from '@angular/core/testing';

import { ObradiZahtevPredavacaService } from './obradi-zahtev-predavaca.service';

describe('ObradiZahtevPredavacaService', () => {
  let service: ObradiZahtevPredavacaService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ObradiZahtevPredavacaService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
