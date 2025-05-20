"use client";
import React, { useRef, useState } from "react";
import { z } from "zod";
import Button from "../ui/button";
import Link from "next/link";
import Input from "../ui/input";
import { toast } from "react-toastify";


const shippingSchema = z.object({
    zipCode: z
        .string()
        .min(8, "CEP deve ter no mínimo 8 dígitos")
        .max(9, "CEP inválido")
        .regex(/^\d{5}-?\d{3}$/, "Formato de CEP inválido"),
});

type ShippingFormData = z.infer<typeof shippingSchema>;

const ShippingForm: React.FC = () => {
    const formRef = useRef<HTMLFormElement>(null);
    const [loading, setLoading] = useState(false);

    const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        if (!formRef.current) return;
        const formData = new FormData(formRef.current);
        const rawData = Object.fromEntries(formData.entries());
        const parsed = shippingSchema.safeParse(rawData);
        if (!parsed.success) {
            const errors = parsed.error.flatten().fieldErrors;
            const errorMsg = Object.values(errors).flat().join("\n");
            toast.error(errorMsg || "Erro na validação");
            return;
        }
        const data: ShippingFormData = parsed.data;
        setLoading(true);
        try {

        } catch {

        } finally {

        }
    }

    return (
        <div className="flex items-center gap-4 ">
            <form ref={formRef} onSubmit={handleSubmit} className="flex items-center gap-4">
                <Input
                    id="zipCode"
                    name="zipCode"
                    placeholder="Digite o CEP"
                    required
                    type="text"
                    maxLength={9}
                />
                <Button isLoading={loading} type="submit" variant="primary">
                    OK
                </Button>
            </form>

            <Link href={"https://buscacepinter.correios.com.br/app/endereco/index.php?t"} >
                <span className="underline text-yellow-primary font-bold text-sm">Nao lembro meu cep</span>
            </Link>
        </div>
    );
};

export default ShippingForm;
