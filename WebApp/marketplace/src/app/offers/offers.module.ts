import {NgModule} from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {OfferItemComponent} from './offer-item/offer-item.component';
import {OfferCreationComponent} from './offer-creation/offer-creation.component';
import {OfferListComponent} from './offer-list/offer-list.component';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [OfferItemComponent, OfferCreationComponent, OfferListComponent],
  imports: [
    BrowserModule,
    CommonModule,
    ReactiveFormsModule
  ]
})
export class OffersModule { }
