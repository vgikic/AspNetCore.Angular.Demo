import { Component, OnInit } from '@angular/core';
import { ItemsState } from '../state/reducer';
import { Store, select } from '@ngrx/store';
import { getSelectedItem } from '../state/index'
import { ItemDto } from '../../_autogenerated/itemDto';
import { Observable } from 'rxjs';

@Component({
  selector: 'compB',
  templateUrl: './compB.component.html',
  styleUrls: ['./compB.component.scss']
})
export class CompBComponent implements OnInit {

  public item$: Observable<ItemDto>;
  constructor(private store: Store<ItemsState>) { }

  ngOnInit() {
    this.item$ = this.store.pipe(select(getSelectedItem));
  }

}
