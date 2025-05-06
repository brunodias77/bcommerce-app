"use client"
import { Product } from "@/types/product";
import React, { useEffect, useRef } from "react";
import ProductCard from "../product-card/product-card";
import Title from "../ui/Title";
import Section from "../ui/Section";
import { useProductsContext } from "@/context/products-context";

const NewProducstSection: React.FC = () => {
    const { products } = useProductsContext();
    const [PopularProducts, setPopularProducts] = React.useState<Product[]>([])
    const carouselRef = useRef<HTMLDivElement>(null)

    useEffect(() => {
        if (!products) return
        const data = [...products]
            .sort((a: Product, b: Product) => {
                return new Date(b.createdAt).getTime() - new Date(a.createdAt).getTime();
            })
            .slice(0, 7)
            .map((product) => ({
                ...product,
                isNew: true,
            }));
        setPopularProducts(data)
    }, [products])

    // Autoplay logic
    useEffect(() => {
        const interval = setInterval(() => {
            if (!carouselRef.current) return
            const container = carouselRef.current
            const slideWidth = container.firstChild instanceof HTMLElement
                ? container.firstChild.offsetWidth + 24 // slide width + margin-right
                : 0

            const nextScroll = container.scrollLeft + slideWidth
            if (nextScroll >= container.scrollWidth - container.clientWidth) {
                container.scrollTo({ left: 0, behavior: 'smooth' })
            } else {
                container.scrollTo({ left: nextScroll, behavior: 'smooth' })
            }
        }, 2500)

        return () => clearInterval(interval)
    }, [PopularProducts])

    return (
        <Section>
            <div className='container'>
                <Title
                    title='Novos'
                    subtitle=' Produtos'
                    content='Explore os lançamentos mais recentes, selecionados para transformar sua rotina com inovação e estilo.'
                    titleStyles='pb-1'
                    contentStyles='block'
                    styles='block pb-10'
                />
                <div
                    ref={carouselRef}
                    className="flex overflow-x-auto snap-x snap-mandatory space-x-4 scroll-smooth hide-horizontal-scrollbar "
                >
                    {PopularProducts.map((product) => (
                        <div
                            key={product.id}
                            className="snap-start shrink-0 w-[200px] md:w-[250] xl:w-[280px]"
                        >
                            <ProductCard {...product} />
                        </div>
                    ))}
                </div>
            </div>
        </Section>

    );

}
export default NewProducstSection;