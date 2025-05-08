"use client";
import React, { useRef, useState } from "react";
import { z } from "zod";
import Input from "../ui/input";
import Button from "../ui/button";
import { useRouter } from "next/navigation";
import { toast } from "react-toastify";
import { useAuth } from "@/context/auth-context"; // ✅ CONTEXTO

// ✅ Schema de validação
const loginSchema = z.object({
    email: z
        .string()
        .nonempty("O email é obrigatório")
        .email("Formato de email inválido"),
    password: z
        .string()
        .nonempty("A senha é obrigatória")
        .min(6, "A senha deve ter no mínimo 6 caracteres"),
});

type LoginFormData = z.infer<typeof loginSchema>;

const LoginForm: React.FC = () => {
    const formRef = useRef<HTMLFormElement>(null);
    const router = useRouter();
    const { login } = useAuth(); // ✅ USA O CONTEXTO
    const [loading, setLoading] = useState(false);

    const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        if (!formRef.current) return;

        const formData = new FormData(formRef.current);
        const rawData = Object.fromEntries(formData.entries());

        const parsed = loginSchema.safeParse(rawData);

        if (!parsed.success) {
            const errors = parsed.error.flatten().fieldErrors;
            const errorMsg = Object.values(errors).flat().join("\n");
            toast.error(errorMsg || "Erro na validação");
            return;
        }

        const data: LoginFormData = parsed.data;
        setLoading(true);

        try {
            const result = await login(data); // ✅ CHAMADA DIRETA AO CONTEXTO

            if (result.success) {
                toast.success("Login realizado com sucesso! 🎉");
                router.push("/perfil");
            } else {
                const errorMessages = result.errors.map((e) => e.message).join("\n");
                toast.error(errorMessages || "Erro ao fazer login ❌");
            }
        } catch {
            toast.error("Erro de conexão com o servidor ❌");
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="max-w-sm w-full mx-auto p-6 bg-white rounded shadow">
            <form ref={formRef} onSubmit={handleSubmit} className="space-y-4">
                <h3 className="text-xl font-medium text-gray-900">Acessar sua Conta</h3>

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
                    Entrar
                </Button>
            </form>
        </div>
    );
};

export default LoginForm;



// "use client";

// import React, { useRef, useState } from "react";
// import { z } from "zod";
// import Input from "../ui/input";
// import Button from "../ui/button";
// import { useRouter } from "next/navigation";
// import { toast } from "react-toastify";
// import { signInCustomer } from "@/services/customers/login-customer-service";
// import { useAuth } from "@/context/auth-context"; // 👈 IMPORTANTE!

// // ✅ Schema de validação
// const loginSchema = z.object({
//     email: z.string().nonempty("O email é obrigatório").email("Formato de email inválido"),
//     password: z.string().nonempty("A senha é obrigatória").min(6, "A senha deve ter no mínimo 6 caracteres"),
// });

// type LoginFormData = z.infer<typeof loginSchema>;

// const LoginForm: React.FC = () => {
//     const formRef = useRef<HTMLFormElement>(null);
//     const router = useRouter();
//     const { login } = useAuth(); // 👈 USANDO O CONTEXTO
//     const [loading, setLoading] = useState(false);

//     const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
//         e.preventDefault();

//         if (!formRef.current) return;

//         const formData = new FormData(formRef.current);
//         const rawData = Object.fromEntries(formData.entries());

//         const parsed = loginSchema.safeParse(rawData);

//         if (!parsed.success) {
//             const errors = parsed.error.flatten().fieldErrors;
//             const errorMsg = Object.values(errors).flat().join("\n");
//             toast.error(errorMsg || "Erro na validação");
//             return;
//         }

//         const data: LoginFormData = parsed.data;
//         setLoading(true);

//         try {
//             const result = await signInCustomer(data);

//             if (result.success) {
//                 toast.success("Login realizado com sucesso! 🎉");

//                 // ✅ Usa o contexto para salvar o token globalmente
//                 login(result.data.token);

//                 router.push("/perfil");
//             } else {
//                 const errorMessages = result.errors.map((e) => e.message).join("\n");
//                 toast.error(errorMessages || "Erro ao fazer login ❌");
//             }
//         } catch {
//             toast.error("Erro de conexão com o servidor ❌");
//         } finally {
//             setLoading(false);
//         }
//     };

//     return (
//         <div className="max-w-sm w-full mx-auto p-6 bg-white rounded shadow">
//             <form ref={formRef} onSubmit={handleSubmit} className="space-y-4">
//                 <h3 className="text-xl font-medium text-gray-900">Acessar sua Conta</h3>

//                 <Input id="email" name="email" label="Email" placeholder="Digite o email" required />
//                 <Input id="password" name="password" label="Senha" placeholder="Digite a senha" type="password" required />

//                 <Button type="submit" variant="primary" size="medium" fullWidth isLoading={loading}>
//                     Entrar
//                 </Button>
//             </form>
//         </div>
//     );
// };

// export default LoginForm;


