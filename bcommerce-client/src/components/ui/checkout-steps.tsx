"use client";
import usePageName from "@/hooks/usePageName";
import AddressIcon from "@/icons/address-icon";
import CartIcon from "@/icons/cart-icon";
import CheckCircleIcon from "@/icons/check-circle-icon";
import CreditCardIcon from "@/icons/credit-card-icon";
import ShippingIcon from "@/icons/shipping-icon";

const CheckoutSteps = () => {
    const pageName = usePageName();
    console.log('Current page name:', pageName);
    const isActive = (step: string) => {
        const currentStep = pageName.toLowerCase();
        const stepsOrder = ['carrinho', 'endereco', 'envio', 'pagamento', 'confimacao'];
        const retorno = stepsOrder.indexOf(currentStep) >= stepsOrder.indexOf(step);
        return stepsOrder.indexOf(currentStep) >= stepsOrder.indexOf(step);
    };

    return (
        <div id="steps" className="flex items-center justify-between gap-x-4 my-4">
            <div className="flex items-center gap-x-2">
                <div className={`w-[25px] h-[25px] rounded-full p-1  flex items-center justify-center ${isActive('carrinho') ? 'bg-black-primary' : 'bg-[#D1D5DA]'}`}>
                    <CartIcon
                        color="#fec857"
                        isActive={isActive('carrinho')}
                        width={15}
                        height={15}
                    />
                </div>
                <div className={`${isActive('carrinho') ? 'text-blue-primary' : 'text-gray-400'} flex flex-col items-center text-[12px] leading-none`}>
                    <span>Passo 1</span>
                    <span className="font-bold">Carrinho</span>
                </div>
            </div>

            <div className="flex items-center gap-x-2">
                <div className={`w-[25px] h-[25px] rounded-full p-1  flex items-center justify-center ${isActive('endereco') ? 'bg-black-primary' : 'bg-[#D1D5DA]'}`}>
                    <AddressIcon
                        color="#fec857"
                        isActive={isActive('endereco')}
                        width={15}
                        height={15}
                    />
                </div>
                <div className={`${isActive('endereco') ? 'text-blue-primary' : 'text-gray-400'} flex flex-col items-center text-[12px] leading-none`}>
                    <span>Passo 2</span>
                    <span className="font-bold">Endereço</span>
                </div>
            </div>

            <div className="flex items-center gap-x-2">
                <div className={`w-[25px] h-[25px] rounded-full p-1  flex items-center justify-center ${isActive('envio') ? 'bg-black-primary' : 'bg-[#D1D5DA]'}`}>
                    <ShippingIcon
                        color="#fec857"
                        isActive={isActive('envio')}
                        width={15}
                        height={15}
                    />
                </div>
                <div className={`${isActive('envio') ? 'text-blue-primary' : 'text-gray-400'} flex flex-col items-center text-[12px] leading-none`}>
                    <span>Passo 3</span>
                    <span className="font-bold">Envio</span>
                </div>
            </div>

            <div className="flex items-center gap-x-2">
                <div className={`w-[25px] h-[25px] rounded-full p-1  flex items-center justify-center ${isActive('pagamento') ? 'bg-black-primary' : 'bg-[#D1D5DA]'}`}>
                    <CreditCardIcon
                        color="#fec857"
                        isActive={isActive('pagamento')}
                        width={15}
                        height={15}
                    />
                </div>
                <div className={`${isActive('pagamento') ? 'text-blue-primary' : 'text-gray-400'} flex flex-col items-center text-[12px] leading-none`}>
                    <span>Passo 4</span>
                    <span className="font-bold">Pagamento</span>
                </div>
            </div>

            <div className="flex items-center gap-x-2">
                <div className={`w-[25px] h-[25px] rounded-full p-1  flex items-center justify-center ${isActive('confimacao') ? 'bg-black-primary' : 'bg-[#D1D5DA]'}`}>
                    <CheckCircleIcon
                        color="#fec857"
                        isActive={isActive('confimacao')}
                        width={15}
                        height={15}
                    />
                </div>
                <div className={`${isActive('confimacao') ? 'text-blue-primary' : 'text-gray-400'} flex flex-col items-center text-[12px] leading-none`}>
                    <span>Passo 5</span>
                    <span className="font-bold">Confirmação</span>
                </div>
            </div>

        </div>
    );
};

export default CheckoutSteps;