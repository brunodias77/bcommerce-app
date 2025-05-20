"use client";
import Divider from "@/components/ui/divider";
import Input from "@/components/ui/input";
import Select from "@/components/ui/select";
import BagIcon from "@/icons/bag-icon";
import { useState } from "react";

const MyOrder: React.FC = () => {
    const [orderDate, setOrderDate] = useState<string>("");

    return (
        <div className="mx-auto max-w-[1440px] flex-1 flex flex-col py-10">
            <div className='flex items-center gap-x-4 mb-6'>
                <BagIcon color="#fec857" height={25} width={25} />
                <h2 className="text-blue-primary font-bold text-2xl uppercase">
                    Meus Pedidos
                </h2>
            </div>
            <Divider dashedLine={false} gradientEdge={false} />
            <div className="flex items-center justify-between my-4">
                <Input
                    id="email"
                    name="email"
                    placeholder="Digite o nome ou o codigo do produtox"
                    icon={<BagIcon color="#999999" width={15} height={15} />}
                    required
                />

                <Select id="order-date" placeholder="Todos" value={orderDate}
                    onChange={setOrderDate}
                    options={[
                        { label: 'Ultimos 3 meses', value: '90' },
                        { label: 'Ultimos 6 meses', value: '180' },
                        { label: 'Ultimo ano', value: '365' },
                        { label: 'Todos', value: '0' }
                    ]} />
            </div>
        </div>
    )
}

export default MyOrder; 