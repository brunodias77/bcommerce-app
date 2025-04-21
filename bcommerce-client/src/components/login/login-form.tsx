"use client";

import React, { useRef, useState } from "react";
import { z } from "zod";
import Input from "../ui/input";
import Button from "../ui/button";
import { useRouter } from "next/navigation";
import { toast } from "react-toastify";
import { createCustomer } from "@/services/customer-service";

const registerSchema = z.object({
    name: z.string().nonempty("O nome é obrigatório"),
    email: z.string().nonempty("O email é obrigatório").email("Formato de email inválido"),
    password: z
        .string()
        .nonempty("A senha é obrigatória")
        .min(6, "A senha deve ter no mínimo 6 caracteres"),
});

type RegisterFormData = z.infer<typeof registerSchema>;

const RegisterForm: React.FC = () => {
    const formRef = useRef<HTMLFormElement>(null);
    const router = useRouter();
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

        try {
            setLoading(true);

            const result = await createCustomer({
                name: data.name,
                email: data.email,
                password: data.password,
            });

            if (result.success) {
                toast.success("Conta criada com sucesso! 🎉");
                router.push("/login");
            } else {
                const errorMessages = result.errors.map((e) => e.message).join("\n");
                toast.error(errorMessages || "Erro ao criar conta ❌");
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
                <h3 className="text-xl font-medium text-gray-900">Criar Conta</h3>

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

// const loginSchema = z.object({
//     email: z
//         .string()
//         .nonempty("O email é obrigatório")
//         .email("Formato de email inválido"),
//     password: z
//         .string()
//         .nonempty("A senha é obrigatória")
//         .min(6, "A senha deve ter no mínimo 6 caracteres"),
// });
// type LoginFormData = z.infer<typeof loginSchema>;

// const LoginForm: React.FC = () => {
//     const formRef = useRef<HTMLFormElement>(null);
//     const router = useRouter();
//     const [loading, setLoading] = useState(false);

//     const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
//         // 🛑 Impede o comportamento padrão do formulário (recarregar a página)
//         e.preventDefault();

//         // 🚫 Se o form ainda não estiver montado, interrompe
//         if (!formRef.current) return;

//         // 🧺 Cria um objeto FormData com os campos do formulário
//         const formData = new FormData(formRef.current);

//         // 🔄 Converte FormData em um objeto JS comum: { email: '...', password: '...' }
//         const rawData = Object.fromEntries(formData.entries());

//         console.log("rawData", rawData);
//         console.log(rawData);

//         // ✅ Validação manual usando Zod
//         // safeParse retorna { success: true, data } ou { success: false, error }
//         const parsed = loginSchema.safeParse(rawData);

//         // ❌ Se houver erros de validação:
//         if (!parsed.success) {
//             // 🔎 Coleta os erros de cada campo
//             const errors = parsed.error.flatten().fieldErrors;

//             // 💬 Une as mensagens de erro em uma única string
//             const errorMsg = Object.values(errors)
//                 .flat() // transforma em array plano
//                 .join("\n"); // junta as mensagens separadas por quebra de linha

//             // 🔔 Mostra erro visual com toast
//             toast.error(errorMsg || "Erro na validação");
//             return;
//         }

//         // ✅ Se passou na validação, pegamos os dados tipados (com segurança)
//         const data: LoginFormData = parsed.data;

//         try {
//             setLoading(true);
//             const res = await fetch("/api/login", {
//                 method: "POST",
//                 headers: { "Content-Type": "application/json" },
//                 body: JSON.stringify(data),
//             });

//             if (!res.ok) {
//                 const json = await res.json();
//                 toast.error(json.message || "Erro ao fazer login ❌");
//                 return;
//             }

//             const { token } = await res.json();
//             localStorage.setItem("token", token);
//             toast.success("Login realizado com sucesso! 🎉");

//             router.push("/dashboard");
//         } catch (err) {
//             toast.error("Erro de conexão com o servidor ❌");
//         } finally {
//             setLoading(false);
//         }

//     };

//     return (
//         <div className="max-w-sm w-full mx-auto p-6 bg-white rounded shadow">
//             <form ref={formRef} onSubmit={handleSubmit} className="space-y-4">
//                 <h3 className="text-xl font-medium text-gray-900">Acessar Conta</h3>

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
//                     Entrar
//                 </Button>
//             </form>
//         </div>
//     );
// }

// export default LoginForm;