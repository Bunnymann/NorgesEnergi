import { Component } from '@angular/core';

@Component({
    selector: 'shared-body',
    templateUrl: './body.Component.html'
})
export class body {
    name: string;
    constructor() {
        this.name = 'Sam';
    }
}
