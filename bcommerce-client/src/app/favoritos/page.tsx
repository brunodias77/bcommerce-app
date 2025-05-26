import SidebarNavigation from '@/components/perfil/sidebar-link';
import Button from '@/components/ui/button';
import BagIcon from '@/icons/bag-icon';
import HouseIcon from '@/icons/house-icon';
import StarIcon from '@/icons/star-icon';
import UserIcon from '@/icons/user-icon';
import { Metadata } from 'next';
import Link from 'next/link';

export const metadata: Metadata = {
    title: 'Bcommercer | Favoritos',
    description: 'Pagina de favoritos do Bcommerce',
};

export default async function FavoritesPage() {
    return (
        <div className="w-full flex flex-col md:flex-row bg-[#F2F3F4] flex-1">
            <SidebarNavigation />
            <div className='container z-30 w-full '>
                <div className="flex flex-1 flex-col gap-x-2 my-8">
                    {/* Título - continua alinhado à esquerda */}
                    <div className='flex items-center gap-x-2 mb-6'>
                        <StarIcon color="#fec857" height={25} width={25} />
                        <h2 className="text-blue-primary font-bold text-2xl uppercase">
                            Favoritos
                        </h2>
                    </div>

                    {/* Mensagem centralizada */}
                    <div className="flex justify-center items-center min-h-[40vh]">
                        <div className='flex flex-col items-center gap-2 p-6 rounded-md text-center max-w-md'>
                            <span className='font-bold text-xl text-blue-primary'> Sua lista de favoritos está vazia.</span>
                            <span className='text-gray-tertiary'>  Mas não se preocupe, basta clicar no ícone de coração e o produto será adicionado à sua lista de favoritos.</span>
                            <Link href={"/"} className=''>
                                <Button  >
                                    Continuar Comprando
                                </Button>
                            </Link>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    );
}
