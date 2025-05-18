import RegisterForm from '@/components/register/register-form';
import BagIcon from '@/icons/bag-icon';
import UserIcon from '@/icons/user-icon';
import { Metadata } from 'next';

export const metadata: Metadata = {
    title: 'Bcommercer | Perfil',
    description: 'Verifique seus pedidos no site Bcommerce',
};
export default async function MyOrderPage() {
    return (
        <div className="mx-auto max-w-[1440px] flex-1 py-10">
            <div className='flex items-center gap-x-4 mb-6'>
                <BagIcon color="#fec857" height={25} width={25} />
                <h2 className="text-blue-primary font-bold text-2xl uppercase">
                    Meus Pedidos
                </h2>
            </div>
        </div>
    );
}
