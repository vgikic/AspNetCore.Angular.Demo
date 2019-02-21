import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GalleryComponent } from './gallery/gallery.component';
import { RouterModule } from '@angular/router';
import { DxFileUploaderModule, DxGalleryModule } from 'devextreme-angular';
import { MatIconModule, MatButtonModule, MatToolbarModule, MatCardModule, MatTooltipModule } from '@angular/material';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { HeaderComponent } from '../shared/header/header.component';
import { HeaderModule } from '../shared/header/header.module';
import {FlexLayoutModule} from "@angular/flex-layout";
@NgModule({
  imports: [
    FormsModule,
    CommonModule,
    FlexLayoutModule,
    MatTooltipModule,
    MatIconModule,
    MatButtonModule,
    MatCardModule,
    DxGalleryModule,
    HeaderModule,
    ReactiveFormsModule,
    DxFileUploaderModule,
    RouterModule.forChild([
      { path: '', redirectTo: 'example' },
      { path: 'example', component: GalleryComponent },
    ]),
  ],

  declarations: [GalleryComponent]
})
export class GalleryModule { }
