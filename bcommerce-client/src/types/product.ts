// src/types/product.ts

export type ProductID = string;

export interface Price {
  amount: number;
}

export interface Stock {
  quantity: number;
}

export interface ImageUrl {
  url: string;
}

export interface Category {
  name: string;
}

export interface Color {
  value: string;
}

export interface Product {
  id: ProductID;
  name: string;
  description: string;
  price: Price;
  oldPrice?: Price | null;
  images: ImageUrl[];
  category: Category;
  colors: Color[];
  stock: Stock;
  sold: number;
  isActive: boolean;
  popular: boolean;
  createdAt: string; // ISO string
  updatedAt: string; // ISO string
  isNew: boolean;
  sale: boolean;
}
// src/types/blog.ts

export interface Blog {
  title: string;
  category: string;
  image: string;
}
export interface ReviewItem {
  rating: number;
  comment?: string;
  createdAt: string;
}

export interface ProductItem {
  id: string;
  name: string;
  description: string;
  price: number;
  oldPrice?: number | null;
  categoryId: string;
  categoryName: string;
  stockQuantity: number;
  sold: number;
  isActive: boolean;
  popular: boolean;
  createdAt: string;
  updatedAt: string;
  images: string[];
  colors: string[];
  reviews: ReviewItem[];
}

export interface GetAllProductsResponse {
  products: ProductItem[];
}
