import {TestBed} from '@angular/core/testing';

import {ProfilePhotoSaverService} from './profile-photo-saver.service';

describe('ProfilePhotoSaverService', () => {
  let service: ProfilePhotoSaverService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ProfilePhotoSaverService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
