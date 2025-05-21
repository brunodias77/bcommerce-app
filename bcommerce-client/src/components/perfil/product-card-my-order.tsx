import ArrowRigthIcon from '@/icons/arrow-rigth-icon';
import React from 'react';
import Image from 'next/image';
import CreditCardIcon from '@/icons/credit-card-icon';

const ProductCardMyOrder: React.FC = () => {
    return (
        <div className='container bg-white rounded flex flex-col gap-y-2 '>
            <div className='flex items-center justify-between w-full'>
                <span className='text-gray-tertiary'>Pedido: 37815081 - 06/02/2024</span>
                <button className='border hover:border-yellow-secondary border-yellow-primary rounded px-2 py-1 cursor-pointer'>
                    <div className='flex items-center justify-between gap-x-1'>
                        <span className='uppercase text-sm text-yellow-primary hover:text-yellow-secondary'>Ver detalhes</span>
                        <ArrowRigthIcon height={20} width={20} color='#fec857' />
                    </div>
                </button>
            </div>
            <div className='flex items-center gap-x-2'>
                <CreditCardIcon height={18} width={18} color='#fec857' />
                <span className='text-xs text-gray-tertiary'>Pagamento via <span className='font-bold'>cartao de credito</span> </span>
            </div>
            <span className='text-green-primary text-sm'>Pedido concluído</span>
            <div className='flex items-center gap-x-4'>
                <Image src={'/assets/products-images/product_1.png'} alt='foto do produto' width={80} height={80} />
                <div className='flex flex-col'>
                    <span className='font-bold text-blue-primary'>Galaxy X 2025</span>
                    <span className='text-gray-tertiary text-xs'>quantidade: 1</span>
                </div>
            </div>
        </div>
    );
}

export default ProductCardMyOrder;