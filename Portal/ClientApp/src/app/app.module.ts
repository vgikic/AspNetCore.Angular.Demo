import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import { AppComponent } from './app.component';
import { RouterModule } from '@angular/router';
import localeHr from '@angular/common/locales/hr';
import { NgModule, LOCALE_ID } from '@angular/core';
import { HomeComponent } from './home/home.component';
import { registerLocaleData } from '@angular/common';
import { AppSheetComponent } from './app-sheet.component';
import { BrowserModule } from '@angular/platform-browser';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ForbiddenComponent } from './shared/forbidden/forbidden.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouteNotFoundComponent } from './shared/route-not-found/route-not-found.component';
import { SpinnerInterceptorService } from './services/interceptors/spinner-interceptor.service';
import { AuthenticationInterceptorService } from './services/interceptors/authentication-interceptor.service';
import { MatProgressSpinnerModule, MatBottomSheetModule, MatToolbarModule, MatListModule, MatButtonModule, MatDividerModule, MatCardModule } from '@angular/material';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    AppSheetComponent,
    ForbiddenComponent,
    RouteNotFoundComponent,
  ],
  imports: [
    MatCardModule,
    MatListModule,
    BrowserModule,
    MatButtonModule,
    MatToolbarModule,
    HttpClientModule,
    MatDividerModule,
    MatBottomSheetModule,
    BrowserAnimationsModule,
    MatProgressSpinnerModule,
    // Imports StoreModule with items sub-state and it's reducer function
    // NOTE: 'loadChildren' syntax allows modules to be Lazy Loaded
    // Scripts are downloaded only when navigation to route occurs
    RouterModule.forRoot([
      { path: '', redirectTo: 'home', pathMatch: 'full' },
      { path: 'home', pathMatch: 'full', component: HomeComponent },
      { path: 'item', loadChildren: './items/items.module#ItemsModule' },
      { path: 'signalr', loadChildren: './signalr/signalr.module#SignalrModule' },
      { path: 'forbidden', component: ForbiddenComponent, pathMatch: 'full' },
      { path: 'gallery', loadChildren: './gallery/gallery.module#GalleryModule' },
      { path: 'authentication', loadChildren: './authentication/authentication.module#AuthenticationModule' },
      { path: 'angular', loadChildren: './angular-examples/angular-examples.module#AngularExamplesModule' },
      { path: 'rx', loadChildren: './reactive-programming/reactive-programming.module#ReactiveProgrammingModule' },
      { path: '**', component: RouteNotFoundComponent }
    ]),
    // Both of these need to be registered in app root module
    // For specific feature implementations check reactive-programming.module.ts
    StoreModule.forRoot({}),
    EffectsModule.forRoot([]),
    StoreDevtoolsModule.instrument({
      name: 'Demo App',
      maxAge: 25,
    }),
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: SpinnerInterceptorService, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: AuthenticationInterceptorService, multi: true },
  ],
  entryComponents: [AppSheetComponent],
  bootstrap: [AppComponent],
})
export class AppModule {
  constructor() {
    registerLocaleData(localeHr, 'hr');
  }
}
