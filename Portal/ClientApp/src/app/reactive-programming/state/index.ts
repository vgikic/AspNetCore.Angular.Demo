import { ItemsState } from './reducer';
import * as fromRoot from '../../state/index';
import { createFeatureSelector, createSelector } from '@ngrx/store';


export interface State extends fromRoot.State {
    referendumsState: ItemsState;
}

// Selector functions for itemsState
const getItemsState = createFeatureSelector<ItemsState>('itemsState');

export const getItems = createSelector(
    getItemsState,
    state => state.items
);

export const getSelectedItemId = createSelector(
    getItemsState,
    state => state.selectedItemId
);

export const getSelectedItem = createSelector(
    getItemsState,
    getSelectedItemId,
    (state, itemId) => state.items.find(item => item.id === itemId)
);