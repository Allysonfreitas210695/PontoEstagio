"use client";

import { Dialog } from "@headlessui/react";
import { useState, useEffect } from "react";
import { X, Eye, EyeOff, CheckCircle } from "lucide-react";
import clsx from "clsx";
import { getPasswordRules } from "@/utils/passwordRules";

interface PasswordResetModalProps {
  isOpen: boolean;
  onClose: () => void;
  email: string;
}

export default function PasswordResetModal({
  isOpen,
  onClose,
  email,
}: PasswordResetModalProps) {
  const [timeLeft, setTimeLeft] = useState(180); // 3 minutos
  const [verificationCode, setVerificationCode] = useState<string[]>(
    Array(6).fill("")
  );
  const [password, setPassword] = useState("");
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [showPassword, setShowPassword] = useState(false);
  const [passwordRules, setPasswordRules] = useState(getPasswordRules(""));

  useEffect(() => {
    if (!!password) setPasswordRules(getPasswordRules(password));
  }, [password]);

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
    return `${String(minutes).padStart(2, "0")}:${String(seconds).padStart(
      2,
      "0"
    )}`;
  };

  const handleCodeChange = (value: string, index: number) => {
    const newCode = [...verificationCode];
    newCode[index] = value;
    setVerificationCode(newCode);

    // Auto focus next input
    if (value && index < 5) {
      const nextInput = document.getElementById(
        `verification-code-${index + 1}`
      );
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
      await new Promise((resolve) => setTimeout(resolve, 1000));
      alert("Senha redefinida com sucesso!");
      onClose();
    } catch (error) {
      console.log(error);
      alert("Erro ao redefinir senha. Por favor, tente novamente.");
    } finally {
      setIsSubmitting(false);
    }
  };

  return (
    <Dialog
      open={isOpen}
      onClose={onClose}
      className="fixed inset-0 z-50 flex items-center justify-center px-4"
    >
      <div
        className="fixed inset-0 bg-black/70 backdrop-blur-sm"
        aria-hidden="true"
      />

      <div className="relative z-50 w-full max-w-md bg-white backdrop-blur-xl rounded-2xl border border-white/30 p-6 shadow-xl " style={{ maxHeight: "90vh", maxWidth: "300px" }}>
        <button
          onClick={onClose}
          className="absolute top-3 right-3 text-gray-600 hover:text-black"
        >
          <X />
        </button>

        <Dialog.Title className="text-4xl font-bold text-black mb-5 leading-tight">
          Recuperar <br /> Senha
        </Dialog.Title>

        <p className="text-sm text-gray-800 mb-4">
          Por favor, insira sua nova senha e o código de verificação enviado
          para seu e-mail cadastrado.
        </p>

        {/* Campo de Senha */}
        <div className="relative mb-4">
          <input
            type={showPassword ? "text" : "password"}
            placeholder="Senha"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            className="w-full border border-gray-300 rounded-md py-2 px-3 pr-10 text-sm text-gray-800 placeholder-gray-500 focus:outline-none focus:ring-2 focus:ring-blue-500 bg-white"
          />
          <button
            type="button"
            onClick={() => setShowPassword(!showPassword)}
            className="absolute right-3 top-2.5 text-gray-500 hover:text-gray-700"
          >
            {showPassword ? <EyeOff size={18} /> : <Eye size={18} />}
          </button>
        </div>

        {/* Requisitos de senha */}
        <ul className="text-sm text-gray-600 mb-4 space-y-1">
          {passwordRules.map(
            (rule: { label: string; isValid: boolean }, idx: number) => (
              <li
                key={idx}
                className={clsx(
                  "flex items-center",
                  rule.isValid ? "text-green-600" : "text-black"
                )}
              >
                <CheckCircle className="w-4 h-4 mr-2" />
                {rule.label}
              </li>
            )
          )}
        </ul>

        <p className="text-sm text-gray-800 mb-1 font-bold text-center p-4">Código de Verificação</p>
        <div className="flex justify-between mb-2 gap-1">
          {Array.from({ length: 6 }).map((_, i) => (
            <input
              key={i}
              id={`verification-code-${i}`}
              maxLength={1}
              value={verificationCode[i]}
              onChange={(e) => handleCodeChange(e.target.value, i)}
              className="w-10 h-10 border text-center rounded-md border-gray-300 text-lg bg-white focus:outline-none focus:ring-2 focus:ring-blue-500"
            />
          ))}
        </div>

        <p className="text-xs text-gray-500 mb-2">
          Tempo restante: {formatTime()}
        </p>
        <p className="text-xs text-gray-500 mb-4">
          Enviado para: <strong>{email}</strong>
        </p>

        <div className="flex gap-2">
          <button
            onClick={onClose}
            className="w-1/2 border border-gray-400 text-gray-700 py-2 rounded-md hover:bg-gray-100 text-sm"
          >
            Cancelar
          </button>
          <button
            onClick={handleSubmit}
            disabled={isSubmitting}
            className={`w-1/2 bg-blue-600 text-white py-2 rounded-md hover:bg-blue-700 text-sm ${
              isSubmitting ? "opacity-50 cursor-not-allowed" : ""
            }`}
          >
            {isSubmitting ? "Salvando..." : "Salvar"}
          </button>
        </div>
      </div>
    </Dialog>
  );
}
