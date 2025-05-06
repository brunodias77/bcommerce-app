import { Product, ProductItem } from "@/types/product";

export function mapProductItemToProduct(item: ProductItem): Product {
  return {
    id: item.id,
    name: item.name,
    description: item.description,
    price: { amount: item.price },
    oldPrice: item.oldPrice != null ? { amount: item.oldPrice } : null,
    images: item.images.map((url) => ({ url })),
    category: { name: item.categoryName },
    colors: item.colors.map((color) => ({ value: color })),
    stock: { quantity: item.stockQuantity },
    sold: item.sold,
    isActive: item.isActive,
    popular: item.popular,
    createdAt: item.createdAt,
    updatedAt: item.updatedAt,
    isNew: false,
    sale: item.oldPrice !== null,
  };
}
