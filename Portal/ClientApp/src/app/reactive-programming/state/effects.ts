import { Action } from "@ngrx/store";
import { Observable, of } from "rxjs";
import { Injectable } from "@angular/core";
import { Actions, Effect, ofType } from "@ngrx/effects";
import { ItemService } from "../../services/item.service";
import { mergeMap, map, catchError } from "rxjs/operators";
import { ItemsActionTypes, LoadSuccess, FailedAction, GetById, GetByIdSuccess, GetBadRequestSuccess } from "../state/actions"

@Injectable()
export class ItemsEffects {

    constructor(
        private actions$: Actions,
        private itemService: ItemService) { }

    @Effect()
    loadItems$: Observable<Action> = this.actions$.pipe(
        ofType(ItemsActionTypes.Load),
        mergeMap(action => {
            return this.itemService.getItems().pipe(
                map(items => (new LoadSuccess(items))),
                catchError(err => of(new FailedAction(err)))
            )
        }));

    @Effect()
    getItemById$: Observable<Action> = this.actions$.pipe(
        ofType(ItemsActionTypes.GetById),
        map((action: GetById) => action.payload),
        mergeMap((id: number) =>
            this.itemService.getItem(id).pipe(
                map(item => new GetByIdSuccess(item)),
                catchError(err => of(new FailedAction(err)))
            )
        )
    );

    @Effect()
    getBadRequest$: Observable<Action> = this.actions$.pipe(
        ofType(ItemsActionTypes.GetBadRequest),
        mergeMap(action => {
            return this.itemService.getBadRequest().pipe(
                map(response => new GetBadRequestSuccess(response)),
                catchError(err => of(new FailedAction(err)))
            )
        }));

}