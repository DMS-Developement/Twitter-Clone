import {TestBed} from '@angular/core/testing';

import {LoginRegisterModalFlagService} from './login-register-modal-flag.service';

describe('LoginRegisterModalFlagService', () => {
  let service: LoginRegisterModalFlagService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LoginRegisterModalFlagService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
