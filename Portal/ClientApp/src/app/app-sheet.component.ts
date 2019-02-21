import { Component } from "@angular/core";
import { MatBottomSheetRef } from "@angular/material";

@Component({
    selector: 'app-sheet',
    templateUrl: 'app-sheet.component.html',
  })
  export class AppSheetComponent {
    constructor(private bottomSheetRef: MatBottomSheetRef<AppSheetComponent>) {}
    openLink(event: MouseEvent): void {
      this.bottomSheetRef.dismiss();
    //   event.preventDefault();
    }
  }