import React from 'react';

type CardAddressProps = {
    isStandard?: boolean;
}

const CardAddress: React.FC<CardAddressProps> = ({ isStandard = false }) => {
    return (
        <div className={`w-full  px-8 py-4 ${isStandard ? 'border-l-4 border border-yellow-primary bg-yellow-primary/30' : 'border-l-4 border border-gray-200'}`}>
            <div className='flex flex-col gap-y-2 '>
                <span className='font-bold text-sm text-blue-primary'>Apartamento</span>
                <span className='text-gray-tertiary'>Rua: Rosa Iachel Mazetto</span>
                <span className='text-gray-tertiary'>Número: 195, Torre A, apartamento 11</span>
                <span className='text-gray-tertiary'>CEP: 17512724 - <span className='text-gray-tertiary'>Marília, SP</span> </span>
            </div>
        </div>
    );
}
export default CardAddress;