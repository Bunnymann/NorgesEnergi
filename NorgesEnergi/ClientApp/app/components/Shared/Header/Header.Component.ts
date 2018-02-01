
import { Component } from '@angular/core';

@Component({
    selector: 'shared-header',
    templateUrl: './Header.Component.html'
})
export class header {
    name: string;
    constructor() {
        this.name = 'Sam';
    }
}
