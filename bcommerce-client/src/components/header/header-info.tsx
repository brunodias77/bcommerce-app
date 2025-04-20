import Image from "next/image";
import Button from "../ui/button";
import PhoneIcon from "../../icons/phone-icon";
import EmailIcon from "../../icons/email-icon";

const HeaderInfo = () => {
    return (
        <div className="w-full bg-[#F8F8FB] p-0">
            <div className="relative mx-auto max-w-[1440px] px-6 py-1 lg:px-12 flex items-center justify-between">
                {/* Esquerda */}
                <div className="flex items-center justify-center gap-x-4">
                    <div className="flex items-center gap-x-2">
                        <PhoneIcon color="#777777" height={15} width={15} />
                        <span className="text-[#777777] text-xs">(14) 3415-2890</span>
                    </div>
                    <div className="hidden lg:flex items-center gap-x-2">
                        <EmailIcon color="#777777" height={15} width={15} />
                        <span className="text-[#777777] text-xs">brunohenriqueadias@gmail.com</span>
                    </div>
                </div>

                {/* Centro */}
                <div className="absolute left-1/2 top-1/2 -translate-x-1/2 -translate-y-1/2 text-[#777777] text-xs flex items-center gap-x-1">
                    Produtos com até <span className="text-[15px] font-bold text-[#FEC857]">50% OFF</span>
                </div>

                {/* Direita */}
                <div>
                    <Button className="text-sm" variant="secondary" size="small">
                        <span className="text-sm font-bold text-white">APROVEITE</span>
                    </Button>
                </div>
            </div>
        </div>
    );
}

export default HeaderInfo;
