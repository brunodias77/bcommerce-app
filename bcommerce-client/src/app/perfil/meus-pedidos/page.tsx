import { Metadata } from 'next';
import MyOrder from './my-order';

export const metadata: Metadata = {
    title: 'Bcommercer | Perfil',
    description: 'Verifique seus pedidos no site Bcommerce',
};
export default async function MyOrderPage() {
    return (
        <MyOrder />
    );
}
