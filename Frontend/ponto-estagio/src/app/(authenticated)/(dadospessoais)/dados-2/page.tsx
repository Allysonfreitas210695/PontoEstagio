"use client";
import React, { useEffect, useState } from 'react';
import Image from 'next/image';
import { Plus, ChevronLeft, ArrowLeft } from 'lucide-react';
import Sidebar from "@/app/(authenticated)/dashboard/Sidebar";
import DashboardLayout from "@/app/(authenticated)/dashboard/DashboardLayout";
import Link from 'next/link';
import { ArrowRight } from 'lucide-react';
// import { toast } from 'react-toastify'; // Importe sua biblioteca de toast, se estiver usando

// Importa a função de API e os tipos do seu novo serviço de API
import { authApi, UserLoggedUserDTO } from '@/api/apiService';
import router from 'next/router';
import { toast } from 'sonner';

export default function DadosEmpresaSupervisor() {
  // State for Empresa Data
  const [cnpjCpfEmpresa, setCnpjCpfEmpresa] = useState("");
  const [nomeEmpresa, setNomeEmpresa] = useState("");

  // State for Supervisor Data
  const [cpfSupervisor, setCpfSupervisor] = useState("");
  const [nomeSupervisor, setNomeSupervisor] = useState("");

  const [carregando, setCarregando] = useState(true);
  const [error, setError] = useState<string | null>(null); // Estado para exibir mensagens de erro

  // Efeito para carregar os dados da empresa e supervisor ao montar o componente
  useEffect(() => {
    async function carregarDados() {
      try {
        setCarregando(true);
        setError(null);

        const token = localStorage.getItem("token");
        if (!token) {
          toast.error("Token de autenticação não encontrado. Faça login novamente.");
          router.push('/login'); // Redirecionar para login, se necessário
          setError("Token de autenticação ausente. Por favor, faça login novamente.");
          setCarregando(false);
          return;
        }

        // Usando o authApi.getUserMe do seu serviço de API
        const dadosUsuario: UserLoggedUserDTO = await authApi.getUserMe(token);
        console.log("Dados do usuário recebidos para Empresa/Supervisor (GET /users/me):", dadosUsuario);

        // --- Mapeamento dos Dados da Empresa ---
        // Verifique se 'dadosUsuario.company' existe e possui as propriedades
        if (dadosUsuario.company) {
          setCnpjCpfEmpresa(dadosUsuario.company.cnpj || dadosUsuario.company.cpf || "");
          setNomeEmpresa(dadosUsuario.company.name || "");
        } else {
            console.log("Nenhum dado de empresa encontrado para o usuário.");
            // Opcional: Definir valores padrão ou mostrar que não há empresa associada
            setCnpjCpfEmpresa("");
            setNomeEmpresa("");
        }

        // --- Mapeamento dos Dados do Supervisor ---
        if (dadosUsuario.supervisor) { // Assumindo que a propriedade é 'supervisor'
          setCpfSupervisor(dadosUsuario.supervisor.cpf || "");
          setNomeSupervisor(dadosUsuario.supervisor.fullName || ""); // Use fullName para o nome do supervisor
        } else {
            console.log("Nenhum dado de supervisor encontrado para o usuário.");
            setCpfSupervisor("");
            setNomeSupervisor("");
        }

      } catch (err: any) {
        console.error("Erro ao carregar dados da empresa/supervisor:", err);
        setError(`Erro ao carregar dados: ${err.message}`);
        // toast.error(`Erro ao carregar dados: ${err.message}`);
      } finally {
        setCarregando(false);
      }
    }

    carregarDados();
  }, []); // O array vazio garante que o efeito roda apenas uma vez (ao montar)


  if (carregando) {
    return (
      <DashboardLayout>
        <div className="flex">
          <Sidebar />
          <div className="flex-1 flex items-center justify-center min-h-screen bg-[#FAF9F6]">
            <p className="text-gray-600">Carregando dados da empresa/supervisor...</p>
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

          {/* Header Bar */}
          <div className="pt-1 px-0 mx-auto mt-12 w-ful">
            {/* Progress Indicator */}
            <div className="flex justify-center items-center mb-10 sm:w-[200px] md:w-[700px] lg:w-[1350px]">
              {/* Etapa 1 */}
              <div className="flex items-center">
                <div className="w-10 h-10 rounded-full bg-blue-600 text-white flex items-center justify-center font-bold text-sm">1</div>
                <div className="h-1 bg-blue-600 sm:w-[50px] md:w-[120px] lg:w-[330px]"></div>
              </div>

              {/* Etapa 2 */}
              <div className="flex items-center">
                <div className="w-10 h-10 rounded-full bg-blue-600 text-white flex items-center justify-center font-bold text-sm">2</div>
                <div className="h-1 bg-gray-300 sm:w-[50px] md:w-[120px] lg:w-[330px]"></div>
              </div>

              {/* Etapa 3 */}
              <div className="flex items-center">
                <div className="w-10 h-10 rounded-full bg-gray-300 text-white flex items-center justify-center font-bold text-sm">3</div>
              </div>
            </div>

            {/* Exibição de erros */}
            {error && <div className="bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded relative mb-4" role="alert">
              <span className="block sm:inline">{error}</span>
            </div>}

            {/* DADOS DA EMPRESA Card */}
            <div className="bg-white rounded-lg shadow p-6 mb-8 max-w-7xl">
              <h2 className="font-bold text-gray-800 mb-2 text-xl">DADOS DA EMPRESA</h2>
              <p className="text-sm text-gray-600 mb-6">
                Informe o CNPJ/CPF do concedente responsável pelo estágio. Se o campo "Nome" não for preenchido automaticamente,
                clique no botão "<span className="font-semibold">+</span>" para realizar o cadastro do concedente.
              </p>

              <div className="grid grid-cols-1 md:grid-cols-2 gap-4 items-end">
                <div>
                  <label htmlFor="cnpjCpfEmpresa" className="text-sm font-medium text-gray-600">CNPJ/CPF</label>
                  <input
                    id="cnpjCpfEmpresa"
                    type="text"
                    value={cnpjCpfEmpresa}
                    onChange={(e) => setCnpjCpfEmpresa(e.target.value)}
                    className="w-full border rounded p-2 mt-1 text-gray-600 focus:ring-blue-500 focus:border-blue-500"
                    placeholder="Digite o CNPJ/CPF"
                    readOnly // Tornando somente leitura, pois é para visualização/preenchimento automático
                  />
                </div>
                <div>
                  <label htmlFor="nomeEmpresa" className="text-sm font-medium text-gray-600">Nome</label>
                  <div className="flex items-center">
                    <input
                      id="nomeEmpresa"
                      type="text"
                      value={nomeEmpresa}
                      onChange={(e) => setNomeEmpresa(e.target.value)}
                      className="w-full border rounded p-2 mt-1 text-gray-600 focus:ring-blue-500 focus:border-blue-500"
                      placeholder="Informe o nome da empresa"
                      readOnly // Tornando somente leitura
                    />
                    <button
                      className="ml-3 mt-1 bg-blue-600 hover:bg-blue-700 text-white p-2 rounded-md flex items-center justify-center w-10 h-10"
                      onClick={() => console.log("Botão '+' da empresa clicado")} // Lógica para adicionar/editar empresa
                      title="Cadastrar nova empresa"
                    >
                      <Plus size={24} />
                    </button>
                  </div>
                </div>
              </div>
            </div>

            {/* DADOS DO SUPERVISOR Card */}
            <div className="bg-white rounded-lg shadow p-6 mb-8 max-w-7xl">
              <h2 className="font-bold text-gray-800 mb-2 text-xl">DADOS DO REPRESENTANTE LEGAL</h2>
              <p className="text-sm text-gray-600 mb-6">
                Informe o CPF do representante legal da empresa responsável pelo estágio. Se o campo “Representante Legal” não for preenchido automaticamente, clique no botão “+” para realizar o cadastro do responsável. 
              </p>

              <div className="grid grid-cols-1 md:grid-cols-2 gap-4 items-end">
                <div>
                  <label htmlFor="cpfSupervisor" className="text-sm font-medium text-gray-600">CPF</label>
                  <input
                    id="cpfSupervisor"
                    type="text"
                    value={cpfSupervisor}
                    onChange={(e) => setCpfSupervisor(e.target.value)}
                    className="w-full border rounded p-2 mt-1 text-gray-600 focus:ring-blue-500 focus:border-blue-500"
                    placeholder="Digite o CPF do supervisor"
                    readOnly // Tornando somente leitura
                  />
                </div>
                <div>
                  <label htmlFor="nomeSupervisor" className="text-sm font-medium text-gray-600">Representante Legal</label> {/* Alterado para "Representante Legal" */}
                
                  <div className="flex items-center">
                    <input
                      id="nomeSupervisor"
                      type="text"
                      value={nomeSupervisor}
                      onChange={(e) => setNomeSupervisor(e.target.value)}
                      className="w-full border rounded p-2 mt-1 text-gray-600 focus:ring-blue-500 focus:border-blue-500"
                      placeholder="Informe o nome do supervisor"
                      readOnly // Tornando somente leitura
                    />
                    <button
                      className="ml-3 mt-1 bg-blue-600 hover:bg-blue-700 text-white p-2 rounded-md flex items-center justify-center w-10 h-10"
                      onClick={() => console.log("Botão '+' do supervisor clicado")} // Lógica para adicionar/editar supervisor
                      title="Cadastrar novo supervisor"
                    >
                      <Plus size={24} />
                    </button>
                  </div>
                </div>

                <div className="col-span-2 mt-4">
                <p className="font-bold text-gray-800 mb-2 text-xl">DADOS DO SUPERVISOR</p>
                <p className="text-sm text-gray-600 mb-6">Informe o CPF do responsável pelo estágio. Se o campo “Supervisor” não for preenchido automaticamente, clique no botão “+” para realizar o cadastro do responsável.</p>
                </div>
              </div>
            </div>

            {/* Navigation Buttons */}
            <div className="flex justify-between max-w-7xl mt-8">
              <Link
                title="Voltar para Dados Pessoais"
                href="/dados-1" //link de voltar
                className="border-2 border-blue-600 text-blue-600 hover:bg-blue-50 font-bold py-2 px-4 rounded-md flex items-center space-x-2 transition"
              >
                <ArrowLeft />
                <span>Voltar</span>
              </Link>

              <Link
                href="/dadosdaempresa" // Ajustado para a próxima etapa real (Dados do Estágio)
                title="Avançar para Dados do Estágio"
                className="bg-blue-500 hover:bg-blue-600 text-white py-2 px-4 rounded flex items-center space-x-2"
              >
                <span>Avançar</span>
                <ArrowRight size={24} className="hover:text-gray-200 cursor-pointer" />
              </Link>
            </div>
          </div>
        </div>
      </div>
    </DashboardLayout>
  );
}
