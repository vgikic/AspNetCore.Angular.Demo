import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { Injectable } from "@angular/core";
import { DataService } from "../data.service";
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";

@Injectable({ providedIn: 'root' })
export class CanActivateItems implements CanActivate {

    constructor(private dataService: DataService) { }

    public canActivate = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> => {
        return this.dataService.canActivateItemsRoute$
            .pipe(
                map((canActivate: boolean) => canActivate)
            )
    }
}