import Image from "next/image";
import BannerHome from "@/components/home/banner-home";
import InfoCompanyPolicies from "@/components/home/info-company-policies";
import NewProducstSection from "@/components/section-new-products/section-new-products";
import PopularProductsSection from "@/components/section-popular-products/section-popular-products";
import BannerCenter from "@/components/home/banner-center";

export default function Home() {
  return (
    <>
      <BannerHome />
      <InfoCompanyPolicies />
      <NewProducstSection />
      <PopularProductsSection />
      <BannerCenter />
    </>
  );
}
