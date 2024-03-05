import { TestBed } from '@angular/core/testing';

import { PredavacPredavanjaService } from './predavac-predavanja.service';

describe('PredavacPredavanjaService', () => {
  let service: PredavacPredavanjaService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PredavacPredavanjaService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
