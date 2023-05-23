import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { MarketplaceApiService } from './marketplace-api.service';

describe('MarketplaceApiService', () => {
  let service: MarketplaceApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule], 
      providers: [MarketplaceApiService], 
    });
    service = TestBed.inject(MarketplaceApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
