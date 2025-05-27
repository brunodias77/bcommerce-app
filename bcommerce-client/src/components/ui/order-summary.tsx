import React from "react";
import Link from "next/link";
import SummaryIcon from "../../icons/summary-icon"; // Supondo que o componente SummaryIcon já existe
import Divider from "../../components/ui/divider";
import Button from "../../components/ui/button"; // Supondo que o componente Button já existe

const OrderSummary: React.FC = () => {
    return (
        <div className="flex flex-col bg-white rounded p-8 max-w-[400px]">
            {/* Cabeçalho */}
            <div className="flex items-center gap-x-4 mb-6">
                <SummaryIcon color="#fec857" height={18} width={18} />
                <h2 className="text-blue-primary font-bold uppercase">Resumo</h2>
            </div>

            {/* Valores dos produtos */}
            <div className="flex items-center justify-between my-2">
                <span className="text-sm text-gray-tertiary">Valor dos produtos:</span>
                <span className="font-bold text-blue-primary">R$ 2.000,00</span>
            </div>

            <Divider dashedLine={false} gradientEdge={false} />

            {/* Descontos */}
            <div className="flex items-center justify-between my-2">
                <span className="text-sm text-gray-tertiary">Descontos:</span>
                <span className="font-bold text-blue-primary">R$ 0,00</span>
            </div>

            <Divider dashedLine={false} gradientEdge={false} />

            {/* Frete */}
            <div className="flex items-center justify-between my-2">
                <span className="text-sm text-gray-tertiary">Frete:</span>
                <span className="font-bold text-blue-primary">R$ 0,00</span>
            </div>

            <Divider dashedLine={false} gradientEdge={false} />

            {/* Total a prazo */}
            <div className="flex items-center justify-between mt-2">
                <span className="text-sm text-gray-tertiary">Total a prazo:</span>
                <span className="font-bold text-blue-primary">R$ 2.000,00</span>
            </div>
            <div className="text-right text-xs text-gray-tertiary">
                (em até <span className="font-bold text-blue-primary">10x</span> de{" "}
                <span className="font-bold text-blue-primary">R$ 200,00</span> sem juros)
            </div>

            <Divider dashedLine={false} gradientEdge={false} />

            {/* Total à vista */}
            <div className="flex items-center justify-between mt-2">
                <span className="text-sm text-gray-tertiary">Total à vista no Pix:</span>
                <span className="font-bold text-blue-primary">R$ 1.800,00</span>
            </div>
            <div className="text-right text-xs text-gray-tertiary">
                (Economize: <span className="font-bold text-blue-primary">R$ 200,00</span>)
            </div>

            {/* Botões */}
            <div className="mt-4">
                <button className="w-full border border-black-primary rounded py-2 px-4 text-base cursor-pointer btn-hover-fill group transition active:scale-95">
                    <Link
                        href="/carrinho/endereco"
                        className="uppercase text-black-primary group-hover:text-white transition-colors duration-300"
                    >
                        Ir para o endereço
                    </Link>
                </button>
            </div>

            <div className="mt-4">
                <Button className="w-full">
                    <Link href="/" className="w-full h-full flex items-center justify-center uppercase">
                        Continuar comprando
                    </Link>
                </Button>
            </div>
        </div>
    );
};

export default OrderSummary;