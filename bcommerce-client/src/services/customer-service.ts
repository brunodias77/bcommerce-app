export interface CreateCustomerInput {
  name: string;
  email: string;
  password: string;
}

export interface CreateCustomerOutput {
  id: string;
  name: string;
  email: string;
  createdAt: string;
  updatedAt: string;
}

export interface LoginCustomerInput {
  email: string;
  password: string;
}

export interface LoginCustomerOutput {
  id: string;
  name: string;
  email: string;
  // opcional, caso você retorne token
  token?: string;
}

export interface ErrorResponse {
  message: string;
}

/**
 * Cria um novo customer no backend.
 *
 * @param input Dados do customer
 * @returns Resultado da operação com sucesso ou erro
 */
export async function createCustomer(
  input: CreateCustomerInput
): Promise<
  | { success: true; data: CreateCustomerOutput }
  | { success: false; errors: ErrorResponse[] }
> {
  try {
    const response = await fetch("http://localhost:5111/api/customer/signup", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(input),
    });

    console.log("response", response);

    if (response.ok) {
      const data: CreateCustomerOutput = await response.json();
      return { success: true, data };
    } else {
      const errors: ErrorResponse[] = await response.json();
      return { success: false, errors };
    }
  } catch (err) {
    return {
      success: false,
      errors: [{ message: "Erro ao se conectar com o servidor." }],
    };
  }
}
