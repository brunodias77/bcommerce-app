// app/product/[id]/product-viewer.tsx
"use client";

import ExpandableSection from "@/components/expandable-section/expandable-section";
import PopularProductsSection from "@/components/section-popular-products/section-popular-products";
import ShippingForm from "@/components/shipping/shipping-form";
import StarRating from "@/components/star-rating/star-rating";
import Button from "@/components/ui/button";
import { useCartContext } from "@/context/cart-context";
import AlertIcon from "@/icons/alert-icon";
import ArrowDownIcon from "@/icons/arrow-down-icon";
import ArrowUpIcon from "@/icons/arrow-up-icon";
import CartIcon from "@/icons/cart-icon";
import DocumentICon from "@/icons/document-icon";
import FavoriteIcon from "@/icons/favorite-icon";
import QuestionIcon from "@/icons/question-icon";
import StarIcon from "@/icons/star-icon";
import TruckIcon from "@/icons/truck-icon";
import { Product } from "@/types/product";
import { useState } from "react";



export default function ProductViewer({ product }: { product: Product }) {
    const [image, setImage] = useState(product.images?.[0] || "");
    const [isExpanded, setIsExpanded] = useState(false);
    const [isFavorite, setIsFavorite] = useState(false);
    const { addToCart } = useCartContext(); // 👈 hook do carrinho
    const { price, oldPrice } = product;
    const installmentValue = price / 10;
    const pricePix = price - (price * 0.1);
    const toggleExpand = () => setIsExpanded(prev => !prev);


    return (
        <>
            <div className="container my-4">
                <div className="grid grid-cols-1 md:grid-cols-2 rounded-2xl p-3 mb-6 gap-4">
                    <div className='flex flex-1 gap-x-2 max-w-[600px]'>
                        <div className='flex-1 flex items-center justify-center flex-col gap-[7px] flex-wrap cursor-pointer'>
                            {
                                product.images.map((item, i) => (
                                    <img key={i} onClick={() => setImage(item)} src={item} alt="productImg" className="object-cover aspect-square rounded-lg" />
                                ))
                            }
                        </div>
                        <div className="flex-[4] flex">
                            <img src={image} alt="" className="rounded-xl" />
                        </div>

                    </div>
                    <div className=" flex flex-col gap-1  justify-center pl-8 rounded-2xl px-5 py-3 bg-primary">
                        <h3 className="h1 leading-none text-4xl font-bold text-blue-primary">{product.name}</h3>
                        <div className="flex items-center  gap-x-2 mt-2">
                            <span className="text-[8px] md:text-[11px] text-gray-500 text-center">4.5</span>
                            <StarRating size="text-[10px] md:text-[15px]" />
                            <span className="text-[8px] md:text-[11px] text-gray-500 text-center">(4)</span>
                        </div>
                        <span className="text-gray-tertiary text-sm">Vendido e entrege por: <span className="text-blue-primary font-bold">Bcommerce</span> | <span className="text-yellow-primary font-bold">Em estoque</span></span>
                        <div className="flex items-center gap-x-2 ">
                            <TruckIcon width={20} height={20} color="#fec857" />
                            <span className="text-xs text-gray-tertiary"><span className="font-bold  text-yellow-primary">FRETE GRATÍS*</span> -  consulte disponibilidade de seu CEP</span>
                        </div>
                        {oldPrice && (
                            <span className="  text-gray-tertiary line-through mt-2">{new Intl.NumberFormat('pt-BR', {
                                style: 'currency',
                                currency: 'BRL',
                            }).format(oldPrice)}</span>
                        )}
                        <div className="flex items-center gap-x-6">
                            <span className="text-yellow-primary font-bold text-4xl">{new Intl.NumberFormat('pt-BR', {
                                style: 'currency',
                                currency: 'BRL',
                            }).format(pricePix)}</span>
                            <Button variant="secondary" onClick={(e) => {
                                e.preventDefault();
                                e.stopPropagation();
                                addToCart(product.productId, product.colors[0].value);
                            }} >
                                <CartIcon color="#FFF" height={20} width={20} />
                                <span className="text-lg font-bold text-white">Adicionar ao carrinho</span>
                            </Button>
                            <button >
                                <FavoriteIcon width={25} height={25} isFavorite={isFavorite} />
                            </button>
                        </div>
                        <span className="text-gray-tertiary text-sm">À vista no PIX com <span className=" text-yellow-primary font-bold">10% OFF</span></span>
                        <span className="text-lg text-blue-primary font-bold mt-2">{new Intl.NumberFormat('pt-BR', {
                            style: 'currency',
                            currency: 'BRL',
                        }).format(price)}</span>
                        <span className="text-gray-tertiary">Em até 10x de <span className="text-blue-primary font-bold">{new Intl.NumberFormat('pt-BR', {
                            style: 'currency',
                            currency: 'BRL',
                        }).format(installmentValue)}</span> sem juros no cartão</span>
                        <span className="text-gray-tertiary">Ou em 1x no cartão com  <span className=" text-blue-primary font-bold">10% OFF</span> </span>
                        <span className="underline text-sm text-gray-tertiary cursor-pointer mt-1">Ver mais opções de pagamento</span>
                        <div className="flex flex-col gap-1 mt-2">
                            <span className="text-sm text-blue-primary font-bold">Consultar frete e prazo de entrega</span>
                            <ShippingForm />
                            <div className="flex items-center  gap-2">
                                <AlertIcon color="#fec857" height={12} width={12} />
                                <span className="text-xs text-gray-tertiary">Os prazos de entrega começam a contar a partir da confirmação de pagamento</span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <ExpandableSection
                title="Descrição do produto"
                icon={<DocumentICon color="#fec857" />}
            >
                <span>
                    Lorem ipsum dolor sit amet consectetur adipisicing elit. Voluptatibus suscipit error aut ullam recusandae voluptates...
                </span>
            </ExpandableSection>

            <ExpandableSection
                title="Informações Técnicas"
                icon={<AlertIcon color="#fec857" />}
            >
                <span>
                    Lorem ipsum dolor sit amet consectetur adipisicing elit. Voluptatibus suscipit error aut ullam recusandae voluptates...
                </span>
            </ExpandableSection>




            <ExpandableSection
                title="perguntas e respostas"
                icon={<QuestionIcon color="#fec857" />}
            >
                <span>
                    Lorem ipsum dolor sit amet consectetur adipisicing elit. Voluptatibus suscipit error aut ullam recusandae voluptates...
                </span>
            </ExpandableSection>


            <ExpandableSection
                title="Avaliações dos usuários"
                icon={<StarIcon color="#fec857" />}
                isLast={true}
            >
                <span>
                    Lorem ipsum dolor sit amet consectetur adipisicing elit. Voluptatibus suscipit error aut ullam recusandae voluptates...
                </span>
            </ExpandableSection>

            <PopularProductsSection />

        </>
    );
}
