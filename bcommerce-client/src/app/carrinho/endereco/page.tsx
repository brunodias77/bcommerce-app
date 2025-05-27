import { Metadata } from 'next';
import AddressPageContent from './address-page-content';


export const metadata: Metadata = {
    title: 'Bcommercer | Endereços',
    description: 'Página de enderecos do Bcommerce',
};
export default async function AddressPage() {
    return (
        <AddressPageContent />
    );
}
