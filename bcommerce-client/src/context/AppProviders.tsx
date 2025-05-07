// src/context/AppProviders.tsx
"use client";

import { ReactNode } from "react";
import { AuthProvider } from "./auth-context";
import { ProductsProvider } from "./products-context";
import { CartProvider } from "./cart-context";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

interface Props {
    children: ReactNode;
}

const AppProviders = ({ children }: Props) => {
    return (
        <AuthProvider>
            <ProductsProvider>
                <CartProvider>
                    {children}
                    <ToastContainer />
                </CartProvider>
            </ProductsProvider>
        </AuthProvider>
    );
};

export default AppProviders;
