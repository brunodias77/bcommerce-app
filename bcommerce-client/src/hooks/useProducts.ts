import { useReducer, useEffect, useCallback } from "react";
import { getAllProducts } from "@/services/products/get-all-products";
import { Product } from "@/types/product";

interface ProductsState {
  products: Product[];
  loading: boolean;
  error: string | null;
}

type ProductsAction =
  | { type: "FETCH_START" }
  | { type: "FETCH_SUCCESS"; payload: Product[] }
  | { type: "FETCH_ERROR"; payload: string };

const initialState: ProductsState = {
  products: [],
  loading: true,
  error: null,
};

function productsReducer(
  state: ProductsState,
  action: ProductsAction
): ProductsState {
  switch (action.type) {
    case "FETCH_START":
      return { ...state, loading: true, error: null };
    case "FETCH_SUCCESS":
      return { ...state, loading: false, products: action.payload };
    case "FETCH_ERROR":
      return { ...state, loading: false, error: action.payload };
    default:
      return state;
  }
}

export function useProducts() {
  const [state, dispatch] = useReducer(productsReducer, initialState);

  const fetchProducts = useCallback(async () => {
    dispatch({ type: "FETCH_START" });

    try {
      const response = await getAllProducts();
      console.log("Dados da API useProduct:", response);

      if (!response.success || !response.data) {
        throw new Error("Erro ao carregar os produtos.");
      }

      dispatch({ type: "FETCH_SUCCESS", payload: response.data });
    } catch (error) {
      dispatch({
        type: "FETCH_ERROR",
        payload:
          error instanceof Error
            ? error.message
            : "Erro inesperado ao carregar os produtos.",
      });
    }
  }, []);

  useEffect(() => {
    fetchProducts();
  }, [fetchProducts]);

  return {
    ...state,
    reload: fetchProducts,
  };
}

// import { useReducer, useEffect, useCallback } from "react";
// import { getAllProducts } from "@/services/products/get-all-products";
// import { Product } from "@/types/product";

// interface ProductsState {
//   products: Product[];
//   loading: boolean;
//   error: string | null;
// }

// type ProductsAction =
//   | { type: "FETCH_START" }
//   | { type: "FETCH_SUCCESS"; payload: Product[] }
//   | { type: "FETCH_ERROR"; payload: string };

// const initialState: ProductsState = {
//   products: [],
//   loading: true,
//   error: null,
// };

// function productsReducer(
//   state: ProductsState,
//   action: ProductsAction
// ): ProductsState {
//   switch (action.type) {
//     case "FETCH_START":
//       return { ...state, loading: true, error: null };
//     case "FETCH_SUCCESS":
//       return { ...state, loading: false, products: action.payload };
//     case "FETCH_ERROR":
//       return { ...state, loading: false, error: action.payload };
//     default:
//       return state;
//   }
// }

// export function useProducts() {
//   const [state, dispatch] = useReducer(productsReducer, initialState);

//   const fetchProducts = useCallback(async () => {
//     dispatch({ type: "FETCH_START" });

//     try {
//       const response = await getAllProducts();
//       console.log("Dados da API useProduct:", response);

//       if (!response.success || !response.data) {
//         throw new Error("Erro ao carregar os produtos.");
//       }

//       dispatch({ type: "FETCH_SUCCESS", payload: response.data });
//     } catch (error) {
//       dispatch({
//         type: "FETCH_ERROR",
//         payload:
//           error instanceof Error
//             ? error.message
//             : "Erro inesperado ao carregar os produtos.",
//       });
//     }
//   }, []);

//   useEffect(() => {
//     fetchProducts();
//   }, [fetchProducts]);

//   return {
//     ...state,
//     reload: fetchProducts,
//   };
// }
