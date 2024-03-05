import { TestBed } from '@angular/core/testing';

import { PredavanjaServiceService } from './predavanja-service.service';

describe('PredavanjaServiceService', () => {
  let service: PredavanjaServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PredavanjaServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
