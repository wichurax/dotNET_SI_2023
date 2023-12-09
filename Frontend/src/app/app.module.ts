import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TableComponent } from './table/table.component';
import {MatTableModule} from "@angular/material/table";
import {MatSortModule} from "@angular/material/sort";
import {CdkFixedSizeVirtualScroll} from "@angular/cdk/scrolling";
import {MatRippleModule} from "@angular/material/core";
import {MatPaginatorModule} from "@angular/material/paginator";
import {MatCardModule} from "@angular/material/card";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import { TimeAgoPipe } from './pipes/time-ago.pipe';
import {MatCheckboxModule} from "@angular/material/checkbox";
import {FormsModule} from "@angular/forms";

@NgModule({
  declarations: [
    AppComponent,
    TableComponent,
    TimeAgoPipe
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MatTableModule,
    MatSortModule,
    CdkFixedSizeVirtualScroll,
    MatRippleModule,
    MatPaginatorModule,
    MatCardModule,
    BrowserAnimationsModule,
    MatCheckboxModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
