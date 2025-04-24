export type Price = {
  amount: number;
};

export type Category = {
  name: string;
};

export type Color = {
  value: string;
};

export type ImageUrl = {
  url: string;
};

export type Stock = {
  quantity: number;
};

export type Product = {
  id: string;
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
  createdAt: string; // DateTime ISO
  updatedAt: string;
  isNew: boolean;
  sale: boolean;
};

// // Definição do tipo para um produto
// // export interface Product {
// //     _id: string;
// //     name: string;
// //     description: string;
// //     price: number;
// //     image: string[];
// //     category: string;
// //     colors: string[];
// //     date: number;
// //     popular: boolean;
// //   }

// export type ProductID = string;

// export interface Price {
//   amount: number;
// }

// export interface Stock {
//   quantity: number;
// }

// export interface ImageUrl {
//   url: string;
// }

// export interface Category {
//   name: string;
// }

// export interface Color {
//   value: string;
// }

// export interface Product {
//   id: ProductID;
//   name: string;
//   description: string;
//   price: Price;
//   oldPrice?: Price;
//   images: ImageUrl[];
//   category: Category;
//   colors: Color[];
//   stock: Stock;
//   sold: number;
//   isActive: boolean;
//   popular: boolean;
//   createdAt: string; // ISO date
//   updatedAt: string; // ISO date

//   // Computed properties
//   isNew: boolean;
//   sale: boolean;
// }

// Definição do tipo para um blog
export interface Blog {
  title: string;
  category: string;
  image: string;
}
