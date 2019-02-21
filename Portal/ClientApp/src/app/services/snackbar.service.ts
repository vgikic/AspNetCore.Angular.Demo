
import { Actions } from "@ngrx/effects";
import { Injectable } from "@angular/core";
import { MatSnackBar } from "@angular/material";
import { ItemsActionTypes } from '../reactive-programming/state/actions'


/**
 * Snackbar (popup) options.
 * */
@Injectable({ providedIn: 'root' })
export class SnackBarService {

    constructor(
        private sb: MatSnackBar,
        private actions$: Actions
    ) {
        this.subscribeToActions();
    }

    private showSnackBar = (msg: string, action: string = 'X', panelClass: string[] = ['snackbar-addition']) => {
        this.sb.open(msg, action, {
            duration: 5000,
            horizontalPosition: "right",
            verticalPosition: "bottom",
            politeness: "assertive",
            panelClass: panelClass
        });
    }

    public error = (msg?: string) => this.showSnackBar(msg || 'Greška');
    public success = (msg?: string) => this.showSnackBar(msg || 'Success', 'X', ['success']);

    public subscribeToActions = () => {

        this.actions$
            .ofType(ItemsActionTypes.FailedAction).subscribe(r => this.error("Error occurred!"));

    }

}

