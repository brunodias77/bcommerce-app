// app/product/[id]/product-viewer.tsx
"use client";

import StarRating from "@/components/star-rating/star-rating";
import Button from "@/components/ui/button";
import { useCartContext } from "@/context/cart-context";
import CartIcon from "@/icons/cart-icon";
import FavoriteIcon from "@/icons/favorite-icon";
import TruckIcon from "@/icons/truck-icon";
import { Product, ProductItem } from "@/types/product";
import { useState } from "react";



export default function ProductViewer({ product }: { product: ProductItem }) {
    const [image, setImage] = useState(product.images?.[0] || "");
    const [isFavorite, setIsFavorite] = useState(false);
    const { addToCart } = useCartContext(); // 👈 hook do carrinho

    return (
        <div className="container my-4">
            <div className="grid grid-cols-1 md:grid-cols-2 rounded-2xl p-3 mb-6 gap-4">
                <div className='flex flex-1 gap-x-2 max-w-[577px]'>
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
                <div className=" flex flex-col gap-1 rounded-2xl px-5 py-3 bg-primary">
                    <h3 className="h1 leading-none text-4xl font-bold text-blue-primary">{product.name}</h3>
                    <div className="flex items-center  gap-x-2 mt-4">
                        <span className="text-[8px] md:text-[11px] text-gray-500 text-center">4.5</span>
                        <StarRating size="text-[10px] md:text-[15px]" />
                        <span className="text-[8px] md:text-[11px] text-gray-500 text-center">(4)</span>
                    </div>
                    <span className="text-gray-tertiary">Vendido e entrege por: <span className="text-blue-primary font-bold">Bcommerce</span> | <span className="text-yellow-primary font-bold">Em estoque</span></span>
                    <div className="flex items-center gap-x-2 ">
                        <TruckIcon width={25} height={25} color="#fec857" />
                        <span className="text-xs text-gray-tertiary"><span className="font-bold  text-yellow-primary">FRETE GRATÍS*</span> -  consulte disponibilidade de seu CEP</span>
                    </div>
                    <span className="  text-gray-tertiary line-through mt-4">R$100,00</span>
                    <div className="flex items-center gap-x-6">
                        <span className="text-yellow-primary font-bold text-4xl">R$ 89,99</span>
                        <Button variant="secondary" onClick={(e) => {
                            e.preventDefault();
                            e.stopPropagation();
                            addToCart(product.id, product.colors[0]);
                        }} >
                            <CartIcon color="#FFF" height={20} width={20} />
                            <span className="text-lg font-bold text-white">Adicionar ao carrinho</span>
                        </Button>
                        <button >
                            <FavoriteIcon width={25} height={25} isFavorite={isFavorite} />
                        </button>
                    </div>
                    <span className="text-gray-tertiary text-sm">À vista no PIX com <span className=" text-yellow-primary font-bold">30% OFF</span></span>
                    <span className="text-base text-blue-primary font-bold mt-4">R$ 99,00</span>
                    <span className="text-gray-tertiary">Em até 10x de <span className="text-blue-primary font-bold">R$ 9,00</span> sem juros no cartão</span>
                    <span className="text-gray-tertiary">Ou em 1x no cartão com  <span className=" text-blue-primary font-bold">30% OFF</span> </span>
                    <span className="underline text-gray-tertiary cursor-pointer mt-4">Ver mais opções de pagamento</span>
                </div>
            </div>

            <div id="description" className="w-full bg-gray-300 z-50">
                <span>Descricao do produto</span>
            </div>
        </div>
    );
}
