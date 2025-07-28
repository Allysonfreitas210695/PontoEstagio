"use client";

import { useState } from "react";
import { useRouter } from "next/navigation";
import Link from "next/link";
import { toast } from "sonner";
import { Eye, EyeOff } from "lucide-react";
import { useForm } from "react-hook-form";

import { Header } from "@/components/header";
import { Footer } from "@/components/footer";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { login } from "@/api/users_api";
import Loading from "@/components/loading";

export type LoginFormData = {
  email: string;
  senha: string;
};

export default function LoginPage() {
  const [showPassword, setShowPassword] = useState(false);
  const [isLoading, setIsLoading] = useState(false);

  const router = useRouter();

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<LoginFormData>();

  const togglePasswordVisibility = () => setShowPassword(!showPassword);

  const onSubmit = async (data: LoginFormData) => {
    setIsLoading(true); 
    try {
      await login(data);
      toast.success("Login realizado com sucesso!");
      router.push("/dashboard");
    } catch (error) {
      if (error instanceof Error) {
        toast.error(error.message);
      } else {
        toast.error("Falha ao fazer login.");
      }
    } finally {
      setIsLoading(false);
    }
  };

  if(isLoading) return <Loading/>;

  return (
    <div className="min-h-screen flex flex-col items-center justify-center bg-gray-50 px-4">
      <Header />

      <div
        className="bg-white p-8 rounded-2xl shadow-lg w-full max-w-md"
        style={{ minWidth: "300px", maxWidth: "407px", height: "auto" }}
      >
        <h2 className="text-3xl font-bold mb-2 text-black">Entrar</h2>
        <p className="text-sm text-gray-600 mb-6">
          Insira seus dados de login para acessar o Registra, seu sistema para
          controle de ponto.
        </p>

        <form className="space-y-4" onSubmit={handleSubmit(onSubmit)}>
          <div className="relative">
            <Input
              type="email"
              placeholder="E-mail"
              {...register("email", { required: "E-mail é obrigatório" })}
              className="w-full border border-gray-500 rounded-md py-2 pl-3 pr-10 focus:outline-none focus:ring-2 focus:ring-blue-500 text-gray-700"
            />
            {errors.email && (
              <p className="text-red-500 text-sm mt-1">
                {errors.email.message}
              </p>
            )}
          </div>

          <div className="relative">
            <Input
              type={showPassword ? "text" : "password"}
              placeholder="Senha"
              {...register("senha", { required: "Senha é obrigatória" })}
              className="w-full border border-gray-500 rounded-md 
              py-2 pl-3 pr-10 focus:outline-none focus:ring-2 focus:ring-blue-500
              text-gray-700"
            />
            <button
              type="button"
              className="absolute right-3 top-1/2 transform -translate-y-1/2 text-gray-500"
              onClick={togglePasswordVisibility}
            >
              {showPassword ? <EyeOff size={20} /> : <Eye size={20} />}
            </button>
            {errors.senha && (
              <p className="text-red-500 text-sm mt-1">
                {errors.senha.message}
              </p>
            )}
          </div>

          <div className="text-left mb-4">
            <Link
              href="/forgot"
              className="text-blue-600 text-sm font-semibold hover:underline"
            >
              Esqueceu sua senha?
            </Link>
          </div>

          <Button
            type="submit"
            disabled={isLoading}
            className={`w-full cursor-pointer bg-blue-600 text-white py-2 rounded-md font-semibold hover:bg-blue-700 transition ${
              isLoading ? "opacity-50 cursor-not-allowed" : ""
            }`}
          >
            {isLoading ? "Entrando..." : "Entrar"}
          </Button>

          <div className="flex items-center my-4">
            <hr className="flex-grow border-gray-300" />
            <span className="px-2 text-gray-400 text-sm">ou</span>
            <hr className="flex-grow border-gray-300" />
          </div>

          <p className="text-gray-600 text-sm text-center mb-4">
            Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam eget
            ligula eu lectus lobortis.
          </p>

          <Button
            onClick={() => router.push("/select")}
            type="button"
            className="bg-gray-100 cursor-pointer w-full border-2 border-blue-600 text-blue-600 py-2 rounded-md 
            font-semibold hover:bg-blue-50 transition"
          >
            Cadastre-se Agora
          </Button>
        </form>
      </div>
      <Footer />
    </div>
  );
}
