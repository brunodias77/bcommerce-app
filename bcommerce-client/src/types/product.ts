export type Product = {
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
  images: string[];
  colors: {
    name: string;
    value: string;
  }[];
  reviews: unknown[]; // Você pode substituir `any` por um tipo específico se souber a estrutura dos reviews
};

export interface GetAllProductsResponse {
  products: Product[];
}

// // src/types/blog.ts

// export interface Blog {
//   title: string;
//   category: string;
//   image: string;
// }
// export interface ReviewItem {
//   rating: number;
//   comment?: string;
//   createdAt: string;
// }

// export interface ProductItem {
//   id: string;
//   name: string;
//   description: string;
//   price: number;
//   oldPrice?: number | null;
//   categoryId: string;
//   categoryName: string;
//   stockQuantity: number;
//   sold: number;
//   isActive: boolean;
//   popular: boolean;
//   createdAt: string;
//   updatedAt: string;
//   images: string[];
//   colors: string[];
//   reviews: ReviewItem[];
// }

// export interface GetAllProductsResponse {
//   products: ProductItem[];
// }
// export interface GetProductByIdResponse {
//   product: ProductItem;
// }
