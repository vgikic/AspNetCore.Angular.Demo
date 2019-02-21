import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";

@Injectable({ providedIn: 'root' })
export class DataService {
    public data = ['item1 ', 'item2 ', 'item3 '];
    public canActivateRoute: boolean;
    public canActivateItemsRoute$ = new BehaviorSubject(true);
    constructor() {
        this.canActivateItemsRoute$.subscribe(v => this.canActivateRoute = v);
    }

}
