"use client";

import Link from "next/link";
import Image from "next/image";
import logo from "../../../public/assets/logo/logo-bcommerce.png";
import Navbar from "../ui/navbar";
import { FaBars, FaBarsStaggered } from "react-icons/fa6";
import { IoCartOutline, IoPersonOutline, IoHeartOutline } from "react-icons/io5";
import { useEffect, useState } from "react";
import { useRouter } from "next/navigation";
import { useAuth } from "@/context/auth-context";
import UserIcon from "@/icons/user-icon";
import HeartIcon from "@/icons/heart-icon";
import CartIcon from "@/icons/cart-icon";
import { useCartContext } from "@/context/cart-context";

const Header = () => {
    const [menuOpened, setMenuOpened] = useState(false);
    const [hasShadow, setHasShadow] = useState(false);
    const router = useRouter();
    const { isAuthenticated, userName } = useAuth();
    const { getCartCount } = useCartContext();

    useEffect(() => {
        const handleScroll = () => {
            setHasShadow(window.scrollY > 10);
        };

        window.addEventListener("scroll", handleScroll);
        return () => window.removeEventListener("scroll", handleScroll);
    }, []);

    const toggleMenu = () => setMenuOpened((prev) => !prev);

    const handleProfileClick = () => {
        if (isAuthenticated) {
            router.push("/perfil");
        } else {
            router.push("/login");
        }
    };

    return (
        <header
            className={`w-full sticky top-0 z-50 bg-white/70 backdrop-blur-md transition-shadow duration-300 ${hasShadow ? "shadow-md" : ""
                }`}
        >
            <div className="mx-auto max-w-[1440px] px-6 py-1 lg:px-12 flex items-center justify-between">
                {/* Logo */}
                <Link href="/" className="flex flex-1">
                    <Image
                        src={logo}
                        alt="logo da empresa"
                        width={60}
                        height={60}
                        priority
                    />
                </Link>

                {/* Menu */}
                <div className="flex-1">
                    <Navbar
                        containerStyles={`${menuOpened
                            ? "flex items-start flex-col gap-x-8 fixed top-16 right-6 py-5 px-5 bg-white rounded-xl shadow-md w-52 ring-1 ring-slate-900/5 z-50"
                            : "hidden xl:flex items-center justify-around gap-x-5 xl:gap-x-7 text-[15px] font-[500] bg-primary ring-1 ring-slate-900/5 rounded-full p-1"
                            }`}
                        onClick={() => setMenuOpened(false)}
                    />
                </div>

                {/* Ícones */}
                <div className="flex-1 flex items-center justify-end gap-x-4 xs:gap-x-16">
                    {menuOpened ? (
                        <FaBarsStaggered
                            onClick={toggleMenu}
                            className="xl:hidden cursor-pointer text-xl"
                        />
                    ) : (
                        <FaBars
                            onClick={toggleMenu}
                            className="xl:hidden cursor-pointer text-xl"
                        />
                    )}

                    <span>olá, {userName}</span>

                    {/* PROFILE */}
                    <button
                        onClick={handleProfileClick}
                        className="group relative cursor-pointer flex"
                    >
                        <UserIcon color="#2d2926" />
                    </button>

                    {/* FAVORITOS */}
                    <Link href="/favorites" className="group relative cursor-pointer flex">
                        <HeartIcon color="#2d2926" />
                    </Link>

                    {/* CARRINHO */}
                    <Link href="/cart" className="flex relative">
                        <CartIcon color="#2d2926" />
                        <span className="bg-yellow-primary text-black-primary text-[12px] font-semibold absolute -top-3.5 -right-2 flex items-center justify-center w-4 h-4 rounded-full shadow-md">
                            {getCartCount}
                        </span>
                    </Link>
                </div>
            </div>
        </header>
    );
};

export default Header;


// "use client";

// import Link from "next/link";
// import Image from "next/image";
// import logo from "../../../public/assets/logo/logo-bcommerce.png";
// import Navbar from "../ui/navbar";
// import MenuOpenIcon from "@/icons/menu-open-icon";
// import MenuIcon from "@/icons/menu-icon";
// import CartIcon from "@/icons/cart-icon";
// import UserIcon from "@/icons/user-icon";
// import HeartIcon from "@/icons/heart-icon";
// import { FaBars, FaBarsStaggered } from "react-icons/fa6";
// import { IoCartOutline, IoPersonOutline, IoHeartOutline } from "react-icons/io5";
// import { useEffect, useState } from "react";


// const Header = () => {
//     const [menuOpened, setMenuOpened] = useState(false);
//     const [hasShadow, setHasShadow] = useState(false);

//     useEffect(() => {
//         const handleScroll = () => {
//             if (window.scrollY > 10) {
//                 setHasShadow(true);
//             } else {
//                 setHasShadow(false);
//             }
//         };

//         window.addEventListener("scroll", handleScroll);

//         return () => window.removeEventListener("scroll", handleScroll);
//     }, []);

//     const toggleMenu = () => setMenuOpened((prev) => !prev);
//     // const shopContext = useContext(ShopContext);

//     // if (!shopContext) {
//     //     throw new Error("ShopContext deve ser usado dentro de um ShopContextProvider");
//     // }

//     // const { getCartCount } = shopContext;

//     return (

//         <header className={`w-full sticky top-0 z-50 bg-white/70 backdrop-blur-md transition-shadow duration-300 ${hasShadow ? "shadow-md" : ""}`}>
//             <div className="container flex items-center justify-between">
//                 <Link href="/" className="flex flex-1">
//                     <Image
//                         src={logo}
//                         alt="logo da empresa"
//                         width={60}
//                         height={60}
//                         priority
//                     />
//                 </Link>

//                 <div className="flex-1">
//                     <Navbar
//                         containerStyles={`${menuOpened
//                             ? "flex items-start flex-col gap-x-8 fixed top-16 right-6 py-5 px-5 bg-white rounded-xl shadow-md w-52 ring-1 ring-slate-900/5 z-50"
//                             : "hidden xl:flex items-center justify-around gap-x-5 xl:gap-x-7 text-[15px] font-[500] bg-primary ring-1 ring-slate-900/5 rounded-full p-1"
//                             }`}
//                         onClick={() => setMenuOpened(false)}
//                     />
//                 </div>


//                 <div className="flex-1 flex items-center justify-end gap-x-4 xs:gap-x-16">
//                     {menuOpened ? (
//                         <FaBarsStaggered onClick={toggleMenu} className="xl:hidden cursor-pointer text-xl" />
//                     ) : (
//                         <FaBars onClick={toggleMenu} className="xl:hidden cursor-pointer text-xl" />
//                     )}



//                     {/* PROFILE */}
//                     <Link href="/perfil" className="group relative cursor-pointer flex">
//                         <IoPersonOutline size={20} color="#2d2926" />
//                     </Link>

//                     <Link href="/favorites" className="group relative cursor-pointer flex">
//                         <IoHeartOutline size={20} color="#2d2926" />
//                     </Link>

//                     {/* CART */}
//                     <Link href="/cart" className="flex relative">
//                         <IoCartOutline size={20} color="#2d2926" />
//                         <span className="bg-yellow-primary text-black-400 text-[12px] font-semibold absolute -top-3.5 -right-2 flex items-center justify-center w-4 h-4 rounded-full shadow-md">
//                             {/* {getCartCount()} */}
//                             0
//                         </span>
//                     </Link>
//                 </div>


//             </div>
//         </header>

//     );
// }

// export default Header;
