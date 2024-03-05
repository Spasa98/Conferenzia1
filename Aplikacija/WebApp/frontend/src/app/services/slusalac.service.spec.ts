import { TestBed } from '@angular/core/testing';

import { SlusalacService } from './slusalac.service';

describe('SlusalacService', () => {
  let service: SlusalacService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SlusalacService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
