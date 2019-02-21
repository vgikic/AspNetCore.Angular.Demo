import { Observable } from "rxjs";
import { Injectable } from "@angular/core";
import {  ActivatedRouteSnapshot, RouterStateSnapshot, CanDeactivate } from "@angular/router";
import { AngularExamplesComponent } from "../../angular-examples/angular-examples/angular-examples.component";

@Injectable({ providedIn: 'root' })
export class CanDeactivateNgExample implements CanDeactivate<AngularExamplesComponent> {

    constructor() { }

    public canDeactivate(component: AngularExamplesComponent, currentRoute: ActivatedRouteSnapshot, currentState: RouterStateSnapshot, nextState?: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
        return component.canDeactivate;
    }
}