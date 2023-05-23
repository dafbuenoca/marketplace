import { Component, OnInit } from '@angular/core';
import { MarketplaceApiService } from 'src/app/core/marketplace-api/marketplace-api.service';
import { OfferModel } from 'src/app/core/marketplace-api/models/offer.model';
import { PageModel } from 'src/app/core/models/page.model';

@Component({
  selector: 'app-offer-list',
  templateUrl: './offer-list.component.html',
  styleUrls: ['./offer-list.component.scss']
})
export class OfferListComponent implements OnInit {

  pageNumber = 1;
  pageSize = 4;
  offers: OfferModel[];
  pageCount: number;
  pageIndex: number;

  constructor(private marketplaceApiService: MarketplaceApiService) { }

  ngOnInit(): void {
    this.getOffers();
  }

  getOffers(): void {
    this.marketplaceApiService.getOffers(this.pageNumber, this.pageSize).subscribe(
      pageModel => {
        this.offers = pageModel.items;
        console.log(this.offers);
        this.pageIndex = pageModel.pageIndex;
        this.pageCount = pageModel.pageCount;
      },
      error => {
        console.error(error);
      }
    );
  }

  onPageChange(page: number): void {
    this.pageNumber = page;
    this.getOffers();
  }

  getNextPageIndexes(): number[] {
    const nextIndexes: number[] = [];
    for (let i = this.pageIndex + 1; i < this.pageIndex + 4 && i <= this.pageCount; i++) {
      nextIndexes.push(i);
    }
    return nextIndexes;
  }

  getPreviousPageIndexes(): number[] {
    const previousIndexes: number[] = [];
    for (let i = this.pageIndex - 1; i > this.pageIndex - 4 && i >= 1; i--) {
      previousIndexes.unshift(i);
    }
    return previousIndexes;
  }
}