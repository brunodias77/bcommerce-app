"use client"
import React, { useEffect } from "react";
import Section from "../ui/Section";
import Title from "../ui/Title";
import { Product } from "@/types/product";
import ProductCard from "../product-card/product-card";
import { useProductsContext } from "@/context/products-context";

const PopularProductsSection: React.FC = () => {
    const [PopularProducts, setPopularProducts] = React.useState<Product[]>([]);
    const { products } = useProductsContext();
    useEffect(() => {
        if (!products) return
        const data = products.slice(0, 7)
        setPopularProducts(data)
    }, [products])

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

                <div className='grid grid-cols-1 xs:grid-cols-2 sm:grid-cols-3 md:grid-cols-4  gap-x-4 '>
                    {PopularProducts.map((product) => (
                        <div key={product.id} className="snap-start shrink-0 w-[200px] md:w-[250] xl:w-[280px]"
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