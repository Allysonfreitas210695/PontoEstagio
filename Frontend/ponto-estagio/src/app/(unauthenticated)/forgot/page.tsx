"use client";

import { useState } from "react";
//import { Eye, EyeOff } from "lucide-react";
import Image from "next/image";
import { useRouter } from "next/navigation";
import { toast } from "sonner";
import logo from "../../../../public/assets/image/logo.png";

export default function LoginPage() {
  //const [showPassword, setShowPassword] = useState(false);
  const [loading, setLoading] = useState(false);
  const [email, setEmail] = useState("");
  const [senha] = useState("");

  const router = useRouter();

  //const togglePasswordVisibility = () => setShowPassword(!showPassword);

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
      <div className="absolute top-8 left-8 pl-12">
        <a href="/">
          <Image src={logo} alt="Logo" width={150} height={40} />
        </a>
      </div>

      <div
        className="bg-white p-8 rounded-2xl shadow-lg w-full max-w-md"
        style={{ minWidth: "300px", maxWidth: "407px", height: "auto" }}
      >
        <h2 className="text-3xl font-bold mb-2 text-black">Recuperar Senha</h2>

        <form className="space-y-4" onSubmit={handleLogin}>
          <div className="relative">
            <input
              type="email"
              placeholder="E-mail"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              required
              className="w-full border border-gray-500 rounded-md py-2 pl-3 pr-10 focus:outline-none focus:ring-2 focus:ring-blue-500 text-gray-700"
            />
          </div>

          <div className="text-left mb-4 text-gray-800">
            <p>
              Enviaremos um código de verificação a este e-mail, caso seja
              associado a uma conta Registra.
            </p>
          </div>

          <button
            type="submit"
            onClick={(e) => {
              e.preventDefault();
              handleLogin(e);
            }}
            disabled={loading}
            className={`w-full bg-blue-600 text-white py-2 rounded-md font-semibold hover:bg-blue-700 transition ${
              loading ? "opacity-50 cursor-not-allowed" : ""
            }`}
          >
            {loading ? "Avançando..." : "Avançar"}
          </button>

          <div className="flex items-center my-4">
            <hr className="flex-grow border-gray-300" />
            <span className="px-2 text-gray-400 text-sm">ou</span>
            <hr className="flex-grow border-gray-300" />
          </div>

          <p className="text-gray-600 text-sm text-center mb-4">
            Você pode retornar à tela de entrada a qualquer momento.
          </p>

          <button
            onClick={() => router.push("/login")}
            type="button"
            className="w-full border-2 border-blue-600 text-blue-600 py-2 rounded-md 
            font-semibold hover:bg-blue-50 transition"
          >
            Entrar
          </button>
        </form>
      </div>
    </div>
  );
}
