// app/product/[id]/product-viewer.tsx
"use client";

import { Product, ProductItem } from "@/types/product";
import { useState } from "react";



export default function ProductViewer({ product }: { product: ProductItem }) {
    const [image, setImage] = useState(product.images?.[0] || "");

    return (
        <div className="container">
            <div className="flex gap-10 flex-col xl:flex-row rounded-2xl p-3 mb-6">
                <div className='flex flex-1 gap-x-2 max-w-[477px]'>
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
                <div className="flex-[1.5] rounded-2xl px-5 py-3 bg-primary">
                    <h3 className="h3 leading-none">{product.name}</h3>
                </div>
            </div>
        </div>
    );
}
