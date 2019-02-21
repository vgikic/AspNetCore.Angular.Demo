import { Component } from '@angular/core';
import { MatBottomSheet } from '@angular/material';
import { AppSheetComponent } from '../app-sheet.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
  constructor(private bottomSheet: MatBottomSheet) {}
  public openSheet(): void {
    this.bottomSheet.open(AppSheetComponent);
  }
}
