"use client";
import { useState } from "react";
import Button from "../ui/button";

const CupomInput = () => {
    const [cupom, setCupom] = useState('');
    // const { aplicarCupom } = useContext(ShopContext);

    const handleApply = () => {
        // aplicarCupom(cupom.trim());
    };
    return (
        <div className="flex items-center justify-between w-full my-2 gap-x-4">
            <input
                type="text"
                value={cupom}
                onChange={(e) => setCupom(e.target.value)}
                placeholder="Digite seu cupom"
                className="flex-1 p-3 uppercase border border-gray-100 rounded"
            />
            <button
                disabled={cupom.trim() === ''}
                onClick={handleApply}
                className={`focus:outline-none transition transform active:scale-95 cursor-pointer border font-bold text-sm uppercase py-3 px-6 rounded text-white  ${cupom.trim() === ''
                    ? 'bg-gray-300 cursor-not-allowed'
                    : 'bg-yellow-primary hover:bg-yellow-primary-hover'
                    }`}
            >
                Aplicar Cupom
            </button>

        </div>
    )
}

export default CupomInput;