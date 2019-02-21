import { of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { Component, OnInit } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { ItemService } from '../../services/item.service';
import { UserAuthDto } from '../../_autogenerated/userAuthDto';
import { SnackBarService } from '../../services/snackbar.service';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { AuthenticationService } from '../../services/authentication.service';
import { ErrorStateMatcherExtension } from 'src/app/helpers/forms/error-state-matcher-extension';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  public authUsersResponse;
  public adminDataResponse;
  public supportDataResponse;
  public registering = false;
  public unauthorizedResponse;
  public showPassword = false;
  public loginForm: FormGroup;
  public userAuth: UserAuthDto;
  public matcher = new ErrorStateMatcherExtension();

  constructor(
    private fb: FormBuilder,
    private sb: SnackBarService,
    private itemsService: ItemService,
    private authService: AuthenticationService
  ) { }

  ngOnInit() {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]],
    });
    this.userAuth = this.authService.userAuth;
  }

  public login = () => {
    if (this.loginForm.valid) {
      const model = this.loginForm.getRawValue();

      var sub = this.authService.login(model).pipe(
        map(result => {
          if (result.isInactive) {
            this.sb.error('Inactive user!');
          } else if (result.isLocked) {
            this.sb.error('User is locked!');
          } else if (result.invalidCredentials) {
            this.sb.error('Invalid Credentials!');
          } else {
            this.userAuth = result;
            this.sb.success('You are now signed in!');
          }
          sub.unsubscribe();
        }),
        catchError((err: HttpErrorResponse) => {
          this.showErrorMsg(err);
          sub.unsubscribe();
          return of(err);
        })
      ).subscribe(r => r);
    }
  }

  public register = () => {
    if (this.loginForm.valid) {
      const model = this.loginForm.getRawValue();
      var sub = this.authService.register(model).pipe(
        map(result => {
          this.sb.success('Login with your new credentials!');
          this.registering = false;
          sub.unsubscribe();
        }),
        catchError((err: HttpErrorResponse) => {
          this.showErrorMsg(err);
          sub.unsubscribe();
          return of(err);
        })
      ).subscribe(response => response);
    }
  }

  public logout = () => {
    var sub = this.authService.logOut().subscribe(r => {
      this.userAuth = null;
      this.sb.success('You are now signed out!');
      sub.unsubscribe();
      this.adminDataResponse = undefined;
      this.supportDataResponse = undefined;
      this.unauthorizedResponse = undefined;
      this.authUsersResponse = undefined;
    });
  }

  private showErrorMsg = (err: HttpErrorResponse) => {
    let msg = '';
    for (let prop in err.error) {
      msg += err.error[prop];
    }
    this.sb.error(msg);
  }

  public getDataForAuthorizedUsers = () => {
    this.itemsService.getDataForAuthorizedUsers().subscribe(response => this.authUsersResponse = response.message);
  }

  public getDataForAdmins = () => {
    this.itemsService.getDataForAdmins().subscribe(response => this.adminDataResponse = response.message);
  }

  public getDataForSupport = () => {
    this.itemsService.getDataForSupport().subscribe(response => this.supportDataResponse = response.message);
  }

  public tryGettingUnattainableData = () => {
    this.itemsService.tryGettingUnattainableData().subscribe(response => this.unauthorizedResponse = response.message);
  }


}