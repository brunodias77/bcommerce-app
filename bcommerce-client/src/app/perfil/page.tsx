import RegisterForm from '@/components/register/register-form';
import Button from '@/components/ui/button';
import BagIcon from '@/icons/bag-icon';
import EmailIcon from '@/icons/email-icon';
import PencilIcon from '@/icons/pencil-icon';
import { Metadata } from 'next';

export const metadata: Metadata = {
    title: 'Bcommercer | Perfil',
    description: 'Gerencie seu perfil no site Bcommerce',
};
export default async function PerfilPage() {
    return (
        <div className="mx-auto max-w-[1440px] flex-1 py-10">
            <div className='bg-white rounded w-full px-8 py-4 flex items-center justify-between'>
                <div className='flex items-center gap-x-4'>
                    {/* FOTO PERFIL */}
                    <div className='w-15 h-15 rounded-full bg-gray-200 flex items-center justify-center'>
                        <span className='font-bold text-center'>B</span>
                    </div>
                    <div className='flex flex-col '>
                        <span className='font-bold text-xl text-black-primary'>Bem-vindo, Bruno Dias</span>
                        <div className='flex items-center gap-x-2'>
                            <EmailIcon color="#fec857" height={12} width={12} />
                            <span className='text-gray-tertiary text-sm'>brunohdias95@gmail.com</span>
                        </div>
                    </div>

                </div>
                <Button variant='secondary'>
                    <div className='flex items-center gap-x-2'>
                        <PencilIcon color="#FFF" height={15} width={15} />
                        <span className='uppercase font-bold'>
                            Editar Dados
                        </span>
                    </div>

                </Button>
            </div>

            <div className='grid grid-cols-1 md:grid-cols-3 gap-y-4 gap-x-8 mt-10'>
                <div className='p-8 bg-white rounded shadow-md cursor-pointer flex items-center justify-center transition-transform hover:scale-105 duration-300 active:scale-95  '>
                    <div className='flex items-center gap-x-4'>
                        <BagIcon color="#fec857" height={25} width={25} />
                        <div className='flex flex-col'>
                            <h2 className='font-bold text-blue-primary'>Meus Pedidos</h2>
                            <span className='text-gray-tertiary text-sm '>Veja históricos e acompanhe suas compras.</span>
                        </div>
                    </div>
                </div>
                <div className='px-8 py-4 bg-white rounded shadow-md cursor-pointer flex items-center justify-center transition-transform hover:scale-105 duration-300 active:scale-95'>
                    <span>Meus Pedidos</span>
                </div>
                <div className='px-8 py-4 bg-white rounded shadow-md cursor-pointer flex items-center justify-center transition-transform hover:scale-105 duration-300 active:scale-95'>
                    <span>Meus Pedidos</span>
                </div>
                <div className='px-8 py-4 bg-white rounded shadow-md cursor-pointer flex items-center justify-center transition-transform hover:scale-105 duration-300 active:scale-95'>
                    <span>Meus Pedidos</span>
                </div>
                <div className='px-8 py-4 bg-white rounded shadow-md cursor-pointer flex items-center justify-center transition-transform hover:scale-105 duration-300 active:scale-95'>
                    <span>Meus Pedidos</span>
                </div>
                <div className='px-8 py-4 bg-white rounded shadow-md cursor-pointer flex items-center justify-center transition-transform hover:scale-105 duration-300 active:scale-95'>
                    <span>Meus Pedidos</span>
                </div>
            </div>
        </div>
    );
}
