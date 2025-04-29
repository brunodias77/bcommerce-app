// src/services/productsService.ts

import { ErrorResponse } from "@/types/api";
import {GetAllProductsResponse, } from "@/types/product";

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
  | { success: true; data: GetAllProductsResponse }
  | { success: false; errors: ErrorResponse[] }
> {
  try {
    const response = await fetch(`${API_BASE_URL}/api/products`, {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
      },
    });

    return await handleApiResponse<GetAllProductsResponse>(response);
  } catch (error) {
    console.error("Erro ao se conectar com a API:", error);
    return {
      success: false,
      errors: [{ message: "Erro ao se conectar com o servidor." }],
    };
  }
}
