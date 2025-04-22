"use client";

import { createContext, useContext, useEffect, useState, useCallback, useMemo } from "react";
import { useRouter } from "next/navigation";

interface AuthContextData {
  token: string | null;
  isAuthenticated: boolean;
  login: (token: string) => void;
  logout: () => void;
}

const AuthContext = createContext<AuthContextData | undefined>(undefined);

export const AuthProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const [token, setToken] = useState<string | null>(null);
  const router = useRouter();

  useEffect(() => {
    if (typeof window !== "undefined") {
      const storedToken = localStorage.getItem("token");
      if (storedToken) setToken(storedToken);
    }
  }, []);

  const login = useCallback((token: string) => {
    localStorage.setItem("token", token);
    setToken(token);
  }, []);

  const logout = useCallback(() => {
    localStorage.removeItem("token");
    setToken(null);
    router.push("/login");
  }, [router]);

  const value = useMemo(
    () => ({
      token,
      isAuthenticated: !!token,
      login,
      logout,
    }),
    [token, login, logout]
  );

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
};

// Hook customizado para usar o contexto
export const useAuth = () => {
  const context = useContext(AuthContext);
  if (!context) throw new Error("useAuth deve ser usado dentro de <AuthProvider>");
  return context;
};

// // context/AuthContext.tsx
// "use client";

// import { createContext, useContext, useEffect, useState } from "react";
// import { useRouter } from "next/navigation";

// interface AuthContextData {
//   token: string | null;
//   isAuthenticated: boolean;
//   login: (token: string) => void;
//   logout: () => void;
// }

// const AuthContext = createContext<AuthContextData | undefined>(undefined);

// export const AuthProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
//   const [token, setToken] = useState<string | null>(null);
//   const router = useRouter();

//   useEffect(() => {
//     // ✅ Verificação para evitar erro de hydration
//     if (typeof window !== "undefined") {
//       const storedToken = localStorage.getItem("token");
//       if (storedToken) {
//         setToken(storedToken);
//       }
//     }
//   }, []);

//   const login = (token: string) => {
//     localStorage.setItem("token", token);
//     setToken(token);
//   };

//   const logout = () => {
//     localStorage.removeItem("token");
//     setToken(null);
//     router.push("/login");
//   };

//   return (
//     <AuthContext.Provider value={{ token, isAuthenticated: !!token, login, logout }}>
//       {children}
//     </AuthContext.Provider>
//   );
// };

// // Hook customizado para usar em componentes
// export const useAuth = () => {
//   const context = useContext(AuthContext);
//   if (!context) throw new Error("useAuth deve ser usado dentro de <AuthProvider>");
//   return context;
// };


