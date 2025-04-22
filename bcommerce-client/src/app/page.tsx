import Image from "next/image";
import BannerHome from "@/components/home/banner-home";
import InfoCompanyPolicies from "@/components/home/info-company-policies";

export default function Home() {
  return (
    <>
      <BannerHome />
      <InfoCompanyPolicies />
    </>
  );
}
