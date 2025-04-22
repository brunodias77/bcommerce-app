import RegisterForm from '@/components/register/register-form';
import { Metadata } from 'next';

export const metadata: Metadata = {
    title: 'Bcommercer | Criar Conta',
    description: 'Logue na sua conta no site Bcommerce',
};

export default async function RegisterPage() {
    return (
        <div className='container'>
            <RegisterForm />
        </div>
    );
}
