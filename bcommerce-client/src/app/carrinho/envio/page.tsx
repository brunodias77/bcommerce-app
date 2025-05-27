import { Metadata } from 'next';
import ShippingPageContent from './shipping-page-content';


export const metadata: Metadata = {
    title: 'Bcommercer | Envios',
    description: 'Página de envios do Bcommerce',
};

export default async function AddressPage() {
    return (
        <ShippingPageContent />
    )
}
