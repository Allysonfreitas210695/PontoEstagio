"use client";

import { useState } from "react";
import { useRouter } from "next/navigation";
import { toast } from "sonner";

import { Header } from "@/components/header";
import { Footer } from "@/components/footer";
import PasswordResetModal from "@/components/passwordResetModal";

export default function ForgotPasswordPage() {
  const [loading, setLoading] = useState(false);
  const [email, setEmail] = useState("");
  const [modalOpen, setModalOpen] = useState(false);
  const router = useRouter();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    if (!email) {
      toast.error("Por favor, insira seu e-mail.");
      return;
    }

    setLoading(true);

    try {
      // Simular chamada à API
      await new Promise((resolve) => setTimeout(resolve, 1000));

      toast.success("Código enviado para seu e-mail.");
      setModalOpen(true);
    } catch (error) {
      if (error instanceof Error) return toast.error(error.message);
      toast.error("Erro ao enviar código de verificação.");  
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
        <h2 className="text-3xl font-bold mb-2 text-black">Recuperar Senha</h2>

        <form className="space-y-4" onSubmit={handleSubmit}>
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
            disabled={loading}
            className={`w-full bg-blue-600 text-white py-2 rounded-md font-semibold hover:bg-blue-700 transition ${
              loading ? "opacity-50 cursor-not-allowed" : ""
            }`}
          >
            {loading ? "Enviando..." : "Avançar"}
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

      <PasswordResetModal
        isOpen={modalOpen}
        onClose={() => setModalOpen(false)}
        email={email}
      />
    </div>
  );
}
