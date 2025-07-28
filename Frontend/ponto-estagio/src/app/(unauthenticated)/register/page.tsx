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
    formState: { errors },
    handleSubmit,
    setValue,
    getValues,
    watch,
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

  function handleSelectUserType({ email, password, type }: HandleSelectProps) {
    setUserType(type);
    setValue("email", email);
    setValue("password", password);
  }

  async function loadGetAllCources() {
    try {
      const cources = await getAllCources();
      setAvailableCourses(cources);
    } catch (error) {
      if (error instanceof Error) return toast.error(error.message);
      toast.error("Erro ao carregar cursos");
    }
  }

  async function loadGetAllUniversities() {
    try {
      const universities = await getAllUniversities();
      setListUniversities(universities);
    } catch (error) {
      if (error instanceof Error) return toast.error(error.message);
      toast.error("Erro ao carregar universidades");
    }
  }

  const handleUniversityChange = (value: string) => {
    setValue("universityId", value);
  };

  const handleCourseChange = (value: string) => {
    setValue("courseId", value);
  };

  const universityId = watch("universityId");
  const courseId = watch("courseId");

  async function onSumit(data: FormData) {
    const {
      email,
      name,
      password,
      phone,
      registration,
      universityId,
      courseId,
      verificationCode,
    } = data;
    console.log("Submitting data:", data);

    
    try {
      await registerUser({
        type: userType!,
        email,
        name,
        password,
        phone,
        registration,
        universityId,
        isActive: true,
        courseId,
        verificationCode,
      });

      toast.success("Sucesso na operação de cadastro do usuário");
    } catch (error) {
      if (error instanceof Error) return toast.error(error.message);
      toast.error("Erro ao cadastrar usuário!");

      router.push("/login");
    } finally {
      // ver loding
    }
  }

  useEffect(() => {
    loadGetAllCources();
    loadGetAllUniversities();
  }, []);

  return (
    <>
      {!userType && <SelectTypeUser onChangeUserType={handleSelectUserType} />}

      {userType && (
        <div className="bg-white min-h-screen flex flex-col items-center relative px-4">
          <main className="flex flex-col items-center justify-center flex-1 w-full max-w-md px-4 py-8">
            <Card className="w-full">
              <CardHeader className="text-center">
                <CardTitle className="text-2xl text-gray-900">
                  Cadastre-se
                </CardTitle>
                <CardDescription className="text-sm text-gray-600 mt-2 text-center">
                  Preencha os campos abaixo para criar sua conta
                </CardDescription>
              </CardHeader>
              <CardContent className="space-y-4">
                <form className="space-y-4" onSubmit={handleSubmit(onSumit)}>
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
                  </div>

                  {userType === "Intern" && (
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
                    </div>
                  )}

                  <div className="space-y-2">
                    <Label htmlFor="name">Nome</Label>
                    <Input {...register("name", { required: true })} />
                  </div>

                  <div className="space-y-2">
                    <Label htmlFor="registration">Matrícula</Label>
                    <Input {...register("registration", { required: true })} />
                  </div>

                  <div className="space-y-2">
                    <Label htmlFor="phone">Telefone</Label>
                    <Input {...register("phone", { required: true })} />
                  </div>

                  {userType === "Coordinator" && (
                    <div className="space-y-2">
                      <Label htmlFor="verificationCode">
                        Código de Verificação
                      </Label>
                      <Input
                        {...register("verificationCode", { required: true })}
                      />
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
