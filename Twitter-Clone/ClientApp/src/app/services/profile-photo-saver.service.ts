import {Injectable} from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ProfilePhotoSaverService {

  constructor() {
  }

  saveProfilePhotoToSessionStorage(key: string, value: Blob): void {
    let reader = new FileReader();
    reader.addEventListener("load", () => {
      sessionStorage.setItem(key, reader.result as string);
    }, false);

    if (value) {
      reader.readAsDataURL(value);
    }
  }

  getProfilePhotoFromSessionStorage(key: string): string {
    return sessionStorage.getItem(key) as string;
  }
}
