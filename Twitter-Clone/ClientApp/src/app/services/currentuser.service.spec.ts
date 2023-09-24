import {TestBed} from '@angular/core/testing';

import {CurrentUserService} from './currentUser.service';

describe('CurrentuserService', () => {
  let service: CurrentUserService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CurrentUserService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
