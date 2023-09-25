import {Component, OnInit} from '@angular/core';
import {Tweet} from "../Interfaces/Tweet";
import {HttpClient} from "@angular/common/http";
import {CurrentUserService} from "../services/currentUser.service";

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

    tweets: Tweet[] | undefined;
    following: boolean = false;
    private API_BASE_URL = 'https://localhost:7282/api/Tweets';

    constructor(private http: HttpClient, private currentUserService: CurrentUserService) {
    }

    ngOnInit() {
        if (this.currentUserService.getItem('currentUser') != null) {
            this.following = true;
        }

        const userId = this.currentUserService.getItem('currentUser').id;

        this.http.get<Tweet[]>(`${this.API_BASE_URL}/user/${userId}/following/tweets`, {withCredentials: true}).subscribe(response => {
            this.tweets = response;
        });
    }
}
