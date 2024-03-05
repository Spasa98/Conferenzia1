import { TestBed } from '@angular/core/testing';

import { ZvanjeOvlastSalaService } from './zvanjeoblastsala.service';

describe('ZvanjeOvlastSalaService', () => {
  let service: ZvanjeOvlastSalaService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ZvanjeOvlastSalaService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
