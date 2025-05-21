import BagIcon from '@/icons/bag-icon';
import CartIcon from '@/icons/cart-icon';
import SummaryIcon from '@/icons/summary-icon';
import UserIcon from '@/icons/user-icon';
import { Metadata } from 'next';
import Link from 'next/link';
import Image from 'next/image';
import Divider from '@/components/ui/divider';
import Button from '@/components/ui/button';
import ArrowRigthIcon from '@/icons/arrow-rigth-icon';

export const metadata: Metadata = {
    title: 'Bcommercer | Favoritos',
    description: 'Pagina de carrinho do Bcommerce',
};

export default async function FavoritesPage() {
    const products = null;
    return (
        <div className="w-full flex flex-col md:flex-row bg-[#F2F3F4] flex-1 py-4">
            <div className='container py-10'>
                <div className='flex items-center gap-x-4 mb-6'>
                    <CartIcon color="#fec857" height={25} width={25} />
                    <h2 className="text-blue-primary font-bold text-2xl uppercase">
                        Carrinho de compras
                    </h2>
                </div>
                {products != null ? (
                    <div>
                        carrinho vazio
                    </div>
                ) : (
                    <div className='grid grid-cols-1 md:grid-cols-[1fr_400px] gap-x-8 animeLeft'>
                        <div className='flex flex-col bg-white rounded p-4'>
                            <div className='flex items-center gap-x-4 mb-6'>
                                <BagIcon color="#fec857" height={18} width={18} />
                                <h2 className="text-blue-primary font-bold  uppercase">
                                    Produtos
                                </h2>
                            </div>
                            <div className='flex items-center gap-x-4'>
                                <Image src={'/assets/products-images/product_1.png'} alt='foto do produto' width={80} height={80} />
                                <div className='flex flex-col'>
                                    <span className='font-bold text-blue-primary'>Galaxy X 2025</span>
                                    <span className='text-gray-tertiary text-xs'>quantidade: 1</span>
                                </div>
                            </div>
                        </div>

                        <div className='flex flex-col bg-white rounded p-4 max-w-[400px]'>
                            <div className='flex items-center gap-x-4 mb-6'>
                                <SummaryIcon color="#fec857" height={18} width={18} />
                                <h2 className="text-blue-primary font-bold  uppercase">
                                    Resumo
                                </h2>
                            </div>
                            <div className='flex items-center justify-between my-2'>
                                <span className='text-sm text-gray-tertiary'>Valor dos produtos:</span>
                                <span className='font-bold text-blue-primary'>R$ 2.000,00</span>
                            </div>
                            <Divider dashedLine={false} gradientEdge={false} />
                            <div className='flex items-center justify-between my-2'>
                                <span className='text-sm text-gray-tertiary'>Descontos:</span>
                                <span className='font-bold text-blue-primary'>R$ 0,00</span>
                            </div>
                            <Divider dashedLine={false} gradientEdge={false} />
                            <div className='flex items-center justify-between my-2'>
                                <span className='text-sm text-gray-tertiary'>Frete:</span>
                                <span className='font-bold text-blue-primary'>R$ 0,00</span>
                            </div>
                            <Divider dashedLine={false} gradientEdge={false} />
                            <div className='flex items-center justify-between mt-2'>
                                <span className='text-sm text-gray-tertiary'>Total a prazo:</span>
                                <span className='font-bold text-blue-primary'>R$ 2.000,00</span>
                            </div>
                            <div className='text-right'>
                                <span className='text-gray-tertiary text-xs '>(em ate <span className='font-bold text-blue-primary'>10x</span> de <span className='font-bold text-blue-primary'>R$ 200,00</span> sem juros)</span>
                            </div>
                            <Divider dashedLine={false} gradientEdge={false} />
                            <div className='flex items-center justify-between mt-2'>
                                <span className='text-sm text-gray-tertiary'>Total à vista no Pix:</span>
                                <span className='font-bold text-blue-primary'>R$ 1.800,00</span>
                            </div>
                            <div className='text-right'>
                                <span className='text-gray-tertiary text-xs '>(Economize: <span className='font-bold text-blue-primary'>R$ 200,00</span> )</span>
                            </div>
                            <button className=" focus:outline-none transition transform active:scale-95 mt-4 border border-black-primary  rounded py-2 px-4 text-base cursor-pointer btn-hover-fill group">
                                <Link href={'/carrinho/endereco'} className='uppercase  text-black-primary group-hover:text-white transition-colors duration-300'>
                                    Ir para o endereço
                                </Link>
                            </button>
                            <Button className='mt-4' >
                                <Link href={'/'} className='w-full h-full flex items-center justify-center uppercase'>
                                    Continuar comprando
                                </Link>
                            </Button>

                        </div>
                    </div>
                )}

            </div>
        </div >
    )
}

