"use client";

import { useState } from "react";
import { CheckCircle, Eye, EyeOff } from "lucide-react";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import clsx from "clsx";
import Header from "@/app/components/header/page";
import Footer from "@/app/components/footer/page";

export default function RegisterPage() {
  const [password, setPassword] = useState("");
  const [showPassword, setShowPassword] = useState(false);
  const togglePasswordVisibility = () => setShowPassword(!showPassword);

  const passwordRules = [
    {
      label: "Caractere especial",
      isValid: /[@#$!%*?&]/.test(password),
    },
    {
      label: "Letra maiúscula",
      isValid: /[A-Z]/.test(password),
    },
    {
      label: "Letra minúscula",
      isValid: /[a-z]/.test(password),
    },
    {
      label: "Número",
      isValid: /[0-9]/.test(password),
    },
    {
      label: "Oito caracteres",
      isValid: password.length >= 8,
    },
  ];

  return (
    <div className="bg-white p-6 flex flex-col items-center ">
      {/* Logo */}
      <Header />

      {/* Card de Cadastro */}
      <div className="flex flex-col w-[350] items-center justify-center min-h-screen bg-white p-6 gap-2">
        <h1 className="text-2xl font-bold mb-2">Cadastre-se</h1>
        <p className="text-sm text-gray-600 mb-6">
          Lorem ipsum dolor sit amet, consectetur adipisicing elit. Etiam eget
          ligula eu lectus lobortis.
        </p>

        {/* Formulário */}
        <form className="space-y-4">
          <Input type="email" placeholder="E-mail" />

          {/* Campo de senha */}
          <div className="relative">
            <Input
              type={showPassword ? "text" : "password"}
              placeholder="Senha"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
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

          {/* Validação da senha */}
          <ul className="text-sm space-y-1 mt-2">
            {passwordRules.map((rule, idx) => (
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
            ))}
          </ul>

          {/* Termos */}
          <p className="text-xs text-gray-600 mt-4">
            Ao clicar em Aceite e cadastre-se, você aceita os{" "}
            <span className="font-semibold">Termos de Uso</span> e as{" "}
            <span className="font-semibold">Políticas de Privacidade</span>
          </p>

          {/* Botão */}

          <Button
            type="submit"
            className="bg-blue-600 hover:bg-blue-700 text-white w-full mt-4"
          >
            Aceitar e cadastrar-se
          </Button>
        </form>
      </div>
      <Footer />
    </div>
  );
}
