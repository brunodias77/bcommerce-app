import RegisterForm from '@/components/register/register-form';
import { Metadata } from 'next';

export const metadata: Metadata = {
    title: 'Bcommercer | Perfil',
    description: 'Gerencie seu perfil no site Bcommerce',
};
export default async function PerfilPage() {
    return (
        <div className="flex-1 bg-gray-primary flex">
            <div className="container flex-1 py-10">
                <h1 className="text-2xl font-semibold">Perfil</h1>
            </div>
        </div>
    );
}
