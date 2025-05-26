import Image from 'next/image';
import ArrowLeftIcon from '@/icons/arrow-left-icon';
import ArrowRigthIcon from '@/icons/arrow-rigth-icon';
import TrashIcon from '@/icons/trash-icon';

export interface ProductCartItemCardProps {
    brand: string;
    name: string;
    imageUrl: string;
    quantity: number;
    pricePix: string;
    priceDescription: string;

}

const ProductCartItemCard = ({
    brand,
    name,
    imageUrl,
    quantity,
    pricePix,
    priceDescription,

}: ProductCartItemCardProps) => {
    return (
        <div className="flex items-center justify-between gap-x-4 mb-4">
            <div className="flex items-center gap-x-4">
                <Image
                    src={imageUrl}
                    alt={`Foto do produto: ${name}`}
                    width={80}
                    height={80}
                />
                <div className="flex flex-col">
                    <span className="text-gray-tertiary text-xs">{brand}</span>
                    <span className="font-bold text-blue-primary">{name}</span>
                    <span className="text-gray-tertiary text-xs">
                        {priceDescription}{' '}
                        <span className="font-bold text-blue-primary">{pricePix}</span>
                    </span>
                </div>
            </div>

            <div className="flex flex-col items-center gap-y-1">
                <span className="text-sm text-gray-tertiary">Quant.</span>
                <div className="flex items-center gap-x-6">
                    <button
                        className="cursor-pointer hover:brightness-50 transition-all duration-300"
                    >
                        <ArrowLeftIcon color="#4b5966" height={15} width={15} />
                    </button>
                    <span>{quantity}</span>
                    <button className="cursor-pointer">
                        <ArrowRigthIcon color="#4b5966" height={15} width={12} />
                    </button>
                </div>
                <button className="flex items-center gap-x-2 cursor-pointer">
                    <TrashIcon color="#fec857" height={16} width={16} />
                    <span className="text-sm text-yellow-primary font-bold">Remover</span>
                </button>
            </div>

            <div className="flex flex-col items-center gap-y-1">
                <span className="text-sm text-gray-tertiary">Preço à vista no PIX:</span>
                <span className="font-bold text-lg text-yellow-primary">{pricePix}</span>
            </div>
        </div>
    );
};

export default ProductCartItemCard;
