import { CreateCustomerInput, CreateCustomerOutput } from "@/types/customer";
import { ErrorResponse } from "@/types/api";

// Crie um cliente API configurado para DRY (Don't Repeat Yourself)
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

export async function createCustomer(
  input: CreateCustomerInput
): Promise<
  | { success: true; data: CreateCustomerOutput }
  | { success: false; errors: ErrorResponse[] }
> {
  try {
    const response = await fetch(`${API_BASE_URL}/api/customer/signup`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(input),
    });

    return await handleApiResponse<CreateCustomerOutput>(response);
  } catch (error) {
    console.error("Erro ao se conectar com a API:", error);
    return {
      success: false,
      errors: [{ message: "Erro ao se conectar com o servidor." }],
    };
  }
}
