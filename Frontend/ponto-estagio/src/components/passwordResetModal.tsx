"use client";

import { Dialog } from "@headlessui/react";
import { useState, useEffect } from "react";
import { X } from "lucide-react";

interface PasswordResetModalProps {
  isOpen: boolean;
  onClose: () => void;
  email: string;
}

export default function PasswordResetModal({ isOpen, onClose, email }: PasswordResetModalProps) {
  const [timeLeft, setTimeLeft] = useState(180); // 3 minutos
  const [verificationCode, setVerificationCode] = useState<string[]>(Array(6).fill(""));
  const [password, setPassword] = useState("");
  const [isSubmitting, setIsSubmitting] = useState(false);

  useEffect(() => {
    if (!isOpen) return;

    const interval = setInterval(() => {
      setTimeLeft((prev) => (prev > 0 ? prev - 1 : 0));
    }, 1000);

    return () => clearInterval(interval);
  }, [isOpen]);

  const formatTime = () => {
    const minutes = Math.floor(timeLeft / 60);
    const seconds = timeLeft % 60;
    return `${String(minutes).padStart(2, "0")}:${String(seconds).padStart(2, "0")}`;
  };

  const handleCodeChange = (value: string, index: number) => {
    const newCode = [...verificationCode];
    newCode[index] = value;
    setVerificationCode(newCode);

    // Auto focus next input
    if (value && index < 5) {
      const nextInput = document.getElementById(`verification-code-${index + 1}`);
      if (nextInput) nextInput.focus();
    }
  };

  const handleSubmit = async () => {
    if (timeLeft === 0) {
      alert("O tempo expirou. Por favor, solicite um novo código.");
      return;
    }

    if (password.length < 8) {
      alert("A senha deve ter pelo menos 8 caracteres.");
      return;
    }

    const code = verificationCode.join("");
    if (code.length !== 6) {
      alert("Por favor, preencha todos os campos do código de verificação.");
      return;
    }

    setIsSubmitting(true);
    try {
      // Simular chamada à API
      await new Promise(resolve => setTimeout(resolve, 1000));
      alert("Senha redefinida com sucesso!");
      onClose();
    } catch (error) {
      console.log(error)
      alert("Erro ao redefinir senha. Por favor, tente novamente.");
    } finally {
      setIsSubmitting(false);
    }
  };

  return (
    <Dialog open={isOpen} onClose={onClose} className="fixed z-50 inset-0 overflow-y-auto">
      <div className="flex items-center justify-center min-h-screen px-4">
        <div className="fixed inset-0 bg-black bg-opacity-30" />

        <div className="relative bg-white rounded-2xl shadow-xl w-full max-w-md p-6 z-50">
          <button
            onClick={onClose}
            className="absolute top-3 right-3 text-gray-500 hover:text-gray-700"
          >
            <X />
          </button>

          <Dialog.Title className="text-2xl font-bold mb-2">
            <span className="text-black">Recuperar <span className="underline text-blue-600">Senha</span></span>
          </Dialog.Title>

          <p className="text-sm text-gray-600 mb-4">
            Por favor, insira sua nova senha e o código de verificação enviado para seu e-mail cadastrado.
          </p>

          <input
            type="password"
            placeholder="Senha"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            className="w-full border border-gray-400 rounded-md py-2 px-3 text-gray-700 mb-4"
          />

          <ul className="text-sm text-gray-600 mb-4 space-y-1">
            <li>✔️ Caractere especial</li>
            <li>✔️ Letra maiúscula</li>
            <li>✔️ Letra minúscula</li>
            <li>✔️ Número</li>
            <li>✔️ Oito caracteres</li>
          </ul>

          <p className="text-sm text-gray-700 mb-1">Código de Verificação</p>
          <div className="flex justify-between mb-2">
            {Array.from({ length: 6 }).map((_, i) => (
              <input
                key={i}
                id={`verification-code-${i}`}
                maxLength={1}
                value={verificationCode[i]}
                onChange={(e) => handleCodeChange(e.target.value, i)}
                className="w-10 h-10 border text-center rounded-md border-gray-400"
              />
            ))}
          </div>

          <p className="text-xs text-gray-500 mb-2">Tempo restante: {formatTime()}</p>
          <p className="text-xs text-gray-500 mb-4">Enviado para: <strong>{email}</strong></p>

          <div className="flex gap-2">
            <button
              onClick={onClose}
              className="w-1/2 border border-gray-400 text-gray-700 py-2 rounded-md"
            >
              Cancelar
            </button>
            <button 
              onClick={handleSubmit}
              disabled={isSubmitting}
              className={`w-1/2 bg-blue-600 text-white py-2 rounded-md hover:bg-blue-700 ${
                isSubmitting ? "opacity-50 cursor-not-allowed" : ""
              }`}
            >
              {isSubmitting ? "Salvando..." : "Salvar"}
            </button>
          </div>
        </div>
      </div>
    </Dialog>
  );
}