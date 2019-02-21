import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { HeaderModule } from '../shared/header/header.module';
import { SnackBarService } from '../services/snackbar.service';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MatCardModule, MatButtonModule, MatInputModule, MatIconModule, MatSnackBarModule } from '@angular/material';

@NgModule({
  imports: [
    FormsModule,
    CommonModule,
    HeaderModule,
    MatIconModule,
    MatCardModule,
    MatInputModule,
    MatButtonModule,
    MatSnackBarModule,
    ReactiveFormsModule,
    RouterModule.forChild([
      { path: 'login', component: LoginComponent },
    ]),

  ],
  providers: [SnackBarService],
  declarations: [LoginComponent],

})
export class AuthenticationModule { }
