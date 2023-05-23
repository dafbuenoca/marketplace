import {Injectable} from '@angular/core';
import {Observable, of} from 'rxjs';
import {OfferModel} from './models/offer.model';
import { HttpClient } from '@angular/common/http';
import { PageModel } from '../models/page.model';
import { map } from 'rxjs/operators';
@Injectable({
  providedIn: 'root'
})
export class MarketplaceApiService {

  private readonly marketplaceApUrl = 'https://localhost:44313';

  constructor(private http: HttpClient) { }

  getOffers(pageIndex: number, pageSize: number): Observable<PageModel<OfferModel>> {
    const url = `${this.marketplaceApUrl}/Offer?pageIndex=${pageIndex}&pageSize=${pageSize}`;

    return this.http.get<PageModel<OfferModel>>(url);
  }

  postOffer(offer): Observable<string> {
    
    return this.http.post<string>(this.marketplaceApUrl + '/Offer', offer);
  }

  getCategories(): Observable<any> {
    return this.http.get<any>(this.marketplaceApUrl + '/Category');
  }
}
