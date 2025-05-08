"use client";

import React, {
  createContext,
  useContext,
  useEffect,
  useState,
  useCallback,
  useMemo,
} from "react";
import { useRouter } from "next/navigation";
import { createCustomer } from "@/services/customers/create-customer-service";
import { signInCustomer } from "@/services/customers/login-customer-service";

import {
  LoginCustomerInput,
  CreateCustomerInput,
  CreateCustomerOutput,
} from "@/types/customer";
import { ErrorResponse } from "@/types/api";

interface AuthContextData {
  userName: string | null;
  token: string | null;
  isAuthenticated: boolean;
  login: (input: LoginCustomerInput) => Promise<{ success: true } | { success: false; errors: ErrorResponse[] }>;
  register: (input: CreateCustomerInput) => Promise<{ success: true; data: CreateCustomerOutput } | { success: false; errors: ErrorResponse[] }>;
  logout: () => void;
}

const AuthContext = createContext<AuthContextData | undefined>(undefined);

export const AuthProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const [token, setToken] = useState<string | null>(null);
  const [userName, setUserName] = useState<string | null>(null);

  const router = useRouter();

  useEffect(() => {
    const storedToken = localStorage.getItem("token");
    const storedUserName = localStorage.getItem("userName");
    if (storedToken) setToken(storedToken);
    if (storedUserName) setUserName(storedUserName);
  }, []);

  const login = useCallback(async (input: LoginCustomerInput) => {
    const result = await signInCustomer(input);

    if (result.success) {
      localStorage.setItem("token", result.data.token);
      localStorage.setItem("userName", result.data.name);
      setToken(result.data.token);
      setUserName(result.data.name); // <-- atualiza estado também
      return { success: true } as const;
    } else {
      return { success: false, errors: result.errors } as const;
    }
  }, []);

  const register = useCallback(async (input: CreateCustomerInput) => {
    return await createCustomer(input);
  }, []);

  const logout = useCallback(() => {
    localStorage.removeItem("token");
    localStorage.removeItem("name");
    setToken(null);
    setUserName(null); // <-- limpa nome
    router.push("/login");
  }, [router]);

  const value = useMemo(
    () => ({ userName, token, isAuthenticated: !!token, login, register, logout }),
    [token, login, register, logout]
  );

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
};

export const useAuth = (): AuthContextData => {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error("useAuth deve ser usado dentro de <AuthProvider>");
  }
  return context;
};
