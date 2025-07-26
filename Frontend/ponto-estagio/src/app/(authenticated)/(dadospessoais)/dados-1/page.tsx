"use client";
import React, { useEffect, useState } from 'react';
import { useRouter } from 'next/navigation';
import Sidebar from "@/app/(authenticated)/dashboard/Sidebar";
import DashboardLayout from "@/app/(authenticated)/dashboard/DashboardLayout";
import { ArrowRight } from 'lucide-react';
import Link from 'next/link';
// import { toast } from 'react-toastify'; // Importe sua biblioteca de toast, se estiver usando

// Importa as funções e tipos do seu novo serviço de API
import { authApi, companyApi, courseApi, UserRegisterDTO, UniversityDTO, CourseDTO, CoordinatorDTO, apiService } from '@/api/apiService';

export default function DadosPessoais() {
  const router = useRouter();

  const [nome, setNome] = useState("");
  const [matricula, setMatricula] = useState("");
  const [telefone, setTelefone] = useState("");
  const [email, setEmail] = useState("");
  
  const [universidadeId, setUniversidadeId] = useState(""); // Armazena o ID da universidade selecionada
  const [cursoId, setCursoId] = useState(""); // Armazena o ID do curso selecionado
  const [coordenadorId, setCoordenadorId] = useState(""); // Armazena o ID do coordenador selecionado

  const [universidades, setUniversidades] = useState<UniversityDTO[]>([]);
  const [cursos, setCursos] = useState<CourseDTO[]>([]);
  const [coordenadores, setCoordenadores] = useState<CoordinatorDTO[]>([]); // Lista de coordenadores para o select

  const [carregandoListas, setCarregandoListas] = useState(true); 
  const [isSaving, setIsSaving] = useState(false); // Estado para controlar o botão de salvar
  const [error, setError] = useState<string | null>(null); // Estado para exibir mensagens de erro

  // Efeito para carregar as listas de universidades, cursos e coordenadores ao montar o componente
  useEffect(() => {
    async function carregarListas() {
      try {
        const token = localStorage.getItem("token");
        if (!token) {
          // Se o token de autenticação for nulo, redirecione para a tela de login ou outra tela de autenticação 
          router.push("/login");
          return;
        }

        // 1. Buscar Universidades
        const uniData = await apiService.get<UniversityDTO[]>("/universities", token);
        setUniversidades(uniData);

        // 2. Buscar Cursos
        const courseData = await courseApi.getCourses(token);
        setCursos(courseData);

        // 3. Buscar Coordenadores
        // Se a API para coordenadores for diferente, ajuste aqui.
        try {
            const coordData = await apiService.get<CoordinatorDTO[]>("/users?type=Coordinator", token); // Usando o get genérico do apiService
            setCoordenadores(coordData);
        } catch (coordError) {
            console.warn("Não foi possível buscar coordenadores. Verifique o endpoint ou tipo:", coordError);
            setCoordenadores([]);
        }

      } catch (err: any) {
        console.error("Erro ao carregar listas de seleção:", err);
        setError(`Erro ao carregar opções: ${err.message}`);
      } finally {
        setCarregandoListas(false);
      }
    }

    carregarListas();
  }, []);

  // Função para lidar com o envio do formulário (POST para criar novo usuário)
  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setIsSaving(true);
    setError(null);

    try {
      const token = localStorage.getItem("token");
      if (!token) {
        throw new Error("Token de autenticação não encontrado.");
      }

      const userData: UserRegisterDTO = {
        name: nome,
        email: email,
        registration: matricula,
        phone: telefone,
        universityId: universidadeId,
        courseId: cursoId,
        // coordinatorId: coordenadorId, // Descomente se a API POST /users aceitar coordinatorId
        isActive: true,
        type: "Intern", // Defina o tipo de usuário ("Intern", "Coordinator", "Supervisor")
      };

      console.log("Enviando dados para cadastro (DadosPessoais):", userData);

      const newUser = await authApi.registerUser(userData);

      // toast.success('Usuário cadastrado com sucesso!');
      console.log('Usuário cadastrado com sucesso!', newUser);

      router.push(`/dados-2?userId=${newUser.id}`);

    } catch (err: any) {
      setError(err.message || "Ocorreu um erro ao cadastrar o usuário.");
      console.error('Erro ao cadastrar usuário:', err);
    } finally {
      setIsSaving(false);
    }
  };

  if (carregandoListas) {
    return (
      <DashboardLayout>
        <div className="flex">
          <Sidebar />
          <div className="flex-1 flex items-center justify-center min-h-screen bg-[#FAF9F6]">
            <p className="text-gray-600">Carregando opções de Universidades, Cursos e Coordenadores...</p>
          </div>
        </div>
      </DashboardLayout>
    );
  }

  return (
    <DashboardLayout>
      <div className="flex">
        <Sidebar />
        <div className="flex-1 bg-[#FAF9F6] min-h-screen p-6 pl-18">

          {/* Navegação entre etapas (indicador de progresso) */}
          <div className="pt-1 px-6 mx-auto mt-12 w-full">
            <div className="flex justify-center items-center mb-10 sm:w-[200px] md:w-[700px] lg:w-[1350px]">
              <div className="flex items-center ">
                <div className="w-10 h-10  rounded-full bg-blue-600 text-white flex items-center justify-center font-bold text-sm">1</div>
                <div className="h-1 bg-gray-300 sm:w-[50px] md:w-[120px] lg:w-[330px]"></div>
              </div>
              <div className="flex items-center">
                <div className="w-10 h-10 rounded-full bg-gray-300 text-white flex items-center justify-center font-bold text-sm">2</div>
                <div className="h-1 bg-gray-300 sm:w-[50px] md:w-[120px] lg:w-[330px]"></div>
              </div>
              <div className="flex items-center">
                <div className="w-10 h-10 rounded-full bg-gray-300 text-white flex items-center justify-center font-bold text-sm">3</div>
              </div>
            </div>
          </div>

          {/* Card principal do formulário */}
          <div className="bg-white sm:w-[200px] md:w-[700px] lg:w-[1350px] rounded-lg shadow p-6 mb-8 max-w-7xl">
            <h2 className="font-bold text-gray-800 mb-2 text-xl">DADOS PESSOAIS</h2>
            <p className="text-sm text-gray-600 mb-6">Informe seus dados pessoais para cadastro</p>

            {/* Exibição de erros */}
            {error && <div className="bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded relative mb-4" role="alert">
              <span className="block sm:inline">{error}</span>
            </div>}

            <form onSubmit={handleSubmit}>
              <div className="grid grid-cols-1 md:grid-cols-2 gap-4 items-end">
                <div>
                  <label htmlFor="nome" className="text-sm font-medium text-gray-600">Nome</label>
                  <input
                    id="nome"
                    type="text"
                    value={nome}
                    onChange={(e) => setNome(e.target.value)}
                    className="w-full border rounded p-2 mt-1 text-gray-600"
                    placeholder="Digite seu nome completo"
                    required
                  />
                </div>

                <div>
                  <label htmlFor="matricula" className="text-sm font-medium text-gray-800">Matrícula</label>
                  <input
                    id="matricula"
                    type="text"
                    value={matricula}
                    onChange={(e) => setMatricula(e.target.value)}
                    className="w-full border rounded p-2 mt-1 text-gray-600"
                    placeholder="Digite sua matrícula"
                    required
                  />
                </div>

                <div>
                  <label htmlFor="telefone" className="text-sm font-medium text-gray-600">Telefone</label>
                  <input
                    id="telefone"
                    type="tel"
                    value={telefone}
                    onChange={(e) => setTelefone(e.target.value)}
                    className="w-full border rounded p-2 mt-1 text-gray-600"
                    placeholder="Informe seu telefone"
                    required
                  />
                </div>

                <div>
                  <label htmlFor="email" className="text-sm font-medium text-gray-600">E-mail</label>
                  <input
                    id="email"
                    type="email"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                    className="w-full border rounded p-2 mt-1 text-gray-600"
                    placeholder="Digite seu e-mail"
                    required
                  />
                </div>

                <div className="md:col-span-1">
                  <label htmlFor="universidade" className="text-sm font-medium text-gray-600">Universidade</label>
                  <select
                    id="universidade"
                    value={universidadeId}
                    onChange={(e) => setUniversidadeId(e.target.value)}
                    className="w-full border rounded p-2 mt-1 text-gray-600"
                    required
                  >
                    <option value="">Selecione uma universidade</option>
                    {universidades.map((uni) => (
                      <option key={uni.id} value={uni.id}>{uni.name}</option>
                    ))}
                  </select>
                </div>
                
                <div>
                  <label htmlFor="curso" className="text-sm font-medium text-gray-600">Curso</label>
                  <select
                    id="curso"
                    value={cursoId}
                    onChange={(e) => setCursoId(e.target.value)}
                    className="w-full border rounded p-2 mt-1 text-gray-600"
                    required
                  >
                    <option value="">Selecione um curso</option>
                    {cursos.map((c) => (
                      <option key={c.id} value={c.id}>{c.name}</option>
                    ))}
                  </select>
                </div>

                <div>
                  <label htmlFor="coordenador" className="text-sm font-medium text-gray-600">Coordenador</label>
                  <select
                    id="coordenador"
                    value={coordenadorId}
                    onChange={(e) => setCoordenadorId(e.target.value)}
                    className="w-full border rounded p-2 mt-1 text-gray-600"
                    // Remova 'required' se o coordenador for opcional para o usuário
                    // Ou adicione se o POST /api/users aceitar 'coordinatorId' e for obrigatório
                  >
                    <option value="">Selecione um coordenador</option>
                    {coordenadores.map((coord) => (
                      <option key={coord.id} value={coord.id}>{coord.name}</option>
                    ))}
                  </select>
                </div>
              </div>

              {/* Botão de Cadastrar e Avançar */}
              <div className="flex justify-end mt-6">
                <button
                  type="submit"
                  disabled={isSaving}
                  className="bg-blue-500 hover:bg-blue-600 text-white py-2 px-4 rounded flex items-center space-x-2 disabled:opacity-50 disabled:cursor-not-allowed"
                >
                  {isSaving ? "Cadastrando..." : "Avançar"}
                  <ArrowRight size={24} />
                </button>
              </div>
            </form>
          </div>
        </div>
      </div>
    </DashboardLayout>
  );
}
