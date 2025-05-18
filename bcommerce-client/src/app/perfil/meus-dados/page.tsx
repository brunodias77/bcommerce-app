import RegisterForm from '@/components/register/register-form';
import UserIcon from '@/icons/user-icon';
import { Metadata } from 'next';

export const metadata: Metadata = {
    title: 'Bcommercer | Perfil',
    description: 'Gerencie seu perfil no site Bcommerce',
};
export default async function MyDataPage() {
    return (
        <div className="mx-auto max-w-[1440px] flex-1 py-10">
            <div className='flex items-center gap-x-2 mb-6'>
                <UserIcon color="#fec857" height={25} width={25} />
                <h2 className="text-blue-primary font-bold text-2xl uppercase">
                    Meus Dados
                </h2>
            </div>
        </div>
    );
}
