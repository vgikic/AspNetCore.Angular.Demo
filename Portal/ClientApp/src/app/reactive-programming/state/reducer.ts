
import { ItemDto } from '../../_autogenerated/itemDto';
import { HttpErrorResponse } from '@angular/common/http';
import { ItemsActions, ItemsActionTypes } from './actions';

export interface ItemsState {
    items: ItemDto[],
    selectedItemId: number | null,
    error: HttpErrorResponse
}

const initialState: ItemsState = {
    items: [],
    selectedItemId: null,
    error: null
};

export function reducer(state = initialState, action: ItemsActions): ItemsState {

    switch (action.type) {

        case ItemsActionTypes.LoadSuccess:
            return {
                ...state,
                items: action.payload,
            };

            case ItemsActionTypes.GetByIdSuccess:
            case ItemsActionTypes.SetSelectedItem:
            return {
                ...state,
                items: !state.items.find(i => i.id === action.payload.id) ?
                    state.items.concat(action.payload) : state.items,
                selectedItemId: action.payload.id
            };

        case ItemsActionTypes.FailedAction:
            return {
                ...state,
                error: action.payload
            };

        default:
            return state;

    }

}