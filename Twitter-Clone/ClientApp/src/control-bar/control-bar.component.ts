import {Component} from '@angular/core';
import {ActivatedRoute} from "@angular/router";


@Component({
    selector: 'app-control-bar',
    templateUrl: './control-bar.component.html',
    styleUrls: ['./control-bar.component.css']
})
export class ControlBarComponent {

    public isUrlHome: boolean = false;
    public isUrlSearch: boolean = false;

    constructor(route: ActivatedRoute) {
        route.url.subscribe(url => {
            this.isUrlHome = url.join('/') === 'home';
        });

        route.url.subscribe(url => {
            this.isUrlSearch = url.join('/') === 'search';
        });
    }

}
