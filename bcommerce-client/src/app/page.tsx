import Image from "next/image";
import BannerHome from "@/components/home/banner-home";
import InfoCompanyPolicies from "@/components/home/info-company-policies";
import NewProducstSection from "@/components/section-new-products/section-new-products";

export default function Home() {
  return (
    <>
      <BannerHome />
      <InfoCompanyPolicies />
      <NewProducstSection />
    </>
  );
}
