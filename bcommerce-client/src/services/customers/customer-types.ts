// customer-types.ts

// Cadastro - entrada
export interface CreateCustomerInput {
  name: string;
  email: string;
  password: string;
}

// Cadastro - saída
export interface CreateCustomerOutput {
  id: string;
  name: string;
  email: string;
  isActive: boolean;
  createdAt: string;
}

// Login - entrada
export interface LoginCustomerInput {
  email: string;
  password: string;
}

// Login - saída (retorna apenas o token!)
export interface LoginCustomerOutput {
  token: string;
}

// Erros genéricos da API
export interface ErrorResponse {
  message: string;
}

// export interface CreateCustomerInput {
//   name: string;
//   email: string;
//   password: string;
// }

// export interface CreateCustomerOutput {
//   id: string;
//   name: string;
//   email: string;
//   createdAt: string;
//   updatedAt: string;
// }

// export interface LoginCustomerInput {
//   email: string;
//   password: string;
// }

// export interface LoginCustomerOutput {
//   id: string;
//   name: string;
//   email: string;
//   // opcional, caso você retorne token
//   token?: string;
// }

// export interface ErrorResponse {
//   message: string;
// }
