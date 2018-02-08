import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'fetchdata',
    templateUrl: './fetchdata.component.html',
    styleUrls: ['./admin.css']
})
export class FetchDataComponent {

    alertAdd() {
        alert("New helptext added!");
    }

    alertEdit() {
        alert("The helptext has beed edited!");
    }

    alertDelete() {
        alert("Are you sure you would like to delete this helptext?");
    }

}
