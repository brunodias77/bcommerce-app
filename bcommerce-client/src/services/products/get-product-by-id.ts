import { ErrorResponse } from "@/types/api";
import { ProductItem } from "@/types/product";

const API_BASE_URL = process.env.NEXT_PUBLIC_API_URL ?? "http://127.0.0.1:5111";

async function handleApiResponse<T>(
  response: Response
): Promise<
  { success: true; data: T } | { success: false; errors: ErrorResponse[] }
> {
  if (response.ok) {
    const data: T = await response.json();
    console.log("Dados da API:", data);
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

export async function getProductById(
  id: string
): Promise<
  | { success: true; data: ProductItem }
  | { success: false; errors: ErrorResponse[] }
> {
  try {
    const response = await fetch(`${API_BASE_URL}/api/products/${id}`, {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
      },
    });

    return await handleApiResponse<ProductItem>(response);
  } catch (error) {
    console.error("Erro ao buscar produto por ID:", error);
    return {
      success: false,
      errors: [{ message: "Erro ao se conectar com o servidor." }],
    };
  }
}
