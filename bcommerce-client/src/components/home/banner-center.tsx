import React from "react";
import banner from "../../../public/assets/banners/Banner 2.png";
import banner3 from "../../../public/assets/banners/banner3.png";
import banner4 from "../../../public/assets/banners/banner4.png";
import banner5 from "../../../public/assets/banners/banner5.png";

import Imagem from "next/image";
import Section from "../ui/Section";

const BannerCenter: React.FC = () => {
    return (
        <Section>
            <div className="container flex items-center justify-between gap-x-4">
                <Imagem src={banner3} alt="" />
                <Imagem src={banner4} alt="" />
                <Imagem src={banner5} alt="" />
            </div>
        </Section>
    );
}

export default BannerCenter;