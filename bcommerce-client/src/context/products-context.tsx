"use client";
import { createContext, useContext, useMemo, ReactNode } from "react";
import { useProducts } from "@/hooks/useProducts";
/**
 * ProductsContext
 *
 * Contexto React responsável por fornecer o estado global dos produtos,
 * incluindo a lista de produtos, estado de carregamento, erros e uma função
 * de `reload()`. Esse contexto deve envolver qualquer parte da aplicação que
 * precise acessar ou atualizar produtos.
 */
const ProductsContext = createContext<ReturnType<typeof useProducts> | undefined>(undefined);

/**
 * ProductsProvider
 *
 * Provider que encapsula o hook `useProducts` e fornece seus dados e funções
 * através de um contexto global. Usa `useMemo` para evitar re-renderizações
 * desnecessárias em componentes filhos.
 *
 * @param children - Componentes filhos que terão acesso ao contexto
 */
export function ProductsProvider({ children }: { children: ReactNode }) {
    const manager = useProducts();

    const value = useMemo(() => manager, [manager.products, manager.loading, manager.error]);

    return <ProductsContext.Provider value={value}>{children}</ProductsContext.Provider>;
}

/**
 * useProductsContext
 *
 * Hook personalizado para acessar o contexto de produtos de forma segura.
 * Garante que o contexto seja utilizado apenas dentro do `ProductsProvider`.
 *
 * @returns Estado e funções fornecidas pelo `useProducts`
 * @throws Erro se usado fora do `ProductsProvider`
 */
export function useProductsContext() {
    const context = useContext(ProductsContext);
    if (!context) {
        throw new Error("useProducts deve ser usado dentro de ProductsProvider.");
    }
    return context;
}

