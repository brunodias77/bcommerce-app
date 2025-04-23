import DevolutionIcon from "@/icons/devolution-icon";
import MoneyIcon from "@/icons/money-icon";
import SupportIcon from "@/icons/support-icon";
import TruckIcon from "@/icons/truck-icon";


const InfoCompanyPolicies: React.FC = () => {
    return (
        <div className="w-full ">
            <div className="m-auto max-w-[1200px] p-4 border-[1px] border-[#E5E7EB] rounded-lg mt-8">
                <div className="flex items-center justify-between gap-x-4 ">
                    <div className=" w-full border-r-[1px] border-[#E5E7EB]  pr-4 flex items-center justify-center gap-x-4">
                        <TruckIcon width={25} height={25} color="#2d2926" />
                        <div className="flex flex-col  gap-x-2">
                            <h2 className="text-black-primary font-bold text-sm">Frete grátis</h2>
                            <span className="text-gray-secondary text-xs">
                                Em pedidos acima de $ 50,00
                            </span>
                        </div>

                    </div>
                    <div className=" w-full border-r-[1px] border-[#E5E7EB] pr-4 flex items-center justify-center gap-x-4">
                        <SupportIcon width={25} height={25} color="#2d2926" />
                        <div className="flex flex-col  gap-x-2">
                            <h2 className="text-black-primary  font-bold text-sm">Suport 24 H</h2>
                            <span className="text-gray-secondary text-xs">
                                Contato/mensagem ao vivo
                            </span>
                        </div>                </div>
                    <div className=" w-full flex items-center justify-center gap-x-4">
                        <MoneyIcon width={25} height={25} color="#2d2926" />
                        <div className="flex flex-col  gap-x-2 ">
                            <h2 className="text-black-primary  font-bold text-sm">Política de reembolso</h2>
                            <span className="text-gray-secondary text-xs">
                                30 dias para devolução por qualquer motivo
                            </span>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    );
}

export default InfoCompanyPolicies;