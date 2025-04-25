import { Product } from "@/types/product";
import { ErrorResponse } from "@/types/api";

const API_BASE_URL = process.env.NEXT_PUBLIC_API_URL ?? "http://localhost:5111";

async function handleApiResponse<T>(
  response: Response
): Promise<
  { success: true; data: T } | { success: false; errors: ErrorResponse[] }
> {
  if (response.ok) {
    const data: T = await response.json();
    return { success: true, data };
  } else {
    try {
      const errors: ErrorResponse[] = await response.json();
      return { success: false, errors };
    } catch {
      return {
        success: false,
        errors: [
          { message: "Erro desconhecido ao processar a resposta da API." },
        ],
      };
    }
  }
}

export async function getAllProducts(): Promise<
  | { success: true; data: Product[] }
  | { success: false; errors: ErrorResponse[] }
> {
  try {
    const response = await fetch(`${API_BASE_URL}/api/product`, {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
      },
    });

    return await handleApiResponse<Product[]>(response);
  } catch (error) {
    console.error("Erro ao se conectar com a API:", error);
    return {
      success: false,
      errors: [{ message: "Erro ao se conectar com o servidor." }],
    };
  }
}

// import { useEffect, useState } from "react";
// import { getAllProducts } from "@/services/api/product";
// import { ProductOutput } from "@/types/product-types";

// export default function ProductList() {
//   const [products, setProducts] = useState<ProductOutput[]>([]);

//   useEffect(() => {
//     getAllProducts().then((res) => {
//       if (res.success) {
//         setProducts(res.data);
//       } else {
//         console.error("Erro ao carregar produtos:", res.errors);
//       }
//     });
//   }, []);

//   return (
//     <div>
//       {products.map((p) => (
//         <div key={p.id}>
//           <h2>{p.name}</h2>
//           <p>{p.description}</p>
//           <span>Preço: R$ {p.price.amount.toFixed(2)}</span>
//           {p.oldPrice && (
//             <span style={{ textDecoration: "line-through", marginLeft: "1rem" }}>
//               De: R$ {p.oldPrice.amount.toFixed(2)}
//             </span>
//           )}
//           <p>Categoria: {p.category.name}</p>
//           <p>Cores: {p.colors.map(c => c.value).join(", ")}</p>
//         </div>
//       ))}
//     </div>
//   );
// }
