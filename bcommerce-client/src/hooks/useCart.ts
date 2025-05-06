import { CartItems } from "@/types/cart";
import { useEffect, useMemo, useReducer, useState } from "react";
import { toast } from "react-toastify";
import { useProductsContext } from "@/context/products-context";

type Action =
  | { type: "ADD_TO_CART"; productId: string; color: string }
  | {
      type: "UPDATE_QUANTITY";
      productId: string;
      color: string;
      quantity: number;
    };

const initialState: CartItems = {};

function cartReducer(state: CartItems, action: Action): CartItems {
  const newState = JSON.parse(JSON.stringify(state));
  switch (action.type) {
    case "ADD_TO_CART":
      if (!action.color) {
        toast.error("Selecione uma cor!");
        return state;
      }
      if (!newState[action.productId]) {
        newState[action.productId] = {};
      }
      newState[action.productId][action.color] =
        (newState[action.productId][action.color] || 0) + 1;
      return newState;
    case "UPDATE_QUANTITY":
      if (!newState[action.productId]) newState[action.productId] = {};
      newState[action.productId][action.color] = action.quantity;
      return newState;

    default:
      return state;
  }
}

export function useCart() {
  const [cartItems, dispatch] = useReducer(cartReducer, initialState);
  const [cupomDesconto, setCupomDesconto] = useState<number>(0);
  const { products } = useProductsContext();

  const addToCart = (productId: string, color: string) => {
    dispatch({ type: "ADD_TO_CART", productId, color });
  };
  const updateQuantities = (
    productId: string,
    color: string,
    quantity: number
  ) => {
    dispatch({ type: "UPDATE_QUANTITY", productId, color, quantity });
  };

  const getCartCount = useMemo(() => {
    return Object.values(cartItems).reduce(
      (acc, colors) =>
        acc + Object.values(colors ?? {}).reduce((sum, qty) => sum + qty, 0),
      0
    );
  }, [cartItems]);

  const getCartAmount = useMemo(() => {
    return Object.entries(cartItems).reduce((total, [productId, colors]) => {
      const product = products.find((product) => product.id === productId);
      if (!product || !colors) return total;

      return (
        total +
        Object.values(colors).reduce(
          (sum, qty) => sum + qty * product.price.amount,
          0
        )
      );
    }, 0);
  }, [cartItems, products]);

  const aplicarCupom = (codigo: string) => {
    if (codigo.toLowerCase() === "desconto10") {
      setCupomDesconto(0.1);
      toast.success("Cupom aplicado com sucesso! ✅");
      return true;
    } else {
      setCupomDesconto(0);
      toast.error("Cupom inválido! ❌");
      return false;
    }
  };
  useEffect(() => {
    console.log("Carrinho atualizado no hook:", cartItems);
  }, [cartItems]);

  return {
    cartItems,
    addToCart,
    updateQuantities,
    getCartCount,
    getCartAmount,
    aplicarCupom,
    cupomDesconto,
  };
}
