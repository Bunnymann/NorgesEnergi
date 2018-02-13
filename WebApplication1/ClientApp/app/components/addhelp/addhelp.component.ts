import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { Router } from '@angular/router';

@Component({
    selector: 'addhelp',
    templateUrl: './addhelp.component.html',
    styleUrls: ['./addhelp.component.css']
})
export class AddHelpComponent {
    
    stage1 = [{ 'name': 'NOR' }, { 'name': 'SWE' }, { 'name': 'FIN' }];
    selectedStage1 = this.stage1[0];

    stage2 = [{ 'name': 'Option 1' }, { 'name': 'Option 2' }, { 'name': 'Option 3' }];
    selectedStage2 = this.stage2[0];

    stage3 = [{ 'name': 'Option 1' }, { 'name': 'Option 2' }, { 'name': 'Option 3' }];
    selectedStage3 = this.stage3[0];

    stage4 = [{ 'name': 'Option 1' }, { 'name': 'Option 2' }, { 'name': 'Option 3' }];
    selectedStage4 = this.stage4[0];

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

