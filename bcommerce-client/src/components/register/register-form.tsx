"use client";

import React, { useRef, useState } from "react";
import { z } from "zod";
import Input from "../ui/input";
import Button from "../ui/button";
import { useRouter } from "next/navigation";
import { toast } from "react-toastify";
import { useAuth } from "@/context/auth-context"; // ✅ Importa o contexto

// Schema de validação
const registerSchema = z.object({
    name: z.string().nonempty("O nome é obrigatório"),
    email: z
        .string()
        .nonempty("O email é obrigatório")
        .email("Formato de email inválido"),
    password: z
        .string()
        .nonempty("A senha é obrigatória")
        .min(6, "A senha deve ter no mínimo 6 caracteres"),
});

type RegisterFormData = z.infer<typeof registerSchema>;

const RegisterForm: React.FC = () => {
    const formRef = useRef<HTMLFormElement>(null);
    const router = useRouter();
    const { register } = useAuth(); // ✅ Hook de contexto
    const [loading, setLoading] = useState(false);

    const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        if (!formRef.current) return;

        const formData = new FormData(formRef.current);
        const rawData = Object.fromEntries(formData.entries());

        const parsed = registerSchema.safeParse(rawData);

        if (!parsed.success) {
            const errors = parsed.error.flatten().fieldErrors;
            const errorMsg = Object.values(errors).flat().join("\n");
            toast.error(errorMsg || "Erro na validação");
            return;
        }

        const data: RegisterFormData = parsed.data;
        setLoading(true);

        try {
            const result = await register(data); // ✅ Chama o contexto

            if (result.success) {
                toast.success("Conta criada com sucesso! 🎉");
                router.push("/login");
            } else {
                const errorMessages = result.errors
                    .map((err) => err.message)
                    .join("\n");

                toast.error(errorMessages || "Erro ao criar conta ❌");
            }
        } catch (error) {
            toast.error("Erro de conexão com o servidor ❌");
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="max-w-sm w-full mx-auto p-6 bg-white rounded shadow">
            <form ref={formRef} onSubmit={handleSubmit} className="space-y-4">
                <h3 className="text-xl font-medium text-gray-900">Crie sua Conta</h3>

                <Input
                    id="name"
                    name="name"
                    label="Nome"
                    placeholder="Digite seu nome completo"
                    required
                />

                <Input
                    id="email"
                    name="email"
                    label="Email"
                    placeholder="Digite o email"
                    required
                />

                <Input
                    id="password"
                    name="password"
                    label="Senha"
                    placeholder="Digite a senha"
                    type="password"
                    required
                />

                <Button
                    type="submit"
                    variant="primary"
                    size="medium"
                    fullWidth
                    isLoading={loading}
                >
                    Cadastrar
                </Button>
            </form>
        </div>
    );
};

export default RegisterForm;

// "use client";

// import React, { useRef, useState } from "react";
// import { z } from "zod";
// import Input from "../ui/input";
// import Button from "../ui/button";
// import { useRouter } from "next/navigation";
// import { toast } from "react-toastify";
// import { createCustomer } from "@/services/customers/create-customer-service";

// const registerSchema = z.object({
//     name: z.string().nonempty("O nome é obrigatório"),
//     email: z
//         .string()
//         .nonempty("O email é obrigatório")
//         .email("Formato de email inválido"),
//     password: z
//         .string()
//         .nonempty("A senha é obrigatória")
//         .min(6, "A senha deve ter no mínimo 6 caracteres"),
// });

// type RegisterFormData = z.infer<typeof registerSchema>;

// const RegisterForm: React.FC = () => {
//     const formRef = useRef<HTMLFormElement>(null);
//     const router = useRouter();
//     const [loading, setLoading] = useState(false);

//     const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
//         e.preventDefault();

//         if (!formRef.current) return;

//         const formData = new FormData(formRef.current);
//         const rawData = Object.fromEntries(formData.entries());

//         const parsed = registerSchema.safeParse(rawData);

//         if (!parsed.success) {
//             const errors = parsed.error.flatten().fieldErrors;
//             const errorMsg = Object.values(errors).flat().join("\n");
//             toast.error(errorMsg || "Erro na validação");
//             return;
//         }

//         const data: RegisterFormData = parsed.data;

//         setLoading(true);

//         try {
//             const result = await createCustomer(data);

//             if (result.success) {
//                 toast.success("Conta criada com sucesso! 🎉");
//                 router.push("/login");
//             } else {
//                 const errorMessages = result.errors
//                     .map((err) => err.message)
//                     .join("\n");

//                 toast.error(errorMessages || "Erro ao criar conta ❌");
//             }
//         } catch (error) {
//             toast.error("Erro de conexão com o servidor ❌");
//         } finally {
//             setLoading(false);
//         }
//     };

//     return (
//         <div className="max-w-sm w-full mx-auto p-6 bg-white rounded shadow">
//             <form ref={formRef} onSubmit={handleSubmit} className="space-y-4">
//                 <h3 className="text-xl font-medium text-gray-900">Crie sua Conta</h3>

//                 <Input
//                     id="name"
//                     name="name"
//                     label="Nome"
//                     placeholder="Digite seu nome completo"
//                     required
//                 />

//                 <Input
//                     id="email"
//                     name="email"
//                     label="Email"
//                     placeholder="Digite o email"
//                     required
//                 />

//                 <Input
//                     id="password"
//                     name="password"
//                     label="Senha"
//                     placeholder="Digite a senha"
//                     type="password"
//                     required
//                 />

//                 <Button
//                     type="submit"
//                     variant="primary"
//                     size="medium"
//                     fullWidth
//                     isLoading={loading}
//                 >
//                     Cadastrar
//                 </Button>
//             </form>
//         </div>
//     );
// };

// export default RegisterForm;


