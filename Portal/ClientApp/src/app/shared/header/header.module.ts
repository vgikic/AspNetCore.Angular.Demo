import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { HeaderComponent } from "./header.component";
import { MatToolbarModule } from "@angular/material";
import { RouterModule } from "@angular/router";


@NgModule({
    imports: [
        CommonModule,
        MatToolbarModule,
        RouterModule,
    ],
    declarations: [HeaderComponent],
    exports: [HeaderComponent]
})
export class HeaderModule { }
