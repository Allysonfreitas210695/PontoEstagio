"use client";
import React, { useEffect, useState } from 'react';
import { useRouter, useSearchParams } from 'next/navigation';
import Sidebar from "@/app/(authenticated)/dashboard/Sidebar";
import DashboardLayout from "@/app/(authenticated)/dashboard/DashboardLayout";
import { ArrowRight, ArrowLeft } from 'lucide-react';
import Link from 'next/link';

// import { toast } from 'react-toastify'; // Importe sua biblioteca de toast, se estiver usando

// Importa as funções e tipos do seu novo serviço de API
import { projectApi, ProjectRegisterDTO, CoordinatorDTO, apiService } from '@/api/apiService';

export default function DadosEstagio() {
  const router = useRouter();
  const searchParams = useSearchParams();

  // IDs passados das páginas anteriores via URL
  const userId = searchParams.get('userId');
  const companyId = searchParams.get('companyId');
  const supervisorId = searchParams.get('supervisorId'); // legalRepresentativeId

  // Estados para os dados do estágio
  const [tipoEstagio, setTipoEstagio] = useState("");
  const [cargaHorariaSemanal, setCargaHorariaSemanal] = useState("");
  const [valorBolsa, setValorBolsa] = useState("");
  const [professorOrientadorId, setProfessorOrientadorId] = useState(""); // ID do professor/coordenador
  const [dataInicio, setDataInicio] = useState("");
  const [dataFim, setDataFim] = useState("");
  const [descricaoAtividades, setDescricaoAtividades] = useState("");

  const [professoresOrientadores, setProfessoresOrientadores] = useState<CoordinatorDTO[]>([]); // Lista de professores/coordenadores
  const [carregandoListas, setCarregandoListas] = useState(true);
  const [isSaving, setIsSaving] = useState(false);
  const [error, setError] = useState<string | null>(null);

  // Efeito para verificar IDs e carregar lista de professores/coordenadores
  useEffect(() => {
    async function carregarDadosIniciais() {
      if (!userId || !companyId || !supervisorId) {
        console.error("IDs necessários ausentes. Redirecionando para o início do cadastro.");
        // toast.error("Informações anteriores ausentes. Por favor, reinicie o cadastro.");
       // router.push('/dados-1'); // Redireciona para a primeira etapa
        return;
      }

      try {
        const token = localStorage.getItem("token");
        if (!token) {
          router.push("/login");
          return;
        }

        // Buscar Professores Orientadores (assumindo que são usuários do tipo 'Coordinator' ou 'Professor')
    
        const coordResponse = await apiService.get<CoordinatorDTO[]>("/users?type=Coordinator", token); // Exemplo de endpoint
        setProfessoresOrientadores(coordResponse);

      } catch (err: any) {
        console.error("Erro ao carregar lista de professores orientadores:", err);
        setError(`Erro ao carregar opções de professores: ${err.message}`);
      } finally {
        setCarregandoListas(false);
      }
    }

    carregarDadosIniciais();
  }, [userId, companyId, supervisorId, router]); // Dependências para re-executar se IDs mudarem

  // Função para lidar com o cancelamento do formulário
  const handleCancel = () => {
    router.push('/dashboard'); // Redireciona para o dashboard ou outra página desejada
  };

  // Função para lidar com o envio do formulário (POST /api/project)
  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setIsSaving(true);
    setError(null);

    if (!userId || !companyId || !supervisorId) {
      setError("Informações essenciais ausentes para cadastrar o estágio.");
      setIsSaving(false);
      return;
    }

    try {
      const token = localStorage.getItem("token");
      if (!token) {
        throw new Error("Token de autenticação não encontrado.");
      }

      // Mapear os dados do formulário para o payload esperado pela API POST /api/project
      const projectData: ProjectRegisterDTO = {
        name: tipoEstagio, // Usando "Tipo do Estágio" como nome do projeto
        description: descricaoAtividades,
        status: "Pending", // Defina um status inicial padrão para o projeto
        startDate: dataInicio,   
        endDate: dataFim,     
        companyId: companyId,
        userId: userId,
        legalRepresentativeId: supervisorId, // ID do supervisor
        // Campos adicionais que podem ou não ser aceitos pela API /api/project
        weeklyHours: parseInt(cargaHorariaSemanal) || 0, // Converte para número
        scholarshipValue: parseFloat(valorBolsa.replace(",", ".")) || 0, // Converte para número (lidando com vírgula)
        advisorUserId: professorOrientadorId, // ID do professor orientador
      };

      console.log("Payload de cadastro do estágio (projeto):", projectData);

      const newProject = await projectApi.createProject(projectData, token);
      console.log("Estágio (projeto) cadastrado com sucesso:", newProject);
      // toast.success("Dados do estágio cadastrados com sucesso!");

      // Redirecionar para uma página de sucesso ou dashboard
      router.push('/dashboard'); // Exemplo: redireciona para o dashboard após o cadastro completo

    } catch (err: any) {
      setError(err.message || "Ocorreu um erro ao cadastrar o estágio.");
      console.error("Erro ao cadastrar estágio:", err);
    } finally {
      setIsSaving(false);
    }
  };

//   if (carregandoListas) {
//     return (
//       <DashboardLayout>
//         <div className="flex">
//           <Sidebar />
//           <div className="flex-1 flex items-center justify-center min-h-screen bg-[#FAF9F6]">
//             <p className="text-gray-600">Carregando dados iniciais e opções...</p>
//           </div>
//         </div>
//       </DashboardLayout>
//     );
//  }

  return (
    <DashboardLayout>
      <div className="flex">
        <Sidebar />
        <div className="flex-1 bg-[#FAF9F6] min-h-screen p-6 pl-20">

          {/* Navegação entre etapas (indicador de progresso) */}
          <div className="pt-1 px-6 mx-auto mt-12 w-full">
            <div className="flex justify-center items-center mb-10 sm:w-[200px] md:w-[700px] lg:w-[1350px]">
              <div className="flex items-center">
                <div className="w-10 h-10 rounded-full bg-blue-600 text-white flex items-center justify-center font-bold text-sm">1</div>
                <div className="h-1 bg-blue-600 sm:w-[50px] md:w-[120px] lg:w-[330px]"></div>
              </div>
              <div className="flex items-center">
                <div className="w-10 h-10 rounded-full bg-blue-600 text-white flex items-center justify-center font-bold text-sm">2</div>
                <div className="h-1 bg-blue-600 sm:w-[50px] md:w-[120px] lg:w-[330px]"></div>
              </div>
              <div className="flex items-center">
                <div className="w-10 h-10 rounded-full bg-blue-600 text-white flex items-center justify-center font-bold text-sm">3</div> {/* Etapa 3 ativa */}
              </div>
            </div>
          </div>

          {/* Card principal do formulário */}
          <div className="bg-white rounded-lg shadow p-6 mb-8 max-w-7xl">
            <h2 className="font-bold text-gray-800 mb-2 text-xl">DADOS DO ESTÁGIO</h2>
            <p className="text-sm text-gray-600 mb-6">Informe os dados referentes ao seu estágio.</p>

            {/* Exibição de erros */}
            {error && <div className="bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded relative mb-4" role="alert">
              <span className="block sm:inline">{error}</span>
            </div>}

            <form onSubmit={handleSubmit}>
              <div className="grid grid-cols-1 md:grid-cols-3 gap-4 items-end">
                {/* Tipo do Estágio */}
                <div>
                  <label htmlFor="tipoEstagio" className="text-sm font-medium text-gray-600">Tipo do Estágio *</label>
                  <input
                    id="tipoEstagio"
                    type="text" // Pode ser um select se houver tipos pré-definidos
                    value={tipoEstagio}
                    onChange={(e) => setTipoEstagio(e.target.value)}
                    className="w-full border rounded p-2 mt-1 text-gray-600"
                    placeholder="Ex: Estágio Curricular Obrigatório"
                    required
                  />
                </div>

                {/* Carga Horária Semanal */}
                <div>
                  <label htmlFor="cargaHorariaSemanal" className="text-sm font-medium text-gray-600">Carga Horária Semanal</label>
                  <div className="flex items-center mt-1">
                    <input
                      id="cargaHorariaSemanal"
                      type="number"
                      value={cargaHorariaSemanal}
                      onChange={(e) => setCargaHorariaSemanal(e.target.value)}
                      className="w-full border rounded p-2 text-gray-600"
                      placeholder="00"
                      min="0"
                    />
                    <span className="ml-2 text-gray-600">Horas</span>
                  </div>
                </div>

                {/* Valor da Bolsa */}
                <div>
                  <label htmlFor="valorBolsa" className="text-sm font-medium text-gray-600">Valor da Bolsa</label>
                  <div className="flex items-center mt-1">
                    <span className="mr-2 text-gray-600">R$</span>
                    <input
                      id="valorBolsa"
                      type="text" // Usar text para permitir formatação de moeda
                      value={valorBolsa}
                      onChange={(e) => setValorBolsa(e.target.value)}
                      className="w-full border rounded p-2 text-gray-600"
                      placeholder="1.000,00"
                    />
                  </div>
                </div>

                {/* Professor Orientador */}
                <div>
                  <label htmlFor="professorOrientador" className="text-sm font-medium text-gray-600">Professor Orientador *</label>
                  <select
                    id="professorOrientador"
                    value={professorOrientadorId}
                    onChange={(e) => setProfessorOrientadorId(e.target.value)}
                    className="w-full border rounded p-2 mt-1 text-gray-600"
                    required
                  >
                    <option value="">Selecione um Professor</option>
                    {professoresOrientadores.map((prof) => (
                      <option key={prof.id} value={prof.id}>{prof.name}</option>
                    ))}
                  </select>
                </div>

                {/* Data de Início */}
                <div>
                  <label htmlFor="dataInicio" className="text-sm font-medium text-gray-600">Data de Início *</label>
                  <input
                    id="dataInicio"
                    type="date" // Tipo date para seletor de data
                    value={dataInicio}
                    onChange={(e) => setDataInicio(e.target.value)}
                    className="w-full border rounded p-2 mt-1 text-gray-600"
                    required
                  />
                </div>

                {/* Data de Fim */}
                <div>
                  <label htmlFor="dataFim" className="text-sm font-medium text-gray-600">Data de Fim *</label>
                  <input
                    id="dataFim"
                    type="date" // Tipo date para seletor de data
                    value={dataFim}
                    onChange={(e) => setDataFim(e.target.value)}
                    className="w-full border rounded p-2 mt-1 text-gray-600"
                    required
                  />
                </div>

                {/* Descrição das Atividades */}
                <div className="md:col-span-3"> {/* Ocupa todas as 3 colunas */}
                  <label htmlFor="descricaoAtividades" className="text-sm font-medium text-gray-600">Descrição das Atividades *</label>
                  <textarea
                    id="descricaoAtividades"
                    value={descricaoAtividades}
                    onChange={(e) => setDescricaoAtividades(e.target.value)}
                    rows={4}
                    className="w-full border rounded p-2 mt-1 text-gray-600 resize-y"
                    placeholder="Breve descrição das atividades exercidas no estágio"
                    required
                  ></textarea>
                </div>
              </div>

                {/* Action Buttons */}
                <div className="flex justify-end space-x-4">
                  <button
                    type="button"
                    onClick={handleCancel}
                    className="border-2 border-blue-600 text-blue-600 hover:bg-blue-50 font-bold py-2 px-4 rounded-md flex items-center space-x-2 transition"
                  >
                    Cancelar
                  </button>
                  <button
                    type="submit"
                    disabled={isSaving}
                    className="bg-blue-600 hover:bg-blue-700 text-white font-bold py-2 px-6 rounded-md transition duration-150 ease-in-out disabled:opacity-50 disabled:cursor-not-allowed"
                  >
                    {isSaving ? "Cadastrando..." : "Cadastrar"}
                  </button>
                </div>
            </form>
          </div>
        </div>
      </div>
    </DashboardLayout>
  );
}
