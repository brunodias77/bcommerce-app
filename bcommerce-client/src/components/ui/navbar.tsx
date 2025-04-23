"use client";
import Link from "next/link";
import { usePathname } from "next/navigation";

interface NavbarProps {
    containerStyles?: string;
    onClick?: () => void;
}

const navLinks = [
    { path: "/", title: "início" },
    { path: "/collection", title: "Coleções" },
    { path: "/blog", title: "Blog" },
    { path: "mailto:brunohenriqueadias@gmail.com", title: "Contato" },
];

const Navbar: React.FC<NavbarProps> = ({ containerStyles = "", onClick }) => {
    const pathname = usePathname();

    const isActive = (path: string) => pathname === path;

    return (
        <nav className={containerStyles}>
            {navLinks.map((link) => (
                <Link
                    key={link.title}
                    href={link.path}
                    className={`
            px-3 py-2 rounded-full capitalize transition-colors 
            ${isActive(link.path) ? "bg-[#2d2926] text-white" : "text-black-primary hover:bg-black-primary/10"}
          `}
                    onClick={onClick}
                >
                    {link.title}
                </Link>
            ))}
        </nav>
    );
};

export default Navbar;
