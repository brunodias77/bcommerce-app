"use client"
import React, { useEffect } from "react";
import Section from "../ui/Section";
import Title from "../ui/Title";
import { Product } from "@/types/product";
import { products } from "@/data";
import ProductCard from "../product-card/product-card";

const PopularProductsSection: React.FC = () => {
    const [PopularProducts, setPopularProducts] = React.useState<Product[]>([]);
    useEffect(() => {
        if (!products) return
        const data = products.slice(0, 7)
        setPopularProducts(data)
    }, [products])

    return (
        <Section>
            <div className="container">
                <Title
                    title='Novos'
                    subtitle=' Produtos'
                    content='Explore os lançamentos mais recentes, selecionados para transformar sua rotina com inovação e estilo.'
                    titleStyles='pb-1'
                    contentStyles='block'
                    styles='block pb-10'
                />

                <div className='grid grid-cols-1 xs:grid-cols-2 sm:grid-cols-3 md:grid-cols-4 lg:grid-cols-5 gap-8 '>
                    {PopularProducts.map((product) => (
                        <div key={product._id} className="snap-start shrink-0 w-[200px] md:w-[250] xl:w-[280px]"
                        >
                            <ProductCard {...product} />
                        </div>
                    ))}
                </div>
            </div>
        </Section>
    );
}
export default PopularProductsSection;