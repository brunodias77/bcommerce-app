import { ProductID } from "./product";

/**
 * CartItem
 *
 * Representa a estrutura de um item do carrinho.
 * O carrinho é modelado como um objeto onde a chave é o `ProductID`,
 * e o valor é outro objeto que mapeia a `Color` para a `quantidade`.
 *
 * Exemplo de estrutura:
 * ```ts
 * {
 *   "abc123": {
 *     "vermelho": 2,
 *     "azul": 1
 *   },
 *   "xyz789": {
 *     "preto": 3
 *   }
 * }
 * ```
 */
export type CartItems = {
  [productId in ProductID]?: {
    [color: string]: number;
  };
};
