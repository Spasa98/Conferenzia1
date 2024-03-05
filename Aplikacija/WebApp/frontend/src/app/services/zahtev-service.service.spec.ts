import { TestBed } from '@angular/core/testing';

import { ZahtevServiceService } from './zahtev-service.service';

describe('ZahtevServiceService', () => {
  let service: ZahtevServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ZahtevServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
