import { getProductById } from "@/services/products/get-product-by-id";
import { notFound } from "next/navigation";

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
        notFound(); // ou renderize um fallback de erro
    }

    const product = result.data;

    return (
        <div className="container py-6">
            <h1 className="text-2xl font-bold mb-2">{product.name}</h1>
            <p className="text-gray-700 mb-4">{product.description}</p>

            <div className="text-lg font-semibold mb-2">
                Preço: R$ {product.price.toFixed(2)}
            </div>

            {product.oldPrice && (
                <div className="text-sm text-gray-500 line-through mb-4">
                    De: R$ {product.oldPrice.toFixed(2)}
                </div>
            )}

            <div>
                <strong>Categoria:</strong> {product.categoryName}
            </div>

            <div className="mt-2">
                <strong>Cores:</strong> {product.colors.join(", ")}
            </div>

            <div className="mt-4">
                <strong>Imagens:</strong>
                <ul className="list-disc ml-6">
                    {product.images.map((img, index) => (
                        <li key={index}>{img}</li>
                    ))}
                </ul>
            </div>

            <div className="mt-4">
                <strong>Avaliações:</strong>
                <ul className="list-disc ml-6">
                    {product.reviews.length > 0 ? (
                        product.reviews.map((review, index) => (
                            <li key={index}>
                                ⭐ {review.rating} -{" "}
                                {review.comment ? review.comment : "Sem comentário"}
                            </li>
                        ))
                    ) : (
                        <li>Sem avaliações</li>
                    )}
                </ul>
            </div>
        </div>
    );
}
