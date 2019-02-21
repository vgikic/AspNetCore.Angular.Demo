import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatSnackBarModule } from '@angular/material';
import { HeaderModule } from '../shared/header/header.module';
import { SignalrComponent } from './signalr/signalr.component';
import { SnackBarService } from '../services/snackbar.service';

@NgModule({
  imports: [
    CommonModule,
    HeaderModule,
    MatSnackBarModule,
    RouterModule.forChild([
      { path: '', component: SignalrComponent },
    ]),
  ],
  declarations: [SignalrComponent],
  providers: [SnackBarService]
})
export class SignalrModule { }
