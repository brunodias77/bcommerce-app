export type Product = {
  productId: string;
  name: string;
  description: string;
  price: number;
  oldPrice?: number;
  categoryId: string;
  categoryName: string;
  stockQuantity: number;
  sold: number;
  isActive: boolean;
  popular: boolean;
  createdAt: string; // ou Date
  images: string[];
  colors: {
    name: string;
    value: string;
  }[];
  reviews: unknown[]; // Você pode substituir `any` por um tipo específico se souber a estrutura dos reviews
};

export type GetAllProductsResponse = Product[];
