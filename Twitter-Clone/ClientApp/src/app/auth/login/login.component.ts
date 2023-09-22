import {Component, OnInit} from '@angular/core';
import {animate, state, style, transition, trigger} from "@angular/animations";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {LoginRegisterModalFlagService} from "../../login-register-modal-flag.service";
import {catchError, tap} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";

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

  constructor(private formBuilder: FormBuilder, private loginFlag: LoginRegisterModalFlagService, private http: HttpClient, private router: Router) {
  }

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      'username': ['', Validators.required],
      'password': ['', [Validators.required, Validators.minLength(6)]]
    });

    this.loginFlag.currentLoginFlag.subscribe(flag => this.showLoginForm = flag);
  }

  onSubmit() {

    const user = {
      username: '',
      password: ''
    };

    if (this.loginForm.valid) {
      user.username = this.loginForm.get('username')?.value;
      user.password = this.loginForm.get('password')?.value;
    }

    this.http.post('https://localhost:7282/api/Auth/login', user).pipe(
        tap(response => {
          console.log(response);
          this.closeLoginForm();
          this.router.navigate(['']);
        }),
        catchError(error => {
          const loginErrorMessage = "Invalid username or password."
          window.alert(loginErrorMessage);
          return error;
        }),
    ).subscribe();
  }

  closeLoginForm() {
    this.loginFlag.changeLoginFlag(false);
  }
}
