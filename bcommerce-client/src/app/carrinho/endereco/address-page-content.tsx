import CardAddress from '@/components/address/card-address';
import CheckoutSteps from '@/components/ui/checkout-steps';
import Divider from '@/components/ui/divider';
import OrderSummary from '@/components/ui/order-summary';
import MapIcon from '@/icons/map-icon';
import PlusIcon from '@/icons/plus-icon';
import React from 'react';

const AddressPageContent = () => {
    const cart = true;
    return (
        <div className="w-full flex flex-col md:flex-row bg-[#F2F3F4] flex-1 py-4">
            <div className='container'>
                <CheckoutSteps />

                {cart != null ? (
                    <div className="grid grid-cols-1 md:grid-cols-[1fr_400px] gap-x-8 animeLeft">
                        <div className='bg-white px-8 py-4 flex flex-col gap-y-4 rounded'>
                            <div className='flex items-center gap-x-2'>
                                <MapIcon color='#fec857' />
                                <span className='uppercase text-blue-primary font-bold'>Endereços</span>
                            </div>
                            <CardAddress isStandard={true} />
                            <CardAddress />
                            <Divider variant='button-icon' icon={PlusIcon} label='adicionar um novo endereço' />
                        </div>
                        <OrderSummary />
                    </div>
                ) : (
                    <div>Carrinho esta vazio</div>
                )}
            </div>
        </div>
    );
}
export default AddressPageContent;