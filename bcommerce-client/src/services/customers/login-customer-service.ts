import { LoginCustomerInput, LoginCustomerOutput } from "@/types/customer";
import { ErrorResponse } from "@/types/api";

// Centraliza URL da API
const API_BASE_URL = process.env.NEXT_PUBLIC_API_URL ?? "http://localhost:5111";

// Reutiliza o handler de resposta
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

/**
 * Realiza login de um customer no backend.
 *
 * @param input Dados de login (email e senha)
 * @returns Resultado da operação com sucesso ou erro
 */
export async function signInCustomer(
  input: LoginCustomerInput
): Promise<
  | { success: true; data: LoginCustomerOutput }
  | { success: false; errors: ErrorResponse[] }
> {
  try {
    const response = await fetch(`${API_BASE_URL}/api/customer/signin`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(input),
    });

    return await handleApiResponse<LoginCustomerOutput>(response);
  } catch (error) {
    console.error("Erro ao se conectar com o servidor:", error);
    return {
      success: false,
      errors: [{ message: "Erro ao se conectar com o servidor." }],
    };
  }
}

// import { ErrorResponse, LoginCustomerInput, LoginCustomerOutput } from "./customer-types";

// /**
//  * Realiza login de um customer no backend.
//  *
//  * @param input Dados de login (email e senha)
//  * @returns Resultado da operação com sucesso ou erro
//  */
// export async function signInCustomer(
//   input: LoginCustomerInput
// ): Promise<
//   | { success: true; data: LoginCustomerOutput }
//   | { success: false; errors: ErrorResponse[] }
// > {
//   try {
//     const response = await fetch("http://localhost:5111/api/customer/signin", {
//       method: "POST",
//       headers: {
//         "Content-Type": "application/json",
//       },
//       body: JSON.stringify(input),
//     });

//     if (response.ok) {
//       const data: LoginCustomerOutput = await response.json();
//       return { success: true, data };
//     } else {
//       const errors: ErrorResponse[] = await response.json();
//       return { success: false, errors };
//     }
//   } catch (err) {
//     return {
//       success: false,
//       errors: [{ message: "Erro ao se conectar com o servidor." }],
//     };
//   }
// }
