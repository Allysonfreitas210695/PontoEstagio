"use client";

import { useState } from "react";
import Image from "next/image";
import Logo from "../../../../public/assets/image/logo.png";
import { CheckCircle, Eye } from "lucide-react";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import clsx from "clsx";

export default function RegisterPage() {
  const [password, setPassword] = useState("");

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
      <div className="absolute top-8 left-8 pl-12">
        <a href="/">
          <Image src={Logo} alt="Logo" width={150} height={40} />
        </a>
      </div>

      {/* Card de Cadastro */}
      <div className="flex flex-col w-[350] items-center justify-center min-h-screen bg-white p-6 gap-2">
        <h1 className="text-2xl font-bold mb-2">Cadastre-se</h1>
        <p className="text-sm text-gray-600 mb-6">
          Lorem ipsum dolor sit amet, consectetur adipisicing elit. Etiam eget
          ligula eu lectus lobortis.
        </p>

        {/* Formulário */}
        <div className="space-y-4">
          <Input type="email" placeholder="E-mail" />

          {/* Campo de senha */}
          <div className="relative">
            <Input
              type="password"
              placeholder="Senha"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
            />
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
          <Button className="bg-blue-600 hover:bg-blue-700 text-white w-full mt-4">
            Aceitar e cadastrar-se
          </Button>
        </div>
      </div>
    </div>
  );
}
