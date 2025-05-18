import PerfilCard from '@/components/perfil/perfil-card';
import RegisterForm from '@/components/register/register-form';
import Button from '@/components/ui/button';
import BagIcon from '@/icons/bag-icon';
import CreditCardIcon from '@/icons/credit-card-icon';
import EmailIcon from '@/icons/email-icon';
import HeartIcon from '@/icons/heart-icon';
import LikeIcon from '@/icons/like-icon';
import PencilIcon from '@/icons/pencil-icon';
import SupportIcon from '@/icons/support-icon';
import UserIcon from '@/icons/user-icon';
import { Metadata } from 'next';

export const metadata: Metadata = {
    title: 'Bcommercer | Perfil',
    description: 'Gerencie seu perfil no site Bcommerce',
};
export default async function PerfilPage() {
    const cards = [
        {
            title: 'Meus Pedidos',
            description: 'Veja históricos e acompanhe suas compras.',
            icon: <BagIcon color="#fec857" height={25} width={25} />
        },
        {
            title: 'Meus Dados',
            description: 'Altere seus dados cadastrados, endereços ou cadastre um novo endereço.',
            icon: <UserIcon color="#fec857" height={25} width={25} />
        },
        {
            title: 'Carteira',
            description: 'Gerencie seus cartões, créditos e resgate gift card.',
            icon: <CreditCardIcon color="#fec857" height={25} width={25} />
        },
        {
            title: 'Protocolos',
            description: 'Aqui você encontra seus protocolos de atendimento.',
            icon: <SupportIcon color="#fec857" height={25} width={25} />
        },
        {
            title: 'Avaliacoes',
            description: 'Avalie suas compras e visualize suas avaliações e comentários.',
            icon: <LikeIcon color="#fec857" height={25} width={25} />
        },
        {
            title: 'Favoritos',
            description: 'Consulte sua lista de produtos favoritados.',
            icon: <HeartIcon color="#fec857" height={25} width={25} />
        }
    ];
    return (
        <div className="mx-auto max-w-[1440px] flex-1 py-10">
            <div className='bg-white rounded w-full px-8 py-4 flex items-center justify-between'>
                <div className='flex items-center gap-x-4'>
                    {/* FOTO PERFIL */}
                    <div className='w-15 h-15 rounded-full bg-gray-200 flex items-center justify-center'>
                        <span className='font-bold text-center'>B</span>
                    </div>
                    <div className='flex flex-col '>
                        <span className='font-bold text-xl text-black-primary'>Bem-vindo, Bruno Dias</span>
                        <div className='flex items-center gap-x-2'>
                            <EmailIcon color="#fec857" height={12} width={12} />
                            <span className='text-gray-tertiary text-sm'>brunohdias95@gmail.com</span>
                        </div>
                    </div>

                </div>
                <Button variant='secondary'>
                    <div className='flex items-center gap-x-2'>
                        <PencilIcon color="#FFF" height={15} width={15} />
                        <span className='uppercase font-bold'>
                            Editar Dados
                        </span>
                    </div>

                </Button>
            </div>

            <div className="grid grid-cols-1 md:grid-cols-3 gap-y-4 gap-x-8 mt-10">
                {cards.map((card, index) => (
                    <PerfilCard
                        key={index}
                        title={card.title}
                        description={card.description}
                        icon={card.icon}
                    />
                ))}
            </div>
        </div>
    );
}



