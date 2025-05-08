// hooks/useProducts.ts
import { useReducer, useEffect, useCallback } from "react";
import { getAllProducts } from "@/services/products/get-all-products";
import { mapProductItemToProduct } from "@/mappers/product-mapper";
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
      return { products: action.payload, loading: false, error: null };
    case "FETCH_ERROR":
      return { ...state, loading: false, error: action.payload };
    default:
      return state;
  }
}

export function useProducts() {
  const [state, dispatch] = useReducer(productsReducer, initialState);

  const loadProducts = useCallback(async () => {
    dispatch({ type: "FETCH_START" });

    try {
      const response = await getAllProducts();
      if (response.success && response.data?.products) {
        const mapped = response.data.products.map(mapProductItemToProduct);
        dispatch({ type: "FETCH_SUCCESS", payload: mapped });
      } else {
        dispatch({
          type: "FETCH_ERROR",
          payload: "Erro ao carregar os produtos.",
        });
      }
    } catch (error) {
      dispatch({
        type: "FETCH_ERROR",
        payload: "Erro inesperado ao carregar os produtos.",
      });
    }
  }, []);

  useEffect(() => {
    loadProducts();
  }, [loadProducts]);

  return {
    ...state,
    reload: loadProducts,
  };
}

// import { mapProductItemToProduct } from "@/mappers/product-mapper";
// import { getAllProducts } from "@/services/products/get-all-products";
// import { Product } from "@/types/product";
// import { useReducer, useEffect, useCallback } from "react";

// /**
//  * Interface que define a estrutura do estado para o gerenciamento de produtos.
//  */
// interface ProductsState {
//   products: Product[];
//   loading: boolean;
//   error: string | null;
// }

// /**
//  * Tipos de ações possíveis no reducer de produtos.
//  */
// type ProductsAction =
//   | { type: "FETCH_START" } // Inicia o carregamento
//   | { type: "FETCH_SUCCESS"; payload: Product[] } // Sucesso ao buscar
//   | { type: "FETCH_ERROR"; payload: string }; // Erro na requisição

// /**
//  * Estado inicial para o reducer de produtos.
//  */
// const initialState: ProductsState = {
//   products: [],
//   loading: true,
//   error: null,
// };

// /**
//  * productsReducer
//  *
//  * Função reducer para controlar o estado relacionado ao carregamento de produtos.
//  * Lida com as ações de iniciar fetch, sucesso e erro.
//  *
//  * @param state Estado atual
//  * @param action Ação a ser processada
//  * @returns Novo estado após processar a ação
//  */
// function productsReducer(
//   state: ProductsState,
//   action: ProductsAction
// ): ProductsState {
//   switch (action.type) {
//     case "FETCH_START":
//       return { ...state, loading: true, error: null };
//     case "FETCH_SUCCESS":
//       return { products: action.payload, loading: false, error: null };
//     case "FETCH_ERROR":
//       return { ...state, loading: false, error: action.payload };
//     default:
//       return state;
//   }
// }

// /**
//  * useProducts
//  *
//  * Hook personalizado que encapsula toda a lógica de gerenciamento de estado
//  * para a listagem de produtos. Utiliza `useReducer` para lidar com os estados
//  * de carregamento (`loading`), erro (`error`) e dados (`products`), além de
//  * realizar o carregamento inicial via `getAllProducts()` ao montar o componente.
//  *
//  * Recursos:
//  * - `products`: lista de produtos carregados da API
//  * - `loading`: booleano indicando carregamento em andamento
//  * - `error`: mensagem de erro, se houver
//  * - `reload()`: função que refaz o fetch de produtos
//  *
//  * Ideal para ser utilizado dentro de contextos como `ProductsProvider`, ou
//  * diretamente em componentes que precisem de acesso ao estado de produtos.
//  */
// export function useProducts() {
//   const [state, dispatch] = useReducer(productsReducer, initialState);

//   const loadProducts = useCallback(async () => {
//     dispatch({ type: "FETCH_START" });

//     try {
//       const response = await getAllProducts();
//       if (response.success) {
//         const mapped = response.data.products.map(mapProductItemToProduct);
//         dispatch({ type: "FETCH_SUCCESS", payload: mapped });
//       } else {
//         dispatch({
//           type: "FETCH_ERROR",
//           payload: "Erro ao carregar os produtos.",
//         });
//       }
//     } catch (error) {
//       dispatch({
//         type: "FETCH_ERROR",
//         payload: "Erro inesperado ao carregar os produtos.",
//       });
//     }
//   }, []);

//   useEffect(() => {
//     loadProducts();
//   }, [loadProducts]);

//   return {
//     ...state,
//     reload: loadProducts,
//   };
// }
