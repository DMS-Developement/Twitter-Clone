import {Injectable} from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CurrentUserService {

  constructor() {
  }

  public setItem(key: string, value: any): void {
    sessionStorage.setItem(key, JSON.stringify(value));
  }

  public getItem(key: string): any {
    const value = sessionStorage.getItem(key);
    return value ? JSON.parse(value) : null;
  }

  public clear(): void {
    sessionStorage.clear();
  }
}
