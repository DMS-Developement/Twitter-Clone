import {Component, OnInit} from '@angular/core';
import {LoginRegisterModalFlagService} from "../login-register-modal-flag.service";

@Component({
    selector: 'app-welcome',
    templateUrl: './welcome.component.html',
    styleUrls: ['./welcome.component.css']
})
export class WelcomeComponent implements OnInit {

    showLoginForm: boolean = false;
    showRegisterForm: boolean = false;

    constructor(private loginFlag: LoginRegisterModalFlagService) {
    }

    ngOnInit() {
        this.loginFlag.currentLoginFlag.subscribe(flag => this.showLoginForm = flag);
        this.loginFlag.currentRegisterFlag.subscribe(flag => this.showRegisterForm = flag);
    }

    showHideLoginForm() {
        this.loginFlag.changeLoginFlag(true);
    }

    showHideRegisterForm() {
        this.loginFlag.changeRegisterFlag(true);
    }
}
