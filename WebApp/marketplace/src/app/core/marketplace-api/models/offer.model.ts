export interface OfferModel {
  category: {
    id: number;
    name: string;
    offers: any;
  };
  categoryId: number;
  description: string;
  id: string;
  location: string;
  pictureUrl: string;
  publishedOn: string; // Asegúrate de que esta propiedad esté definida
  title: string;
  user: {
    id: number;
    offers: any;
    username: string;
  };
  userId: number;
}