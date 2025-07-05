"use client";
import { RegisterAluno, type AlunoData } from "@/api/api";
import { useState } from "react";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import Header from "@/app/components/header/page";
import Footer from "@/app/components/footer/page";
import { useRouter } from "next/navigation";

type Course = { id: string; name: string };
type University = { id: string; name: string; courses: Course[] };

const universitiesData: University[] = [
  {
    id: "2e22c3e1-4c7b-4d5a-8b9a-1c2d3e4f5a6b",
    name: "Universidade Federal Rural do Semi-Árido (UFERSA)",
    courses: [
      { id: "1a22c3e1-4c7b-4d5a-8b9a-1c2d3e4f5a6b", name: "Engenharia de Software" },
      { id: "2a22c3e1-4c7b-4d5a-8b9a-1c2d3e4f5a6b", name: "Engenharia de Computação" },
      { id: "3a22c3e1-4c7b-4d5a-8b9a-1c2d3e4f5a6b", name: "Engenharia Civil" },
      { id: "4a22c3e1-4c7b-4d5a-8b9a-1c2d3e4f5a6b", name: "Medicina Veterinária" },
      { id: "5a22c3e1-4c7b-4d5a-8b9a-1c2d3e4f5a6b", name: "Agronomia" },
    ],
  },
  {
    id: "3f4g5h6i-7j8k-9l0m-1n2o-3p4q5r6s7t8u",
    name: "Universidade Estadual de Campinas (UNICAMP)",
    courses: [
      { id: "1b4g5h6i-7j8k-9l0m-1n2o-3p4q5r6s7t8u", name: "Engenharia da Computação" },
      { id: "2b4g5h6i-7j8k-9l0m-1n2o-3p4q5r6s7t8u", name: "Medicina" },
      { id: "3b4g5h6i-7j8k-9l0m-1n2o-3p4q5r6s7t8u", name: "Física" },
      { id: "4b4g5h6i-7j8k-9l0m-1n2o-3p4q5r6s7t8u", name: "Letras" },
    ],
  },
  {
    id: "4a5b6c7d-8e9f-0a1b-2c3d-4e5f6a7b8c9d",
    name: "Universidade Federal do Rio de Janeiro (UFRJ)",
    courses: [
      { id: "1c5b6c7d-8e9f-0a1b-2c3d-4e5f6a7b8c9d", name: "Direito" },
      { id: "2c5b6c7d-8e9f-0a1b-2c3d-4e5f6a7b8c9d", name: "Jornalismo" },
      { id: "3c5b6c7d-8e9f-0a1b-2c3d-4e5f6a7b8c9d", name: "Química Industrial" },
      { id: "4c5b6c7d-8e9f-0a1b-2c3d-4e5f6a7b8c9d", name: "Artes Cênicas" },
    ],
  },
  {
    id: "5d6e7f8a-9b0c-1d2e-3f4a-5b6c7d8e9f0a",
    name: "Universidade Federal de Minas Gerais (UFMG)",
    courses: [
      { id: "1d6e7f8a-9b0c-1d2e-3f4a-5b6c7d8e9f0a", name: "Arquitetura e Urbanismo" },
      { id: "2d6e7f8a-9b0c-1d2e-3f4a-5b6c7d8e9f0a", name: "Engenharia Aeroespacial" },
      { id: "3d6e7f8a-9b0c-1d2e-3f4a-5b6c7d8e9f0a", name: "Odontologia" },
      { id: "4d6e7f8a-9b0c-1d2e-3f4a-5b6c7d8e9f0a", name: "Farmácia" },
    ],
  },
  {
    id: "6g7h8i9j-0k1l-2m3n-4o5p-6q7r8s9t0u1v",
    name: "Pontifícia Universidade Católica (PUC)",
    courses: [
      { id: "1e7h8i9j-0k1l-2m3n-4o5p-6q7r8s9t0u1v", name: "Psicologia" },
      { id: "2e7h8i9j-0k1l-2m3n-4o5p-6q7r8s9t0u1v", name: "Administração" },
      { id: "3e7h8i9j-0k1l-2m3n-4o5p-6q7r8s9t0u1v", name: "Design Gráfico" },
      { id: "4e7h8i9j-0k1l-2m3n-4o5p-6q7r8s9t0u1v", name: "Relações Internacionais" },
    ],
  },
];

export default function RegisterSignup() {
  const router = useRouter();
  const [formData, setFormData] = useState({
    name: "",
    email: "",
    registration: "",
    password: "",
    phone: "",
    universityId: "",
    courseId: "",
  });
  const [error, setError] = useState("");
  const [success, setSuccess] = useState("");
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [availableCourses, setAvailableCourses] = useState<Course[]>([]);
  const [code, setCode] = useState(Array(6).fill(""));
  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { id, value } = e.target;
    setFormData(prev => ({
      ...prev,
      [id]: value
    }));
  };
  const handleCodeChange = (value: string, index: number) => {
    const updatedCode = [...code];
    updatedCode[index] = value.slice(-1); // apenas 1 caractere
    setCode(updatedCode);
  };

  const handleUniversityChange = (value: string) => {
    setFormData(prev => ({
      ...prev,
      universityId: value,
      courseId: ""
    }));

    const selectedUniversity = universitiesData.find(uni => uni.id === value);
    setAvailableCourses(selectedUniversity?.courses || []);
  };

  const handleCourseChange = (value: string) => {
    setFormData(prev => ({
      ...prev,
      courseId: value
    }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError("");
    setSuccess("");
    setIsSubmitting(true);

    // Validação dos campos obrigatórios
    if (!formData.name || !formData.email || !formData.registration || 
        !formData.password || !formData.phone || !formData.universityId || !formData.courseId) {
      setError("Por favor, preencha todos os campos obrigatórios.");
      setIsSubmitting(false);
      return;
    }

    // Validação básica de email
    if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(formData.email)) {
      setError("Por favor, insira um email válido.");
      setIsSubmitting(false);
      return;
    }

    try {
      const alunoData: AlunoData = {
        ...formData,
        isActive: true,
        type: "Intern"
      };

      await RegisterAluno.register(alunoData);

      setSuccess("Cadastro realizado com sucesso! Redirecionando...");
      
      // Limpar formulário
      setFormData({
        name: "",
        email: "",
        registration: "",
        password: "",
        phone: "",
        universityId: "",
        courseId: "",
      });
      setAvailableCourses([]);

      // Redirecionar após 2 segundos
      setTimeout(() => {
        router.push("/login");
      }, 2000);
    } catch (err: any) {
      setError(err.message || "Erro ao realizar cadastro. Tente novamente.");
    } finally {
      setIsSubmitting(false);
    }
  };

  return (
    <div className="bg-white min-h-screen flex flex-col items-center relative px-4">
      <Header />

      <main className="flex flex-col items-center justify-center flex-1 w-full max-w-md px-4 py-8">
        <Card className="w-full">
          <CardHeader className="text-center">
            <CardTitle className="text-2xl text-gray-900">
              Cadastre-se
            </CardTitle>
            <CardDescription className="text-sm text-gray-600 mt-2 text-center">
              Preencha os campos abaixo para criar sua conta.
            </CardDescription>
          </CardHeader>
          <CardContent className="space-y-4">
            <form className="space-y-4" onSubmit={handleSubmit}>
              {/* Universidade Dropdown */}
              <div className="space-y-2">
                <Label htmlFor="university" className="text-sm font-medium text-gray-700">
                  Universidade
                </Label>
                <Select 
                  onValueChange={handleUniversityChange} 
                  value={formData.universityId}
                >
                  <SelectTrigger className="w-full border border-gray-300 rounded-md bg-white px-3 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent">
                    <SelectValue placeholder="Selecione sua universidade" />
                  </SelectTrigger>
                  <SelectContent className="max-h-60 overflow-y-auto border border-gray-300 rounded-md mt-1 bg-white z-50">
                    {universitiesData.map((uni) => (
                      <SelectItem key={uni.id} value={uni.id}>
                        {uni.name}
                      </SelectItem>
                    ))}
                  </SelectContent>
                </Select>
              </div>

              {/* Curso Dropdown */}
              <div className="space-y-2">
                <Label htmlFor="course" className="text-sm font-medium text-gray-700">
                  Curso
                </Label>
                <Select
                  onValueChange={handleCourseChange}
                  value={formData.courseId}
                  disabled={!formData.universityId || availableCourses.length === 0}
                >
                  <SelectTrigger className="w-full border border-gray-300 rounded-md bg-white px-3 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent">
                    <SelectValue placeholder={
                      formData.universityId 
                        ? "Selecione seu curso" 
                        : "Selecione uma universidade primeiro"
                    } />
                  </SelectTrigger>
                  <SelectContent className="max-h-60 overflow-y-auto border border-gray-300 rounded-md mt-1 bg-white z-50">
                    {availableCourses.map((course) => (
                      <SelectItem key={course.id} value={course.id}>
                        {course.name}
                      </SelectItem>
                    ))}
                  </SelectContent>
                </Select>
              </div>

              {/* Nome */}
              <div className="space-y-2">
                <Label htmlFor="name" className="text-sm font-medium text-gray-700">
                  Nome do coordenador
                </Label>
                <Input
                  id="name"
                  type="text"
                  placeholder="Digite seu nome completo"
                  value={formData.name}
                  onChange={handleChange}
                  className="w-full border border-gray-300 rounded-md px-3 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent"
                  required
                />
              </div>
          {/* Código de Verificação */}
          <div className="mb-2">
            <label className="text-sm font-medium text-gray-700 mb-2 block">
              Código de Verificação <span className="text-red-500">*</span>
            </label>
            <div className="flex gap-2 justify-between">
              {code.map((digit, idx) => (
                <input
                  key={idx}
                  maxLength={1}
                  className="w-10 h-10 text-center border border-gray-300 rounded-md text-lg"
                  value={digit}
                  onChange={(e) => handleCodeChange(e.target.value, idx)}
                />
              ))}
            </div>
          </div>
              

              {/* Mensagens de feedback */}
              {error && (
                <div className="p-3 bg-red-50 text-red-600 rounded-md text-sm">
                  {error}
                </div>
              )}
              {success && (
                <div className="p-3 bg-green-50 text-green-600 rounded-md text-sm">
                  {success}
                </div>
              )}

              {/* Botão de submit */}
              <Button
                type="submit"
                className="w-full bg-blue-600 hover:bg-blue-700 text-white font-medium py-2 px-4 rounded-md mt-6"
                disabled={isSubmitting}
              >
                {isSubmitting ? (
                  <span className="flex items-center justify-center">
                    <svg className="animate-spin -ml-1 mr-2 h-4 w-4 text-white" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24">
                      <circle className="opacity-25" cx="12" cy="12" r="10" stroke="currentColor" strokeWidth="4"></circle>
                      <path className="opacity-75" fill="currentColor" d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z"></path>
                    </svg>
                    Processando...
                  </span>
                ) : "Finalizar Cadastro"}
              </Button>
            </form>
          </CardContent>
        </Card>
      </main>

      <Footer />
    </div>
  );
}