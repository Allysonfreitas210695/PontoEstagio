"use client";

import { useState, useEffect } from "react";
import { CheckCircle, Eye, EyeOff } from "lucide-react";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import clsx from "clsx";
import Header from "@/app/components/header/page";
import Footer from "@/app/components/footer/page";
import { useRouter } from "next/navigation";
import { toast } from "react-hot-toast";
import { log } from "console";

export default function RegisterPage() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [showPassword, setShowPassword] = useState(false);
  const [userType, setUserType] = useState<string | null>(null);
  const [isLoading, setIsLoading] = useState(false);
  const router = useRouter();
  
  useEffect(() => {
    // Recupera o tipo de usuário do localStorage quando a página carrega
    const storedUserType = localStorage.getItem('userType');
    if (storedUserType) {
      setUserType(storedUserType);
    }
  }, []);

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

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setIsLoading(true);

    // Verifica se todas as regras de senha são válidas
    const isPasswordValid = passwordRules.every(rule => rule.isValid);
    if (!isPasswordValid) {
      toast.error("Por favor, crie uma senha que atenda a todos os requisitos");
      setIsLoading(false);
      return;
    }

    try {
  console.log(process.env.NEXT_PUBLIC_API_BASE_URL);
  
  const response = await fetch(`${process.env.NEXT_PUBLIC_API_BASE_URL}users/check-user`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({
      email,
      password,
      type: userType
    }),
  });

  const data = await response.json();

  if (!response.ok) {
    throw new Error(data.message || 'Erro ao cadastrar usuário');
  }

  toast.success('Cadastro realizado com sucesso!');
      localStorage.setItem("userEmail", email);
      localStorage.setItem("userPassword", password);

      if (userType === "0") {
        router.push('/register/aluno');
      } else if (userType === "3") {
        router.push('/register/coordenador');
      } else {
        router.push('/');
      }
      
    } catch (error: any) {
      toast.error(error.message || 'Erro ao cadastrar usuário');
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <div className="bg-white p-6 flex flex-col items-center ">
      {/* Logo */}
      <Header />

      {/* Card de Cadastro */}
      <div className="flex flex-col w-[350] items-center justify-center min-h-screen bg-white p-6 gap-2">
        <h1 className="text-2xl font-bold mb-2">Cadastre-se</h1>
        <p className="text-sm text-gray-600 mb-6">
          {userType === "0" ? "Cadastro de Aluno" : userType === "3" ? "Cadastro de Coordenador" : "Cadastro"}
        </p>

        {/* Formulário */}
        <form className="space-y-4" onSubmit={handleSubmit}>
          <Input 
            type="email" 
            placeholder="E-mail" 
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
          />

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
            disabled={isLoading}
          >
            {isLoading ? "Cadastrando..." : "Aceitar e cadastrar-se"}
          </Button>
        </form>
      </div>
      <Footer />
    </div>
  );
}