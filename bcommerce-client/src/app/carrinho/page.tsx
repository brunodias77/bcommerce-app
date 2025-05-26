import { Metadata } from 'next';
import Link from 'next/link';
import Image from 'next/image';

import BagIcon from '@/icons/bag-icon';
import CartIcon from '@/icons/cart-icon';
import SummaryIcon from '@/icons/summary-icon';
import ArrowLeftIcon from '@/icons/arrow-left-icon';
import ArrowRigthIcon from '@/icons/arrow-rigth-icon';
import TrashIcon from '@/icons/trash-icon';

import Divider from '@/components/ui/divider';
import Button from '@/components/ui/button';
import ProductCartItemCard from '@/components/cart/product-cart-item-card';
import CupomInput from '@/components/cart/cupom-input';

export const metadata: Metadata = {
    title: 'Bcommercer | Favoritos',
    description: 'Pagina de carrinho do Bcommerce',
};

export default async function CartPage() {
    const products = null;

    return (
        <div className="w-full flex flex-col md:flex-row bg-[#F2F3F4] flex-1 py-4">
            <div className="container py-10">
                <div className="flex items-center gap-x-4 mb-6">
                    <CartIcon color="#fec857" height={25} width={25} />
                    <h2 className="text-blue-primary font-bold text-2xl uppercase">
                        Carrinho de compras
                    </h2>
                </div>

                {products !== null ? (
                    <div>carrinho vazio</div>
                ) : (
                    <div className="grid grid-cols-1 md:grid-cols-[1fr_400px] gap-x-8 animeLeft">
                        {/* Produtos */}
                        <div className="flex flex-col items-center">
                            <div className="flex flex-col bg-white rounded p-4 w-full">
                                <div className="flex items-center gap-x-4 mb-6">
                                    <BagIcon color="#fec857" height={18} width={18} />
                                    <h2 className="text-blue-primary font-bold uppercase">
                                        Produtos
                                    </h2>
                                </div>
                                <Divider dashedLine={false} gradientEdge={false} />
                                {/* Produto com componente reutilizável */}
                                <ProductCartItemCard
                                    brand="Samsung"
                                    name="Galaxy X 2025"
                                    imageUrl="/assets/products-images/product_1.png"
                                    quantity={1}
                                    pricePix="R$ 5.426,22"
                                    priceDescription="Parcelado no cartão, à vista ou PIX:"
                                />

                            </div>

                            <div className="px-4 py-2 bg-white flex items-center justify-between rounded mt-4 w-full">
                                <CupomInput />
                            </div>
                        </div>

                        {/* Resumo */}
                        <div className="flex flex-col bg-white rounded p-4 max-w-[400px]">
                            <div className="flex items-center gap-x-4 mb-6">
                                <SummaryIcon color="#fec857" height={18} width={18} />
                                <h2 className="text-blue-primary font-bold uppercase">Resumo</h2>
                            </div>

                            <div className="flex items-center justify-between my-2">
                                <span className="text-sm text-gray-tertiary">Valor dos produtos:</span>
                                <span className="font-bold text-blue-primary">R$ 2.000,00</span>
                            </div>

                            <Divider dashedLine={false} gradientEdge={false} />
                            <div className="flex items-center justify-between my-2">
                                <span className="text-sm text-gray-tertiary">Descontos:</span>
                                <span className="font-bold text-blue-primary">R$ 0,00</span>
                            </div>

                            <Divider dashedLine={false} gradientEdge={false} />
                            <div className="flex items-center justify-between my-2">
                                <span className="text-sm text-gray-tertiary">Frete:</span>
                                <span className="font-bold text-blue-primary">R$ 0,00</span>
                            </div>

                            <Divider dashedLine={false} gradientEdge={false} />
                            <div className="flex items-center justify-between mt-2">
                                <span className="text-sm text-gray-tertiary">Total a prazo:</span>
                                <span className="font-bold text-blue-primary">R$ 2.000,00</span>
                            </div>

                            <div className="text-right text-xs text-gray-tertiary">
                                (em até <span className="font-bold text-blue-primary">10x</span> de{' '}
                                <span className="font-bold text-blue-primary">R$ 200,00</span> sem juros)
                            </div>

                            <Divider dashedLine={false} gradientEdge={false} />

                            <div className="flex items-center justify-between mt-2">
                                <span className="text-sm text-gray-tertiary">Total à vista no Pix:</span>
                                <span className="font-bold text-blue-primary">R$ 1.800,00</span>
                            </div>

                            <div className="text-right text-xs text-gray-tertiary">
                                (Economize: <span className="font-bold text-blue-primary">R$ 200,00</span>)
                            </div>

                            <button className="mt-4 border border-black-primary rounded py-2 px-4 text-base cursor-pointer btn-hover-fill group transition active:scale-95">
                                <Link
                                    href="/carrinho/endereco"
                                    className="uppercase text-black-primary group-hover:text-white transition-colors duration-300"
                                >
                                    Ir para o endereço
                                </Link>
                            </button>

                            <Button className="mt-4">
                                <Link href="/" className="w-full h-full flex items-center justify-center uppercase">
                                    Continuar comprando
                                </Link>
                            </Button>
                        </div>
                    </div>
                )}
            </div>
        </div>
    );
}


// import BagIcon from '@/icons/bag-icon';
// import CartIcon from '@/icons/cart-icon';
// import SummaryIcon from '@/icons/summary-icon';
// import UserIcon from '@/icons/user-icon';
// import { Metadata } from 'next';
// import Link from 'next/link';
// import Image from 'next/image';
// import Divider from '@/components/ui/divider';
// import Button from '@/components/ui/button';
// import ArrowRigthIcon from '@/icons/arrow-rigth-icon';
// import TashIcon from '@/icons/trash-icon';
// import ArrowLeftIcon from '@/icons/arrow-left-icon';
// import ProductCartItemCard from '@/components/cart/product-cart-item-card';

// export const metadata: Metadata = {
//     title: 'Bcommercer | Favoritos',
//     description: 'Pagina de carrinho do Bcommerce',
// };

// export default async function FavoritesPage() {
//     const products = null;
//     return (
//         <div className="w-full flex flex-col md:flex-row bg-[#F2F3F4] flex-1 py-4">
//             <div className='container py-10'>
//                 <div className='flex items-center gap-x-4 mb-6'>
//                     <CartIcon color="#fec857" height={25} width={25} />
//                     <h2 className="text-blue-primary font-bold text-2xl uppercase">
//                         Carrinho de compras
//                     </h2>
//                 </div>
//                 {products != null ? (
//                     <div>
//                         carrinho vazio
//                     </div>
//                 ) : (
//                     <div className='grid grid-cols-1 md:grid-cols-[1fr_400px] gap-x-8 animeLeft'>
//                         <div className='flex flex-col items-center'>
//                             <div className='flex flex-col bg-white rounded p-4 w-full'>
//                                 <div className='flex items-center gap-x-4 mb-6'>
//                                     <BagIcon color="#fec857" height={18} width={18} />
//                                     <h2 className="text-blue-primary font-bold  uppercase">
//                                         Produtos
//                                     </h2>
//                                 </div>
//                                 <Divider dashedLine={false} gradientEdge={false} />
//                                 <div className='flex items-center justify-between gap-x-4 mb-4'>
//                                     <div className='flex items-center gap-x-4'>
//                                         <Image src={'/assets/products-images/product_1.png'} alt='foto do produto' width={80} height={80} />
//                                         <div className='flex flex-col'>
//                                             <span className='text-gray-tertiary text-xs'>Samsung</span>
//                                             <span className='font-bold text-blue-primary'>Galaxy X 2025</span>
//                                             <span className='text-gray-tertiary text-xs'>Parcelado no cartão, à vista ou PIX: <span className='font-bold text-blue-primary'>R$ 5.426,22</span></span>
//                                         </div>
//                                     </div>

//                                     <div className='flex flex-col items-center gap-y-1 '>
//                                         <span className='text-sm text-gray-tertiary'>Quant.</span>
//                                         <div className='flex items-center gap-x-6'>
//                                             <button className='cursor-pointer  hover:brightness-50 transition-all duration-300'>
//                                                 <ArrowLeftIcon color="#4b5966" height={15} width={15} />
//                                             </button>
//                                             <span>1</span>
//                                             <button className='cursor-pointer'>
//                                                 <ArrowRigthIcon color="#4b5966" height={15} width={12} />
//                                             </button>
//                                         </div>
//                                         <button className='flex items-center gap-x-2 cursor-pointer'>
//                                             <TashIcon color="#fec857" height={16} width={16} />
//                                             <span className='text-sm text-yellow-primary font-bold'>Remover</span>
//                                         </button>
//                                     </div>

//                                     <div className='flex flex-col items-center gap-y-1 '>
//                                         <span className='text-sm text-gray-tertiary'>Preço à vista no PIX:</span>
//                                         <span className='font-bold text-lg text-yellow-primary'>R$ 5.426,22</span>
//                                     </div>

//                                 </div>


//                                 <ProductCartItemCard
//                                     brand="Samsung"
//                                     name="Galaxy X 2025"
//                                     imageUrl="/assets/products-images/product_1.png"
//                                     quantity={1}
//                                     pricePix="R$ 5.426,22"
//                                     priceDescription="Parcelado no cartão, à vista ou PIX:"
//                                     onDecrease={() => console.log('Diminuir quantidade')}
//                                     onIncrease={() => console.log('Aumentar quantidade')}
//                                     onRemove={() => console.log('Remover item')}
//                                 />

//                                 <Divider dashedLine={false} />
//                                 <div className='flex items-center gap-x-4 mb-4'>
//                                     <Image src={'/assets/products-images/product_1.png'} alt='foto do produto' width={80} height={80} />
//                                     <div className='flex flex-col'>
//                                         <span className='text-gray-tertiary text-xs'>Samsung</span>
//                                         <span className='font-bold text-blue-primary'>Galaxy X 2025</span>
//                                         <span className='text-gray-tertiary text-xs'>Parcelado no cartão, à vista ou PIX: <span className='font-bold text-blue-primary'>R$ 5.426,22</span></span>
//                                     </div>
//                                 </div>
//                             </div>

//                             <div className='px-4 py-2 bg-white flex items-center justify-between rounded mt-4 w-full'>
//                                 <span>Cupom</span>
//                             </div>
//                         </div>


//                         <div className='flex flex-col bg-white rounded p-4 max-w-[400px]'>
//                             <div className='flex items-center gap-x-4 mb-6'>
//                                 <SummaryIcon color="#fec857" height={18} width={18} />
//                                 <h2 className="text-blue-primary font-bold  uppercase">
//                                     Resumo
//                                 </h2>
//                             </div>
//                             <div className='flex items-center justify-between my-2'>
//                                 <span className='text-sm text-gray-tertiary'>Valor dos produtos:</span>
//                                 <span className='font-bold text-blue-primary'>R$ 2.000,00</span>
//                             </div>
//                             <Divider dashedLine={false} gradientEdge={false} />
//                             <div className='flex items-center justify-between my-2'>
//                                 <span className='text-sm text-gray-tertiary'>Descontos:</span>
//                                 <span className='font-bold text-blue-primary'>R$ 0,00</span>
//                             </div>
//                             <Divider dashedLine={false} gradientEdge={false} />
//                             <div className='flex items-center justify-between my-2'>
//                                 <span className='text-sm text-gray-tertiary'>Frete:</span>
//                                 <span className='font-bold text-blue-primary'>R$ 0,00</span>
//                             </div>
//                             <Divider dashedLine={false} gradientEdge={false} />
//                             <div className='flex items-center justify-between mt-2'>
//                                 <span className='text-sm text-gray-tertiary'>Total a prazo:</span>
//                                 <span className='font-bold text-blue-primary'>R$ 2.000,00</span>
//                             </div>
//                             <div className='text-right'>
//                                 <span className='text-gray-tertiary text-xs '>(em ate <span className='font-bold text-blue-primary'>10x</span> de <span className='font-bold text-blue-primary'>R$ 200,00</span> sem juros)</span>
//                             </div>
//                             <Divider dashedLine={false} gradientEdge={false} />
//                             <div className='flex items-center justify-between mt-2'>
//                                 <span className='text-sm text-gray-tertiary'>Total à vista no Pix:</span>
//                                 <span className='font-bold text-blue-primary'>R$ 1.800,00</span>
//                             </div>
//                             <div className='text-right'>
//                                 <span className='text-gray-tertiary text-xs '>(Economize: <span className='font-bold text-blue-primary'>R$ 200,00</span> )</span>
//                             </div>
//                             <button className=" focus:outline-none transition transform active:scale-95 mt-4 border border-black-primary  rounded py-2 px-4 text-base cursor-pointer btn-hover-fill group">
//                                 <Link href={'/carrinho/endereco'} className='uppercase  text-black-primary group-hover:text-white transition-colors duration-300'>
//                                     Ir para o endereço
//                                 </Link>
//                             </button>
//                             <Button className='mt-4' >
//                                 <Link href={'/'} className='w-full h-full flex items-center justify-center uppercase'>
//                                     Continuar comprando
//                                 </Link>
//                             </Button>

//                         </div>
//                     </div>
//                 )}

//             </div>
//         </div >
//     )
// }

