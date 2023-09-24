import {Component, OnInit} from '@angular/core';
import {ImagesService} from "../services/images.service";
import {CurrentUserService} from "../services/currentUser.service";
import {ProfilePhotoSaverService} from "../services/profile-photo-saver.service";

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {

    public profilePhoto: Blob | undefined;

    constructor(private imagesService: ImagesService, private currentUserService: CurrentUserService, private profilePhotoService: ProfilePhotoSaverService) {
    }

    ngOnInit() {
        this.imagesService.getProfilePhoto(this.currentUserService.getItem('currentUser').username).subscribe(response => {
            this.profilePhotoService.saveProfilePhotoToSessionStorage('profilePhoto', response);
        });
    }

    getProfilePhoto(): string {
        return this.profilePhotoService.getProfilePhotoFromSessionStorage('profilePhoto');
    }

  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
