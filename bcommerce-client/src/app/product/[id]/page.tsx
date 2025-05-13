// app/product/[id]/page.tsx
import { getProductById } from "@/services/products/get-product-by-id";
import { notFound } from "next/navigation";
import ProductViewer from "./product-viewer";

type Params = {
    params: {
        id: string;
    };
};

export default async function ProductPage({ params }: Params) {
    const { id } = params;
    const result = await getProductById(id);

    if (!result.success) {
        console.error("Erro ao buscar produto:", result.errors);
        notFound();
    }

    return <ProductViewer product={result.data} />;
}
