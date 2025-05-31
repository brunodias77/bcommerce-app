import { Metadata } from 'next';
import BagIcon from '@/icons/bag-icon';
import Divider from '@/components/ui/divider';
import ProductCartItemCard from '@/components/cart/product-cart-item-card';
import CupomInput from '@/components/cart/cupom-input';
import OrderSummary from '@/components/ui/order-summary';
import CheckoutSteps from '@/components/ui/checkout-steps';

export const metadata: Metadata = {
    title: 'Bcommercer | Carrinho',
    description: 'Página de carrinho do Bcommerce',
};

export default function CartPage() {
    const products = null;
    return (
        <div className="w-full flex flex-col md:flex-row bg-[#F2F3F4] flex-1 py-4">
            <div className="container">
                {/* Componente cliente isolado */}
                <CheckoutSteps />
                {products !== null ? (
                    <div>carrinho vazio</div>
                ) : (
                    <div className="grid grid-cols-1 md:grid-cols-[1fr_400px] gap-x-8 animeLeft">
                        {/* Produtos */}
                        <div className="flex flex-col items-center">
                            <div className="flex flex-col bg-white rounded p-4 w-full">
                                <div className="flex items-center gap-x-4 mb-6">
                                    <BagIcon color="#fec857" height={18} width={18} />
                                    <h2 className="text-blue-primary font-bold uppercase">
                                        Produtos
                                    </h2>
                                </div>
                                <Divider dashedLine={false} gradientEdge={false} />
                                <ProductCartItemCard
                                    brand="Samsung"
                                    name="Galaxy X 2025"
                                    imageUrl="/assets/products-images/product_1.png"
                                    quantity={1}
                                    pricePix="R$ 5.426,22"
                                    priceDescription="Parcelado no cartão, à vista ou PIX:"
                                />
                            </div>

                            <div className="px-4 py-2 bg-white flex items-center justify-between rounded mt-4 w-full">
                                <CupomInput />
                            </div>
                        </div>

                        {/* Resumo */}
                        <OrderSummary />
                    </div>
                )}
            </div>
        </div>
    );
}
