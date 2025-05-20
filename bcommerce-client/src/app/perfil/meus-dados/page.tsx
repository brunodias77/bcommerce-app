import CardAddress from '@/components/address/card-address';
import RegisterForm from '@/components/register/register-form';
import Button from '@/components/ui/button';
import Input from '@/components/ui/input';
import DocumentICon from '@/icons/document-icon';
import MapIcon from '@/icons/map-icon';
import UserIcon from '@/icons/user-icon';
import { Metadata } from 'next';
import React from 'react';
import MyDataPageContent from './my-data-page';

export const metadata: Metadata = {
    title: 'Bcommercer | Perfil',
    description: 'Gerencie seu perfil no site Bcommerce',
};
export default function MyDataPage() {

    return (
        <MyDataPageContent />
        // <div className="mx-auto max-w-[1440px] flex-1 py-4 flex flex-col">
        //     <div className='flex items-center gap-x-2 mb-6'>
        //         <UserIcon color="#fec857" height={25} width={25} />
        //         <h2 className="text-blue-primary font-bold text-2xl uppercase">
        //             Meus Dados
        //         </h2>
        //     </div>
        //     <div className='grid grid-cols-1 md:grid-cols-2 gap-8'>
        //         <div className='bg-white rounded shadow-md p-8 flex flex-col gap-y-4'>
        //             <div className='flex items-center gap-x-2 '>
        //                 <DocumentICon color='#fec857' />
        //                 <span className='uppercase text-blue-primary font-bold'>Dados Basicos</span>
        //             </div>
        //             <div className='flex gap-4 w-full'>
        //                 <button className=' border border-yellow-primary rounded px-4 py-2 w-full cursor-pointer '>
        //                     <span className='uppercase text-yellow-primary text-sm cursor-pointer font-bold'>Alterar e-mail</span>
        //                 </button>
        //                 <button className=' border border-yellow-primary rounded px-4 py-2 w-full cursor-pointer '>
        //                     <span className='uppercase text-yellow-primary text-sm cursor-pointer font-bold'>Alterar senha</span>
        //                 </button>
        //             </div>
        //             <Input
        //                 id="name"
        //                 name="name"
        //                 label="nome completo *"
        //                 placeholder="Bruno Dias"
        //                 required
        //             />
        //             <Input
        //                 id="phone"
        //                 name="phone"
        //                 label="Telefone celular"
        //                 placeholder="Digite seu celular"
        //                 type='number'
        //                 required
        //             />
        //             <Input
        //                 id="email"
        //                 name="email"
        //                 label="Email"
        //                 type='email'
        //                 placeholder="Digite o email"
        //                 required
        //             />
        //             <Input
        //                 id="cpf"
        //                 name="cpf"
        //                 label="cpf"
        //                 type='number'
        //                 placeholder="Digite o cpf"
        //                 required
        //             />
        //             <Input
        //                 id="date"
        //                 name="date"
        //                 label="Data de nascimento"
        //                 type='date'
        //                 required
        //             />
        //             <div className='grid grid-cols-2 gap-4'>
        //                 <button className='cursor-pointer'>
        //                     <span className='underline uppercase'>Excluir minha conta</span>
        //                 </button>
        //                 <Button>
        //                     <span className='uppercase font-bold'>Salvar alterações</span>
        //                 </Button>
        //             </div>
        //         </div>
        //         <div className='bg-white rounded shadow-md p-8 flex flex-col gap-y-4 flex-1'>
        //             <div className='flex items-center gap-x-2'>
        //                 <MapIcon color='#fec857' />
        //                 <span className='uppercase text-blue-primary font-bold'>Endereços</span>
        //             </div>
        //             <CardAddress isStandard={true} />
        //             <CardAddress />
        //             <div className='mt-auto'>
        //                 <Button className='w-full'>
        //                     <span className='uppercase font-bold'>cadastrar novo endereço</span>
        //                 </Button>
        //             </div>
        //         </div>

        //     </div>
        // </div>
    );
}
