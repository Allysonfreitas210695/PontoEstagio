"use client";

import { useEffect, useState } from "react";
import SelectTypeUser from "./components/selectTypeUser";
import { userTypeDTO } from "@/types/user";
import { courceDTO } from "@/types/cource";
import { UniversityDTO } from "@/types/university";
import { getAllCources } from "@/api/cources_api";
import { toast } from "sonner";
import { getAllUniversities } from "@/api/universities_api";
import { useForm } from "react-hook-form";
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { Label } from "@/components/ui/label";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import { useRouter } from "next/navigation";
import { registerUser } from "@/api/users_api";
import Loading from "@/components/loading";

interface FormData {
  name: string;
  email: string;
  registration: string;
  password: string;
  phone: string;
  universityId: string;
  courseId?: string;
  verificationCode?: string;
}

export interface HandleSelectProps {
  type: userTypeDTO;
  email: string;
  password: string;
}

export default function RegisterPage() {
  const router = useRouter();

  const {
    register, 
    handleSubmit,
    setValue, 
    watch,
    formState: { errors },
  } = useForm<FormData>({
    defaultValues: {
      name: "",
      email: "",
      registration: "",
      password: "",
      phone: "",
      universityId: "",
      courseId: "",
      verificationCode: "",
    },
  });

  const [userType, setUserType] = useState<userTypeDTO | null>(null);
  const [availableCourses, setAvailableCourses] = useState<courceDTO[]>([]);
  const [listUniversities, setListUniversities] = useState<UniversityDTO[]>([]);
  const [isLoading, setIsLoading] = useState(false);

  function handleSelectUserType({ email, password, type }: HandleSelectProps) {
    setUserType(type);
    setValue("email", email);
    setValue("password", password);
  }

  async function loadGetAllCources() {
    setIsLoading(true)
    try {
      const cources = await getAllCources();
      setAvailableCourses(cources);
    } catch (error) {
      toast.error(error instanceof Error ? error.message : "Erro ao carregar cursos");
    }finally {
      setIsLoading(false)
    }
  }

  async function loadGetAllUniversities() {
    setIsLoading(true)
    try {
      const universities = await getAllUniversities();
      setListUniversities(universities);
    } catch (error) {
      toast.error(error instanceof Error ? error.message : "Erro ao carregar universidades");
    }finally {
      setIsLoading(false)
    }
  }

  const handleUniversityChange = (value: string) => {
    setValue("universityId", value, { shouldValidate: true });
  };

  const handleCourseChange = (value: string) => {
    setValue("courseId", value, { shouldValidate: true });
  };

  const universityId = watch("universityId");
  const courseId = watch("courseId");

  async function onSubmit(data: FormData) {
    setIsLoading(true);
    try {
      await registerUser({
        type: userType!,
        ...data,
        isActive: true,
      });
      toast.success("Sucesso na operação de cadastro do usuário");
      setTimeout(() => router.push("/login"), 500);
    } catch (error) {
      toast.error(error instanceof Error ? error.message : "Erro ao cadastrar usuário!");
    } finally {
      setIsLoading(false);
    }
  }

  useEffect(() => {
    loadGetAllCources();
    loadGetAllUniversities();
  }, []);

  if (isLoading) return <Loading />;

  return (
    <>
      {!userType && <SelectTypeUser onChangeUserType={handleSelectUserType} />}

      {userType && (
        <div className="bg-white min-h-screen flex flex-col items-center relative px-4">
          <main className="flex flex-col items-center justify-center flex-1 w-full max-w-md px-4 py-8">
            <Card className="w-full">
              <CardHeader className="text-center">
                <CardTitle className="text-2xl text-gray-900">Cadastre-se</CardTitle>
                <CardDescription className="text-sm text-gray-600 mt-2 text-center">
                  Preencha os campos abaixo para criar sua conta
                </CardDescription>
              </CardHeader>
              <CardContent className="space-y-4">
                <form className="space-y-4" onSubmit={handleSubmit(onSubmit)}>
                  
                  {/* UNIVERSIDADE */}
                  <div className="space-y-2 w-full">
                    <Label htmlFor="universityId">Universidade</Label>
                    <Select
                      onValueChange={handleUniversityChange}
                      value={universityId}
                    >
                      <SelectTrigger className="w-full">
                        <SelectValue placeholder="Selecione sua universidade" />
                      </SelectTrigger>
                      <SelectContent>
                        {listUniversities.map((u) => (
                          <SelectItem key={u.id} value={u.id}>
                            {u.name}
                          </SelectItem>
                        ))}
                      </SelectContent>
                    </Select>
                    {errors.universityId && (
                      <p className="text-sm text-red-500">Selecione uma universidade.</p>
                    )}
                  </div>

                  {/* CURSO */}
                  {userType === "Coordinator" && (
                    <div className="space-y-2 w-full">
                      <Label htmlFor="courseId">Curso</Label>
                      <Select
                        onValueChange={handleCourseChange}
                        value={courseId}
                      >
                        <SelectTrigger className="w-full">
                          <SelectValue placeholder="Selecione seu curso" />
                        </SelectTrigger>
                        <SelectContent>
                          {availableCourses.map((c) => (
                            <SelectItem key={c.id} value={c.id}>
                              {c.name}
                            </SelectItem>
                          ))}
                        </SelectContent>
                      </Select>
                      {errors.courseId && (
                        <p className="text-sm text-red-500">Selecione um curso.</p>
                      )}
                    </div>
                  )}

                  {/* NOME */}
                  <div className="space-y-2">
                    <Label htmlFor="name">{userType === "Intern" ? "Nome" : "Nome do Coordenador"}</Label>
                    <Input
                      {...register("name", { required: "Nome é obrigatório." })}
                    />
                    {errors.name && <p className="text-sm text-red-500">{errors.name.message}</p>}
                  </div>

                  {/* MATRÍCULA */}
                  {userType === "Intern" && 
                    <div className="space-y-2">
                      <Label htmlFor="registration">Matrícula</Label>
                      <Input
                        {...register("registration", { required: "Matrícula é obrigatória." })}
                      />
                      {errors.registration && <p className="text-sm text-red-500">{errors.registration.message}</p>}
                    </div>
                    }

                  {/* TELEFONE */}
                  {userType === "Intern" && 
                    <div className="space-y-2">
                      <Label htmlFor="phone">Telefone</Label>
                      <Input
                        {...register("phone", { 
                          required: "Telefone é obrigatório.",
                          pattern: { value: /^[0-9]{10,11}$/, message: "Digite um telefone válido (10 ou 11 dígitos)." }
                        })}
                      />
                      {errors.phone && <p className="text-sm text-red-500">{errors.phone.message}</p>}
                    </div>
                  }

                  {/* VERIFICAÇÃO - COORDENADOR */}
                  {userType === "Coordinator" && (
                    <div className="space-y-2">
                      <Label htmlFor="verificationCode">Código de Verificação</Label>
                      <div className="flex justify-between gap-2">
                        {[...Array(6)].map((_, index) => (
                          <Input
                            key={index}
                            maxLength={1}
                            className="w-10 text-center"
                            onChange={(e) => {
                              const value = e.target.value.replace(/\D/g, "");
                              e.target.value = value;

                              const currentCode = watch("verificationCode") || "";
                              const codeArray = currentCode.padEnd(6, " ").split("");
                              codeArray[index] = value;
                              setValue("verificationCode", codeArray.join("").trim(), { shouldValidate: true });

                              if (value && e.target.nextElementSibling) {
                                (e.target.nextElementSibling as HTMLInputElement).focus();
                              }
                            }}
                            onKeyDown={(e) => {
                              if (e.key === "Backspace" && !e.currentTarget.value && e.currentTarget.previousElementSibling) {
                                (e.currentTarget.previousElementSibling as HTMLInputElement).focus();
                              }
                            }}
                          />
                        ))}
                      </div>
                      {errors.verificationCode && (
                        <p className="text-sm text-red-500">Digite o código completo de 6 dígitos.</p>
                      )}
                    </div>
                  )}

                  <Button type="submit" className="w-full bg-blue-600 hover:bg-blue-700 text-white">
                    Finalizar Cadastro
                  </Button>
                </form>
              </CardContent>
            </Card>
          </main>
        </div>
      )}
    </>
  );
}
