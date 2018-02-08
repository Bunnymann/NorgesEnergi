import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
    selector: 'home',
    templateUrl: './home.component.html',
    styleUrls: ['./Login.css']
})
export class HomeComponent {

    constructor(private router: Router) {
    }

    public gotoAdmin() {
        this.router.navigate(["admin"]);
    }
}
