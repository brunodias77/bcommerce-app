import SidebarNavigation from "@/components/perfil/sidebar-link";

export default function PerfilLayout({
    children,
}: Readonly<{
    children: React.ReactNode;
}>) {
    return (
        <div className="w-full flex flex-col md:flex-row bg-[#F2F3F4] flex-1">
            <SidebarNavigation />
            <div className='flex-1 w-full '>
                {children}
            </div>
        </div>
    );
}