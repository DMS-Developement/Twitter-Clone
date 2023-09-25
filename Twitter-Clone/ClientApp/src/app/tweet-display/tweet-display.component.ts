import {Component, Input, OnInit} from '@angular/core';
import {Tweet} from "../Interfaces/Tweet";
import {ImagesService} from "../services/images.service";

@Component({
    selector: 'app-tweet-display',
    templateUrl: './tweet-display.component.html',
    styleUrls: ['./tweet-display.component.css']
})
export class TweetDisplayComponent implements OnInit {
    @Input() tweets: Tweet[] | undefined;

    constructor(private imagesService: ImagesService) {
    }

    ngOnInit() {
    }

    getProfilePhotoUrl(username: string) {
        return this.imagesService.getProfilePhotoUrl(username);
    }

}
