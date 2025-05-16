"use client"
import React, { useEffect } from "react";
import Section from "../ui/Section";
import Title from "../ui/Title";
import { Product } from "@/types/product";
import ProductCard from "../product-card/product-card";
import { useProductsContext } from "@/context/products-context";
import LoadingIcon from "@/icons/loading"; // ajuste o caminho se necessário

const PopularProductsSection: React.FC = () => {
    const [PopularProducts, setPopularProducts] = React.useState<Product[]>([]);
    const { products, loading, error } = useProductsContext();

    console.log("PopularProductsSection", products);

    useEffect(() => {
        if (!products) return;
        const data = products.slice(0, 10);
        setPopularProducts(data);
    }, [products]);

    return (
        <Section>
            <div className="container">
                <Title
                    title="Produtos"
                    subtitle=" Populares"
                    content="Explore os lançamentos mais recentes, selecionados para transformar sua rotina com inovação e estilo."
                    titleStyles="pb-1"
                    contentStyles="block"
                    styles="block pb-10"
                />

                {loading ? (
                    <div className="flex justify-center items-center py-20">
                        <div className="animate-spin w-5 h-5 border-2 border-t-transparent border-black-primary rounded-full" />
                    </div>
                ) : error ? (
                    <div className="text-red-500 text-center py-10">
                        {error}
                    </div>
                ) : (
                    <div className="grid grid-cols-1 xs:grid-cols-3 sm:grid-cols-4 md:grid-cols-5 gap-4">
                        {PopularProducts.map((product) => (
                            <ProductCard {...product} key={product.productId} />
                        ))}
                    </div>
                )}
            </div>
        </Section>

    );
};

export default PopularProductsSection;


