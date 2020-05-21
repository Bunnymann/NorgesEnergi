import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
    selector: 'login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.css']
})
export class LoginComponent {

    constructor(private router: Router) {
    }

    public gotoAdmin() {
        this.router.navigate(["admin"]);
    }
}
