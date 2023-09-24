import {TestBed} from '@angular/core/testing';

import {TurnOnOrOffActiveIconService} from './turn-on-or-off-active-icon.service';

describe('TurnOnOrOffActiveIconService', () => {
  let service: TurnOnOrOffActiveIconService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TurnOnOrOffActiveIconService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
