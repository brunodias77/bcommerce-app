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
  name: string;
}
