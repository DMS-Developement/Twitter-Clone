import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {LoginRegisterModalFlagService} from "../../login-register-modal-flag.service";
import {animate, state, style, transition, trigger} from "@angular/animations";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {catchError, tap} from "rxjs";

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.css'],
    animations: [
        trigger('fadeIn', [
            state('void', style({
                opacity: 0
            })),
            transition('void <=> *', animate(100)),
        ])
    ]
})
export class RegisterComponent implements OnInit {
    registerForm!: FormGroup;

    showRegisterForm: boolean = false

    constructor(private formBuilder: FormBuilder, private loginFlag: LoginRegisterModalFlagService, private http: HttpClient, private router: Router) {
    }


    ngOnInit() {
        this.registerForm = this.formBuilder.group({
            'email': ['', [Validators.required, Validators.email]],
            'username': ['', Validators.required],
            'password': ['', [Validators.required, Validators.minLength(6)]]
        });

        this.loginFlag.currentRegisterFlag.subscribe(flag => this.showRegisterForm = flag);
    }

    onSubmit() {

        const user = {
            email: '',
            username: '',
            password: ''
        };

        if (this.registerForm.valid) {
            user.email = this.registerForm.get('email')?.value;
            user.username = this.registerForm.get('username')?.value;
            user.password = this.registerForm.get('password')?.value;
        }

        this.http.post('https://localhost:7282/api/Auth/register', user).pipe(
            tap(response => {
                console.log(response);
                this.closeRegisterForm();
            }),
            catchError(error => {
                window.alert(error.error);
                return error;
            }),
        ).subscribe();
    }

    closeRegisterForm() {
        this.loginFlag.changeRegisterFlag(false);
    }
}
