"use client";
import Link from "next/link";
import HouseIcon from "@/icons/house-icon";
import UserIcon from "@/icons/user-icon";
import BagIcon from "@/icons/bag-icon";
import CreditCardIcon from "@/icons/credit-card-icon";
import LikeIcon from "@/icons/like-icon";
import TelemarketingIcon from "@/icons/telemarketing-icon";
import HeartIcon from "@/icons/heart-icon";

interface SidebarLink {
    href: string;
    label: string;
    icon: React.ReactNode;
}

const links: SidebarLink[] = [
    {
        href: "/perfil",
        label: "Início",
        icon: <HouseIcon color="#fec857" height={25} width={25} />,
    },
    {
        href: "/perfil/meus-dados",
        label: "Meus Dados",
        icon: <UserIcon color="#fec857" height={25} width={25} />,
    },
    {
        href: "/perfil/meus-pedidos",
        label: "Meus Pedidos",
        icon: <BagIcon color="#fec857" height={25} width={25} />,
    },
    {
        href: "/perfil/minha-carteira",
        label: "Carteira",
        icon: <CreditCardIcon color="#fec857" height={25} width={25} />,
    },
    {
        href: "/perfil/avaliacoes",
        label: "Avaliações e Comentários",
        icon: <LikeIcon color="#fec857" height={25} width={25} />,
    },
    {
        href: "/perfil/atendimento",
        label: "Atendimento ao cliente",
        icon: <TelemarketingIcon color="#fec857" height={25} width={25} />,
    },
    {
        href: "/favoritos",
        label: "Favoritos",
        icon: <HeartIcon color="#fec857" height={25} width={25} />,
    },
    // Adicione mais links se precisar
];

const SidebarNavigation: React.FC = () => {
    return (
        <div className="bg-white w-full md:w-[80px] py-8 px-2 flex flex-col items-center gap-y-12 shadow-[4px_0_8px_-2px_rgba(0,0,0,0.1)]">
            {links.map(({ href, label, icon }) => (
                <Link key={href} href={href} className="relative group">
                    {icon}
                    <span className="absolute left-10 top-1/2 -translate-y-1/2 bg-black text-white text-xs rounded px-2 py-1 opacity-0 group-hover:opacity-100 transition whitespace-nowrap">
                        {label}
                    </span>
                </Link>
            ))}
        </div>
    );
};

export default SidebarNavigation;
