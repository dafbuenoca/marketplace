import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { OfferItemComponent } from './offers/offer-item/offer-item.component';
import { OfferCreationComponent } from './offers/offer-creation/offer-creation.component';
import { OfferListComponent } from './offers/offer-list/offer-list.component';
import { CommonModule } from '@angular/common';

@NgModule({
  declarations: [
    AppComponent, OfferItemComponent, OfferCreationComponent, OfferListComponent
  ],
  imports: [
    ReactiveFormsModule,
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    CommonModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
