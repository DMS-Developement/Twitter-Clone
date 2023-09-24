import {Injectable} from '@angular/core';
import {BehaviorSubject} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoginRegisterModalFlagService {

  private showLoginForm = new BehaviorSubject<boolean>(false);
  currentLoginFlag = this.showLoginForm.asObservable()

  private showRegisterForm = new BehaviorSubject<boolean>(false);
  currentRegisterFlag = this.showRegisterForm.asObservable()

  constructor() {
  }

  changeLoginFlag(flag: boolean) {
    this.showLoginForm.next(flag)
  }

  changeRegisterFlag(flag: boolean) {
    this.showRegisterForm.next(flag)
  }
}
