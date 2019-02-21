import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { HeaderModule } from '../shared/header/header.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SpaceRemoverPipe } from '../services/pipes/space-remover.pipe';
import { ChildRouteComponent } from './child-route/child-route.component';
import { ReusableComponent } from '../shared/reusable/reusable.component';
import { CanDeactivateNgExample } from '../services/guards/can-deactivate-ng-example';
import { AngularExamplesComponent } from './angular-examples/angular-examples.component';
import {
  MatTabsModule,
  MatIconModule,
  MatInputModule,
  MatRadioModule,
  MatButtonModule,
  MatTooltipModule,
  MatDividerModule,
  MatExpansionModule,
  MatSlideToggleModule,
} from '@angular/material';

@NgModule({
  imports: [
    FormsModule,
    CommonModule,
    HeaderModule,
    MatTabsModule,
    MatIconModule,
    MatInputModule,
    MatRadioModule,
    MatButtonModule,
    MatDividerModule,
    MatTooltipModule,
    MatExpansionModule,
    ReactiveFormsModule,
    MatSlideToggleModule,
    RouterModule.forChild([
      { path: '', redirectTo: 'example' },
      {
        path: 'example', component: AngularExamplesComponent,
        data: { title: 'This Object Is Passed Through Data Property Of Route Object (angular-example.module.ts)' },
        children: [{ path: 'child', component: ChildRouteComponent }],
        canDeactivate: [CanDeactivateNgExample]
      },
    ]),
  ],
  declarations: [
    AngularExamplesComponent,
    ChildRouteComponent,
    SpaceRemoverPipe,
    ReusableComponent
  ],
  providers: []
})
export class AngularExamplesModule { }
