import { TestBed } from '@angular/core/testing';

import { PredavacSelectOServiceService } from './predavac-select-o-service.service';

describe('PredavacSelectOServiceService', () => {
  let service: PredavacSelectOServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PredavacSelectOServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
