"use client";

import React, { useState } from "react";
import Image from "next/image";
import Link from "next/link";
import { Product } from "@/types/product";
import StarRating from "../star-rating/star-rating";
import FireIcon from "@/icons/fire-icon";
import FavoriteIcon from "@/icons/favorite-icon";
import { formatPrice } from "@/utils/format-price";

const Badge = ({ text, bgColor, top }: { text: string; bgColor: string; top: string }) => (
    <div className={`absolute ${top} left-2 ${bgColor} text-white text-xs px-2 py-1 rounded font-bold`}>
        {text}
    </div>
);

const ProductCard: React.FC<Product> = ({ id, images, name, price, category, isNew, oldPrice }) => {
    const [isFavorite, setIsFavorite] = useState(false);

    const isOnSale = oldPrice?.amount != null && oldPrice.amount > price.amount;
    const discount = isOnSale
        ? Math.round(((oldPrice!.amount - price.amount) / oldPrice!.amount) * 100)
        : null;

    const productImage = images[0];
    console.log(category, "category");

    return (
        <Link href={`/product/${id}`}>
            <div className="relative flex flex-col items-center group w-full min-h-[399px] h-full border border-gray-200 hover:border-yellow-primary rounded-lg shadow-sm overflow-hidden cursor-pointer">
                {isOnSale && <div className="absolute top-2 left-2 z-30 text-xs text-white font-bold bg-yellow-primary rounded-lg p-[4px]">OFERTA</div>}
                <button onClick={(e) => {
                    e.stopPropagation();
                    e.preventDefault();
                    setIsFavorite((prev) => !prev);
                }}
                    className="z-20 absolute top-2 right-2 text-white rounded-full p-1 cursor-pointer transition transform active:scale-95 flex items-center"
                >
                    <FavoriteIcon isFavorite={isFavorite} />
                </button>
                <div className="relative w-full h-[220px] overflow-hidden rounded-md">
                    <Image
                        src={productImage.url}
                        alt={name}
                        fill
                        className="object-cover transition-transform duration-300 group-hover:scale-115"
                    />
                </div>


                <div className="p-3 w-full flex flex-col justify-between flex-grow">
                    <h4 className="text-[12px] md:text-[13px] mb-1 text-gray-tertiary">{category.name}</h4>
                    <h2 className="text-[16px] font-bold text-blue-primary line-clamp-2 h-[48px]">{name}</h2>
                </div>
            </div>
        </Link>

        // <div className="relative flex flex-col items-center group w-full p-4 min-h-[399px] h-full">
        //     <div className="relative z-10 overflow-hidden border border-gray-200 rounded-lg shadow-sm transition-transform hover:scale-105 duration-300 w-full h-full flex flex-col justify-between">
        //         <div className="relative">
        //             {/* BADGES */}
        //             <div className="absolute top-2 left-2">
        //                 {isOnSale && <Badge text="OFERTA" bgColor="bg-yellow-500" top="top-2" />}
        //                 {!isOnSale && <div className="h-[24px]" />} {/* Placeholder para alinhar altura */}
        //                 {isNew && <Badge text="NOVO" bgColor="bg-black-primary" top="top-10" />}
        //                 {!isNew && <div className="h-[24px]" />} {/* Placeholder para alinhar altura */}
        //             </div>

        //             <div className="absolute top-2 right-2">
        //                 <FireIcon />
        //             </div>

        //             <button
        //                 className="absolute top-2 right-2 text-white rounded-full p-1 cursor-pointer transition transform active:scale-95 flex items-center"
        //                 onClick={() => setIsFavorite(!isFavorite)}
        //             >
        //                 <FavoriteIcon isFavorite={isFavorite} />
        //             </button>

        //             <Link href={`/product/${id}`} className="block bg-gray-100 overflow-hidden w-full">
        //                 <Image
        //                     src={productImage.url}
        //                     alt={name}
        //                     width={300}
        //                     height={220}
        //                     className="transition-all duration-300 object-cover w-full h-[220px] group-hover:opacity-70"
        //                 />
        //             </Link>
        //         </div>

        //         <div className="p-3 w-full flex flex-col justify-between flex-grow">
        //             <h4 className="text-[12px] md:text-[13px] mb-1 text-gray-tertiary">{category.name}</h4>
        //             <h2 className="text-[16px] font-bold text-blue-primary line-clamp-2 h-[48px]">{name}</h2>

        //             <div className="flex items-center gap-x-2 mt-1">
        //                 <StarRating size="text-[10px] md:text-[15px]" />
        //                 <span className="text-[8px] md:text-[11px] text-gray-500">4.5</span>
        //                 <span className="text-[8px] md:text-[11px] text-gray-500">(4)</span>
        //             </div>

        //             <div className="flex items-center justify-between gap-x-2 mt-2">
        //                 <div className="flex items-center gap-x-2">
        //                     <h5 className="text-[14px] md:text-[15px] font-bold text-blue-primary">
        //                         {formatPrice(price.amount)}
        //                     </h5>
        //                     {isOnSale && oldPrice && (
        //                         <>
        //                             <span className="text-sm text-gray-tertiary line-through">
        //                                 {formatPrice(oldPrice.amount)}
        //                             </span>
        //                             <span className="text-yellow-primary font-bold text-sm">{discount}% OFF</span>
        //                         </>
        //                     )}
        //                 </div>
        //                 <button
        //                     className="text-white rounded-full p-1 cursor-pointer transition transform active:scale-95 flex items-center"
        //                     onClick={() => setIsFavorite(!isFavorite)}
        //                 >
        //                     <FavoriteIcon isFavorite={isFavorite} />
        //                 </button>
        //             </div>
        //         </div>
        //     </div>

        //     <div className="absolute w-full left-0 bottom-0 transition-all duration-300 translate-y-0 opacity-0 z-[-1] group-hover:translate-y-[120%] group-hover:opacity-100 group-hover:z-50">
        //         <button className="w-full bg-[#FEC857] hover:bg-yellow-600 text-white font-bold py-2 rounded-md shadow-md flex items-center justify-center gap-x-2">
        //             <span className="text-sm">Adicionar ao Carrinho</span>
        //         </button>
        //     </div>
        // </div>
    );
};

export default ProductCard;




// "use client";

// import React, { useState } from "react";
// import Image, { StaticImageData } from "next/image";
// import Link from "next/link";
// import Fire from "@/assets/icons/fire.svg";
// import FavoriteIcon from "@/icons/favorite-icon";
// import { Product } from "@/types/product";
// import { products } from "@/data";
// import StarRating from "../star-rating/star-rating";
// import FireIcon from "@/icons/fire-icon";

// const Badge = ({ text, bgColor, top }: { text: string; bgColor: string; top: string }) => (
//     <div className={`absolute ${top} left-2 ${bgColor} text-white text-xs px-2 py-1 rounded font-bold`}>
//         {text}
//     </div>
// );

// const ProductCard: React.FC<Product> = ({ id, images, name, price, category, isNew, oldPrice }) => {
//     const [isFavorite, setIsFavorite] = useState(false);

//     const isOnSale = oldPrice != null ? oldPrice.amount - price.amount : null;

//     const discount = oldPrice?.amount != null
//         ? Math.round(((oldPrice.amount - price.amount) / oldPrice.amount) * 100)
//         : null;
//     const productImage = products[1].images[0]

//     return (
//         <div className="relative flex flex-col items-center group w-full p-4">
//             <div className="relative z-10 overflow-hidden border border-gray-200 rounded-lg shadow-sm transition-transform hover:scale-105 duration-300 w-full">
//                 {isOnSale && (
//                     <>
//                         <Badge text="OFERTA" bgColor="bg-yellow-500" top="top-2" />
//                     </>
//                 )}
//                 {isNew && (
//                     <>
//                         <Badge text="NOVO" bgColor="bg-black-primary" top="top-10" />
//                     </>
//                 )}

//                 <div className="absolute top-2 right-2">
//                     <FireIcon />
//                 </div>

//                 <Link href={`/product/${id}`} className="block bg-gray-100 overflow-hidden w-full">
//                     <Image
//                         src={productImage.url}
//                         alt={name}
//                         width={300}
//                         height={220}
//                         className="transition-all duration-300 object-cover  group-hover:opacity-70"
//                     />
//                 </Link>

//                 <div className="p-3 w-full">
//                     <h4 className="text-[12px] md:text-[13px] mb-1 text-gray-tertiary">{category.name}</h4>
//                     <h2 className="text-[16px] font-bold text-blue-primary line-clamp-1">{name}</h2>

//                     <div className="flex items-center gap-x-2">
//                         <StarRating size="text-[10px] md:text-[15px]" />
//                         <span className="text-[8px] md:text-[11px] text-gray-500">4.5</span>
//                         <span className="text-[8px] md:text-[11px] text-gray-500">(4)</span>
//                     </div>

//                     <div className="flex items-center justify-between gap-x-2 mt-2">
//                         <div className="flex items-center gap-x-2">
//                             <h5 className="text-[14px] md:text-[15px] font-bold text-blue-primary">${price.amount}.00</h5>
//                             {isOnSale && (
//                                 <>
//                                     <span className="text-sm text-gray-tertiary line-through">${oldPrice}.00</span>
//                                     <span className=" text-yellow-primary font-bold text-sm">{discount}% OFF</span>
//                                 </>
//                             )}
//                         </div>
//                         <button
//                             className="text-white rounded-full p-1 cursor-pointer transition transform active:scale-95 flex items-center"
//                             onClick={() => setIsFavorite(!isFavorite)}
//                         >
//                             <FavoriteIcon isFavorite={isFavorite} />
//                         </button>
//                     </div>
//                 </div>
//             </div>

//             <div className="absolute w-full left-0 bottom-0 transition-all duration-300 translate-y-0 opacity-0 z-[-1] group-hover:translate-y-[120%] group-hover:opacity-100 group-hover:z-50">
//                 <button className="w-full bg-[#FEC857] hover:bg-yellow-600 text-white font-bold py-2 rounded-md shadow-md flex items-center justify-center gap-x-2">
//                     {/* <Image src={Bag} alt="Carrinho" width={20} height={20} /> */}
//                     <span className="text-sm">Adicionar ao Carrinho</span>
//                 </button>
//             </div>
//         </div>
//     );
// };

// export default ProductCard;
