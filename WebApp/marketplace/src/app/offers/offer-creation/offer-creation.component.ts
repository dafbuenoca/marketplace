import {Component} from '@angular/core';
import {FormBuilder, FormGroup, UntypedFormGroup, Validators} from '@angular/forms';
import { MarketplaceApiService } from 'src/app/core/marketplace-api/marketplace-api.service';
import { OfferModel } from 'src/app/core/marketplace-api/models/offer.model';

@Component({
  selector: 'app-offer-creation',
  templateUrl: './offer-creation.component.html',
  styleUrls: ['./offer-creation.component.scss']
})
export class OfferCreationComponent  {

  offerForm: FormGroup;

  categories: { id: number, name: string }[] = []

  constructor(private marketplaceApiService : MarketplaceApiService, private formBuilder: FormBuilder ) { }

  ngOnInit() {
    this.marketplaceApiService.getCategories().subscribe(
      (response) => {
        console.log(response)
        this.categories = response
      },
      (error) => {
        console.error(error)
      }
    );

    this.offerForm = this.formBuilder.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
      location: ['', Validators.required],
      pictureUrl: ['', Validators.required],
      categoryId: ['', Validators.required],
    });

  }

  offerSubmit() {
    if (this.offerForm.valid) {

      const offer = {
        title: this.offerForm.get('title').value,
        description: this.offerForm.get('description').value,
        location: this.offerForm.get('location').value,
        pictureUrl: this.offerForm.get('pictureUrl').value,
        categoryId: this.offerForm.get('categoryId').value,
        userId: 124
      }

      this.marketplaceApiService.postOffer(offer).subscribe(
        (response) => {
          console.log('The offer has been created:', response);
          
        },
        (error) => {
          console.error('Error to create the offer:', error);
        }
      );

    }
  }
}
