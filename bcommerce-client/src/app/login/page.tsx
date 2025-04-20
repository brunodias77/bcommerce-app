import LoginForm from '@/components/login/login-form';
import { Metadata } from 'next';

export const metadata: Metadata = {
    title: 'Bcommercer | Login',
    description: 'Logue na sua conta no site Bcommerce',
};

export default async function LoginPage() {
    return (
        <div className='container'>
            <LoginForm />
        </div>
    );
}
