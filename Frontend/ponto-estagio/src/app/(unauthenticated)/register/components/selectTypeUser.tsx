"use client";

import { useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import { useRouter, useSearchParams } from "next/navigation";
import { CheckCircle, Eye, EyeOff } from "lucide-react";
import clsx from "clsx";

import { Header } from "@/components/header";
 import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import { toast } from "sonner";

import { CheckUser } from "@/api/users_api";
import { getPasswordRules, isPasswordValid } from "@/utils/passwordRules";
 import { HandleSelectProps } from "../page";

type FormData = {
  email: string;
  password: string;
};

const userTypeList = [
  "Intern",
  "Supervisor",
  "Coordinator",
  "Admin",
  "Advisor",
] as const;

type typeUser = (typeof userTypeList)[number];

interface IProps {
  onChangeUserType: (propsSelectTypeUse: HandleSelectProps) => void;
}

export default function SelectTypeUser({ onChangeUserType }: IProps) {
  const searchParams = useSearchParams();
  const router = useRouter();

  const typeParam = searchParams.get("type");
  const isValidType = userTypeList.includes(typeParam as typeUser);
  const type = isValidType ? (typeParam as typeUser) : null;

  const [showPassword, setShowPassword] = useState(false);
  const [passwordRules, setPasswordRules] = useState(getPasswordRules(""));

  const {
    register,
    handleSubmit,
    watch,
    formState: { errors, isSubmitting },
  } = useForm<FormData>();

  const password = watch("password");

  useEffect(() => {
    if (!!password) setPasswordRules(getPasswordRules(password));
  }, [password]);

  useEffect(() => {
    if (!type) {
      router.push("/select");
    }
  }, [type, router]);

  const onSubmit = async (data: FormData) => {
    if (!isPasswordValid(data.password)) {
      toast.error(
        "Por favor, crie uma senha que atenda a todos os requisitos."
      );
      return;
    }

    try {
       await CheckUser({
        type: type!,
        email: data.email,
        password: data.password,
      });

      toast.success("Cadastro realizado com sucesso!");

      if (type === "Intern")
        onChangeUserType({
          email: data.email,
          password: data.password,
          type: "Intern",
        });
      else
        onChangeUserType({
          email: data.email,
          password: data.password,
          type: "Coordinator",
        });
    } catch (error) {
       if (error instanceof Error){
         console.log(error)
      }
      else toast.error("Erro ao cadastrar usuário");
    }
  };

  const togglePasswordVisibility = () => setShowPassword(!showPassword);

  return (
    <div className="bg-white flex flex-col items-center justify-center  min-h-screen">
      <Header />

      <main className=" flex items-center justify-center px-4 bg-white p-8 rounded-2xl shadow-lg w-full max-w-md " style={{ minWidth: "300px", maxWidth: "350px", height: "auto" }}>
        <div className="w-full max-w-md p-6 flex flex-col gap-2">
          <h1 className="text-2xl font-bold mb-2">Cadastre-se</h1>
          <p className="text-sm text-gray-600 mb-6">
            {type === "Intern"
              ? "Cadastro de Aluno"
              : type === "Coordinator"
              ? "Cadastro de Coordenador"
              : "Cadastro"}
          </p>

          <form className="space-y-4" onSubmit={handleSubmit(onSubmit)}>
            <Input
              type="email"
              placeholder="E-mail"
              {...register("email", { required: "E-mail é obrigatório" })}
            />
            {errors.email && (
              <p className="text-red-500 text-sm">{errors.email.message}</p>
            )}

            <div className="relative">
              <Input
                type={showPassword ? "text" : "password"}
                placeholder="Senha"
                {...register("password", { required: "Senha é obrigatória" })}
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

            <p className="text-xs text-black mt-7">
              Ao clicar em Aceitar e cadastrar-se, você aceita os{" "}
              <span className="font-bold underline">Termos de Uso</span> e as{" "}
              <span className="font-bold underline">Políticas de Privacidade</span>
            </p>

            <Button
              type="submit"
              className="bg-blue-600 hover:bg-blue-700 text-white w-full mt-4"
              disabled={isSubmitting}
            >
              {isSubmitting ? "Cadastrando..." : "Aceitar e cadastrar-se"}
            </Button>
          </form>
        </div>
      </main>
    </div>
  );
}
