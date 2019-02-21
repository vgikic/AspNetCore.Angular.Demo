import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { NgSelectModule } from '@ng-select/ng-select';
import { EditComponent } from './edit/edit.component';
import { ItemsComponent } from './items/items.component';
import { HeaderModule } from '../shared/header/header.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CanActivateItems } from '../services/guards/can-activate-items';
import { ItemDetailsResolver } from '../services/resolvers/item-details.resolver';
import { DxDataGridModule, DxDateBoxModule, DxValidatorModule } from 'devextreme-angular';

import {
  MatIconModule,
  MatInputModule,
  MatButtonModule,
  MatCheckboxModule,
  MatButtonToggleModule,
  MatRadioModule,
  MatToolbarModule,
} from '@angular/material';

@NgModule({
  imports: [
    FormsModule,
    CommonModule,
    MatIconModule,
    MatToolbarModule,
    MatInputModule,
    NgSelectModule,
    MatRadioModule,
    MatButtonModule,
    DxDateBoxModule,
    DxDataGridModule,
    DxValidatorModule,
    MatCheckboxModule,
    ReactiveFormsModule,
    MatButtonToggleModule,
    HeaderModule,
    RouterModule.forChild([
      { path: '', redirectTo: 'overview' },
      { path: 'overview', component: ItemsComponent, canActivate: [CanActivateItems] },
      { path: 'edit/:id', component: EditComponent, resolve: { item: ItemDetailsResolver } },
    ]),
  ],
  declarations: [
    ItemsComponent,
    EditComponent
  ],
})
export class ItemsModule { }
