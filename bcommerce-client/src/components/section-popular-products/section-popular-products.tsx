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
    const { products, loading } = useProductsContext();

    useEffect(() => {
        if (!products) return;
        const data = products.slice(0, 10);
        setPopularProducts(data);
    }, [products]);

    return (
        <Section>
            <div className="container">
                <Title
                    title='Produtos'
                    subtitle=' Populares'
                    content='Explore os lançamentos mais recentes, selecionados para transformar sua rotina com inovação e estilo.'
                    titleStyles='pb-1'
                    contentStyles='block'
                    styles='block pb-10'
                />

                {loading ? (
                    <div className="flex justify-center items-center py-20">
                        <LoadingIcon width={40} height={40} color="#555" />
                    </div>
                ) : (
                    <div className='grid grid-cols-1 xs:grid-cols-3 sm:grid-cols-4 md:grid-cols-5 gap-4'>
                        {PopularProducts.map((product) => (

                            <ProductCard {...product} key={product.id} />
                        ))}
                    </div>
                )}
            </div>
        </Section>
    );
};

export default PopularProductsSection;


