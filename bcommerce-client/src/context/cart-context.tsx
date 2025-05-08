"use client";
import { createContext, useContext } from "react";
import { useCart } from "@/hooks/useCart";
import { ReactNode } from "react";

const CartContext = createContext<ReturnType<typeof useCart> | null>(null);

export function CartProvider({ children }: { children: ReactNode }) {
    const cart = useCart();
    return <CartContext.Provider value={cart}>{children}</CartContext.Provider>;
}

export function useCartContext() {
    const context = useContext(CartContext);
    if (!context) {
        throw new Error("useCartContext deve ser usado dentro de CartProvider");
    }
    return context;
}