import {Component, OnInit} from '@angular/core';
import {animate, state, style, transition, trigger} from "@angular/animations";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {LoginRegisterModalFlagService} from "../../login-register-modal-flag.service";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  animations: [
    trigger('fadeIn', [
      state('void', style({
        opacity: 0
      })),
      transition('void <=> *', animate(100)),
    ])
  ]
})
export class LoginComponent implements OnInit {

  loginForm!: FormGroup;

  showLoginForm: boolean = false

  constructor(private formBuilder: FormBuilder, private loginFlag: LoginRegisterModalFlagService) {
  }

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      'username': ['', Validators.required],
      'password': ['', [Validators.required, Validators.minLength(6)]]
    });

    this.loginFlag.currentLoginFlag.subscribe(flag => this.showLoginForm = flag);
  }

  onSubmit() {
    if (this.loginForm.valid) {
      const username = this.loginForm.get('username')?.value;
      const password = this.loginForm.get('password')?.value;
    }
  }

  closeLoginForm() {
    this.loginFlag.changeLoginFlag(false);
  }
}
