"use client"; // 🔥 Next.js instrução: este arquivo é executado no cliente (não no servidor)

import { createContext, useContext, useReducer, useEffect, useMemo, ReactNode } from "react";
import { Product, ProductItem } from "@/types/product";
import { getAllProducts } from "@/services/products/get-all-products";

// ===============================
// 🎯 Definição do estado global
// ===============================
interface ProductsState {
    products: Product[]; // Lista de produtos carregados
    loading: boolean;     // Indicador se está carregando
    error: string | null; // Mensagem de erro, se houver
}

// 🎯 Definição dos tipos de ação que o reducer pode receber
type ProductsAction =
    | { type: "FETCH_START" }                  // Quando começa a buscar os produtos
    | { type: "FETCH_SUCCESS"; payload: Product[] } // Quando busca com sucesso
    | { type: "FETCH_ERROR"; payload: string }; // Quando ocorre erro

// 🎯 Estado inicial
const initialState: ProductsState = {
    products: [],
    loading: true,
    error: null,
};

// ===============================
// 🛠️ Função Reducer
// ===============================
function productsReducer(state: ProductsState, action: ProductsAction): ProductsState {
    switch (action.type) {
        case "FETCH_START":
            return { ...state, loading: true, error: null }; // Começou carregar
        case "FETCH_SUCCESS":
            return { products: action.payload, loading: false, error: null }; // Carregou com sucesso
        case "FETCH_ERROR":
            return { ...state, loading: false, error: action.payload }; // Falhou na carga
        default:
            return state; // Estado atual, caso a ação não seja reconhecida
    }
}

// ===============================
// 🧠 Contexto para acesso global
// ===============================
interface ProductsContextData extends ProductsState {
    reload: () => Promise<void>; // Função para recarregar os produtos
}

const ProductsContext = createContext<ProductsContextData | undefined>(undefined);

// ===============================
// 🔄 Função para mapear ProductItem (API) -> Product (Frontend)
// ===============================
function mapProductItemToProduct(item: ProductItem): Product {
    return {
        id: item.id,
        name: item.name,
        description: item.description,
        price: { amount: item.price },
        oldPrice: item.oldPrice !== null && item.oldPrice !== undefined ? { amount: item.oldPrice } : null,
        images: item.images.map((url) => ({ url })), // mapeando urls em objetos { url }
        category: { name: item.categoryName },
        colors: item.colors.map((color) => ({ value: color })), // transformando string em objeto { value }
        stock: { quantity: item.stockQuantity }, // stock quantity
        sold: item.sold,
        isActive: item.isActive,
        popular: item.popular,
        createdAt: item.createdAt,
        updatedAt: item.updatedAt,
        isNew: false, // você pode implementar lógica baseada no createdAt
        sale: item.oldPrice !== null, // se tem oldPrice, é promoção (sale)
    };
}

// ===============================
// 🌟 Provider para envolver a aplicação
// ===============================
export function ProductsProvider({ children }: { children: ReactNode }) {
    const [state, dispatch] = useReducer(productsReducer, initialState); // useReducer para gerenciar o estado de forma mais controlada

    // Função que busca os produtos
    async function loadProducts() {
        dispatch({ type: "FETCH_START" }); // começa carregamento
        try {
            const response = await getAllProducts(); // faz request
            if (response.success) {
                const productsMapped = response.data.products.map(mapProductItemToProduct); // transforma a resposta da API
                dispatch({ type: "FETCH_SUCCESS", payload: productsMapped }); // sucesso
            } else {
                dispatch({ type: "FETCH_ERROR", payload: "Erro ao carregar os produtos." }); // erro vindo da API
            }
        } catch (error) {
            console.error("Erro ao buscar produtos:", error);
            dispatch({ type: "FETCH_ERROR", payload: "Erro inesperado ao carregar os produtos." }); // erro de rede ou inesperado
        }
    }

    // useEffect para carregar automaticamente quando o Provider for montado
    useEffect(() => {
        loadProducts();
    }, []);

    // useMemo para otimizar o contexto e evitar re-renderizações desnecessárias
    const value = useMemo(
        () => ({
            ...state,
            reload: loadProducts, // expõe a função reload também
        }),
        [state.products, state.loading, state.error]
    );

    return <ProductsContext.Provider value={value}>{children}</ProductsContext.Provider>;
}

// ===============================
// 🪝 Hook customizado para usar o contexto de forma segura
// ===============================
export function useProducts() {
    const context = useContext(ProductsContext);
    if (!context) {
        throw new Error("useProducts deve ser usado dentro de um ProductsProvider.");
    }
    return context;
}


// 📖 Explicação — useReducer e useMemo no código
// 🛠️ useReducer no código(const [state, dispatch] = useReducer(productsReducer, initialState);)

//     O que faz ?
//     Gerencia todo o estado relacionado a produtos(products, loading e error) dentro do contexto.

//     Por que usar ?
//     Como temos várias partes do estado que mudam juntas(products, loading, error), useReducer facilita:

//         Atualizar tudo de forma consistente;

//         Manter o código mais previsível e organizado;

//         Evitar múltiplos useState() espalhados.

//     Como funciona ?

//     Quando chamamos dispatch({ type: "FETCH_START" }), o productsReducer entende a ação "FETCH_START" e atualiza o estado(loading: true, error: null, etc).

//         Tudo isso centralizado e seguro ✅.

// 🚀 useMemo no código(const value = useMemo(() => ({ ...state, reload: loadProducts }), [state.products, state.loading, state.error]);)

//     O que faz ?
//     Memoriza o valor do contexto(products, loading, error, reload) para que o provider(ProductsContext.Provider) só re - renderize quando algum desses realmente mudar.

//     Por que usar ?

//     Sem useMemo, cada re - render do ProductsProvider geraria um novo objeto de value, mesmo se o estado não tivesse mudado.

//         Isso forçaria re - renderizações desnecessárias nos componentes filhos que usam useProducts().

//         Otimização real de performance 🏎️💨.

//     Como funciona ?

//     Se products, loading ou error mudarem, o React recria value.

//         Se não mudarem, o React reaproveita a versão memorizada.

// 🧠 Em resumo:
// Hook	Papel no Código	Benefício Principal
// useReducer	Controla as mudanças de estado complexas(products, loading, error)	Deixa o estado previsível e a atualização organizada
// useMemo	Memoriza o valor do contexto para não re - renderizar desnecessariamente	Garante alta performance no Provider e nos componentes filhos
// 🔥 Conclusão

// Você aplicou useReducer para centralizar o gerenciamento de estado e useMemo para otimizar a performance do contexto, seguindo o que há de melhor nas práticas modernas de React / Next.js! 🚀
