import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";

@Injectable({
    providedIn: 'root'
})
export class ImagesService {
    private API_BASE_URL = 'https://localhost:7282/api/Images';

    constructor(private http: HttpClient) {
    }

    getProfilePhoto(username: string) {
        return this.http.get(`${this.API_BASE_URL}/${username}/profile-photo`, {responseType: 'blob', withCredentials: true});
    }
}
