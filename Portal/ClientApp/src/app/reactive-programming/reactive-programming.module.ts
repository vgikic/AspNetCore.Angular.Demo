import { NgModule } from '@angular/core';
import { StoreModule } from '@ngrx/store';
import { reducer } from './state/reducer';
import { EffectsModule } from '@ngrx/effects';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ItemsEffects } from './state/effects';
import { CompAComponent } from './compA/compA.component';
import { CompBComponent } from './compB/compB.component';
import { HeaderModule } from '../shared/header/header.module';
import { SnackBarService } from '../services/snackbar.service';
import { ReactiveProgrammingComponent } from './reactive-programming/reactive-programming.component';
import {
  MatTabsModule,
  MatIconModule,
  MatRadioModule,
  MatButtonModule,
  MatTooltipModule,
  MatSnackBarModule,
  MatExpansionModule,
  MatSlideToggleModule,
} from '@angular/material';

@NgModule({
  imports: [
    CommonModule,
    HeaderModule,
    MatTabsModule,
    MatIconModule,
    MatRadioModule,
    MatButtonModule,
    MatTooltipModule,
    MatSnackBarModule,
    MatExpansionModule,
    MatSlideToggleModule,
    RouterModule.forChild([
      { path: '', redirectTo: 'example' },
      { path: 'example', component: ReactiveProgrammingComponent },
    ]),
    //  'items' sub-state feature registration
    StoreModule.forFeature('itemsState', reducer),
    EffectsModule.forFeature([ItemsEffects]),
  ],
  declarations: [ReactiveProgrammingComponent, CompAComponent, CompBComponent],
  providers: [SnackBarService]
})
export class ReactiveProgrammingModule { }
