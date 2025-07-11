"use client";

import { useState } from "react";
import { Eye, EyeOff } from "lucide-react";
import { useRouter } from "next/navigation";
import { toast } from "sonner";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import Link from "next/link";
import Header from "@/app/components/header/page";
import Footer from "@/app/components/footer/page";

export default function LoginPage() {
  const [showPassword, setShowPassword] = useState(false);
  const [loading, setLoading] = useState(false);
  const [email, setEmail] = useState("");
  const [senha, setSenha] = useState("");

  const router = useRouter();

  const togglePasswordVisibility = () => setShowPassword(!showPassword);

  const handleLogin = async (e: React.FormEvent) => {
    e.preventDefault();

    setLoading(true);

    try {
      const res = await fetch("/api/auth/login", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ email, senha }),
      });

      const data = await res.json();

      if (res.ok) {
        toast.success("Login realizado com sucesso!");
        // redireciona, se necessário
        router.push("/dashboard");
      } else {
        toast.error(data?.message || "Falha ao fazer login.");
      }
      // eslint-disable-next-line @typescript-eslint/no-unused-vars
    } catch (error) {
      toast.error("Erro inesperado ao tentar logar.");
    } finally {
      setLoading(false);
    }
  };

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

        <form className="space-y-4" onSubmit={handleLogin}>
          <div className="relative">
            <Input
              type="email"
              placeholder="E-mail"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              required
              className="w-full border border-gray-500 rounded-md py-2 pl-3 pr-10 focus:outline-none focus:ring-2 focus:ring-blue-500 text-gray-700"
            />
          </div>

          <div className="relative">
            <Input
              type={showPassword ? "text" : "password"}
              placeholder="Senha"
              value={senha}
              onChange={(e) => setSenha(e.target.value)}
              required
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
            disabled={loading}
            className={`w-full bg-blue-600 text-white py-2 rounded-md font-semibold hover:bg-blue-700 transition ${
              loading ? "opacity-50 cursor-not-allowed" : ""
            }`}
          >
            {loading ? "Entrando..." : "Entrar"}
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
            className="bg-gray-100 w-full border-2 border-blue-600 text-blue-600 py-2 rounded-md 
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
