"use client";
import React, { useState, useEffect } from 'react';
import { useRouter, useSearchParams } from 'next/navigation'; // Importe useSearchParams
import Sidebar from "@/app/(authenticated)/dashboard/Sidebar";
import DashboardLayout from "@/app/(authenticated)/dashboard/DashboardLayout";
import { ChevronLeft, ChevronRight } from 'lucide-react'; // Para botões de navegação
import Link from 'next/link';
// import { toast } from 'react-toastify'; // Importe sua biblioteca de toast, se estiver usando

// Importa as funções e tipos do seu novo serviço de API
import { legalRepresentativeApi, authApi, LegalRepresentativeRegisterDTO } from '@/api/apiService';

export default function DadosSupervisor() {
  const router = useRouter();
  const searchParams = useSearchParams();
  const userId = searchParams.get('userId'); // Obtém o userId da URL (do DadosPessoais)
  const companyId = searchParams.get('companyId'); // Obtém o companyId da URL (do DadosEmpresaCompleto)

  // State for Supervisor Data
  const [nome, setNome] = useState("");
  const [cpf, setCpf] = useState("");
  const [telefone, setTelefone] = useState("");
  const [email, setEmail] = useState("");
  const [cargo, setCargo] = useState(""); // Mapeia para 'position' na API

  const [isSaving, setIsSaving] = useState(false);
  const [error, setError] = useState<string | null>(null);

  // Verifica se userId e companyId foram passados
  useEffect(() => {
    if (!userId) {
      console.error("ID do usuário não fornecido. Redirecionando para a primeira etapa do cadastro.");
      // toast.error("ID do usuário não fornecido. Por favor, volte à etapa anterior.");
    //router.push('/dados-1');
    }
    if (!companyId) {
      console.error("ID da empresa não fornecido. Redirecionando para a etapa de cadastro da empresa.");
      // toast.error("ID da empresa não fornecido. Por favor, volte à etapa anterior.");
      //router.push(`/dados-2?userId=${userId}`); // Volta para a página de dados da empresa
    }
  }, [userId, companyId, router]);

  const handleCancel = () => {
    console.log("Operação de cancelamento acionada.");
    // Implemente a lógica de cancelamento, por exemplo, redirecionar para uma página inicial
    router.push('/dashboard'); // Exemplo: redireciona para o dashboard
    // alert("Operação cancelada."); // Removido alert(), use toast ou um modal customizado
  };

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    setIsSaving(true);
    setError(null);

    if (!userId || !companyId) {
      setError("IDs de usuário ou empresa ausentes. Não foi possível cadastrar o supervisor.");
      setIsSaving(false);
      return;
    }

    try {
      const token = localStorage.getItem("token");
      if (!token) {
        throw new Error("Token de autenticação não encontrado.");
      }

      // 1. Cadastrar o Supervisor (POST /api/legal-representatives)
      const supervisorData: LegalRepresentativeRegisterDTO = {
        fullName: nome, 
        cpf: cpf,
        position: cargo, 
        email: email,
        companyId: companyId, 
      };

      console.log("Payload de cadastro do supervisor:", supervisorData);

      const newSupervisor = await legalRepresentativeApi.createLegalRepresentative(supervisorData, token);
      console.log("Representante cadastrado com sucesso:", newSupervisor);
      //toast.success("Dados do supervisor cadastrados com sucesso!");

     
      const userUpdateData = {
        id: userId,
        supervisorId: newSupervisor.id, // Se a API aceitar
      };
      await authApi.updateUser(userId, userUpdateData, token);
      console.log("Usuário atualizado com supervisor associado.");
      
      router.push(`/dadosdoestagio?userId=${userId}&companyId=${companyId}&supervisorId=${newSupervisor.id}`);

    } catch (err: any) {
      setError(err.message || "Ocorreu um erro ao cadastrar o supervisor.");
      console.error("Erro ao cadastrar supervisor:", err);
    } finally {
      setIsSaving(false);
    }
  };

  return (
    <DashboardLayout>
      <div className="flex">
        <Sidebar />
        <div className="flex-1 bg-[#FAF9F6] min-h-screen p-1 pl-18">

          {/* Main content area, adjusted to be below the fixed header */}
          <div className="pt-20">

             <div className="flex justify-center items-center sm:w-[200px] md:w-[700px] lg:w-[1350px] mb-10 ">
              
              {/* Etapa 1 */}
              <div className="flex items-center">
                <div className="w-10 h-10 rounded-full bg-blue-600 text-white flex items-center justify-center font-bold text-sm">1</div>
                <div className="h-1 bg-blue-600 sm:w-[50px] md:w-[120px] lg:w-[330px]"></div>
              </div>

              {/* Etapa 2 */}
              <div className="flex items-center">
                <div className="w-10 h-10 rounded-full bg-blue-600 text-white flex items-center justify-center font-bold text-sm">2</div> {/* Corrigido para blue-600 */}
                <div className="h-1 bg-gray-300 sm:w-[50px] md:w-[120px] lg:w-[330px]"></div>
              </div>

              {/* Etapa 3 */}
              <div className="flex items-center">
                <div className="w-10 h-10 rounded-full bg-gray-300 text-white flex items-center justify-center font-bold text-sm">3</div>
              </div>
            </div>
            {/* DADOS DO SUPERVISOR Card (Main Form) */}
             <div className="bg-white rounded-lg shadow p-6 mb-8 max-w-7xl">
              <h2 className="font-bold text-gray-800 mb-2 text-xl">DADOS DO REPRESENTANDE LEGAL</h2>
              <p className="text-sm text-gray-600 mb-6">
                Informe os dados do supervisor responsável pelo estágio.
              </p>

              {error && <div className="bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded relative mb-4" role="alert">
                <span className="block sm:inline">{error}</span>
              </div>}

              <form onSubmit={handleSubmit}>
                <div className="grid grid-cols-1 md:grid-cols-2 gap-x-6 gap-y-4 mb-6">
                  {/* Row 1 */}
                  <div>
                    <label htmlFor="nomeSupervisor" className="block text-sm font-medium text-gray-600">Nome *</label>
                    <input
                      id="nomeSupervisor"
                      type="text"
                      value={nome}
                      onChange={(e) => setNome(e.target.value)}
                      className="w-full border rounded p-2 mt-1 text-gray-600 focus:ring-blue-500 focus:border-blue-500"
                      placeholder="Digite o nome completo do supervisor"
                      required
                    />
                  </div>
                  <div>
                    <label htmlFor="cpfSupervisor" className="block text-sm font-medium text-gray-600">CPF *</label>
                    <input
                      id="cpfSupervisor"
                      type="text"
                      value={cpf}
                      onChange={(e) => setCpf(e.target.value)}
                      className="w-full border rounded p-2 mt-1 text-gray-600 focus:ring-blue-500 focus:border-blue-500"
                      placeholder="Digite o CPF do supervisor"
                      required
                    />
                  </div>

                  {/* Row 2 */}
                  <div>
                    <label htmlFor="telefoneSupervisor" className="block text-sm font-medium text-gray-600">Telefone *</label>
                    <input
                      id="telefoneSupervisor"
                      type="tel"
                      value={telefone}
                      onChange={(e) => setTelefone(e.target.value)}
                      className="w-full border rounded p-2 mt-1 text-gray-600 focus:ring-blue-500 focus:border-blue-500"
                      placeholder="Digite o telefone do supervisor"
                      required
                    />
                  </div>
                  <div>
                    <label htmlFor="emailSupervisor" className="block text-sm font-medium text-gray-600">E-mail *</label>
                    <input
                      id="emailSupervisor"
                      type="email"
                      value={email}
                      onChange={(e) => setEmail(e.target.value)}
                      className="w-full border rounded p-2 mt-1 text-gray-600 focus:ring-blue-500 focus:border-blue-500"
                      placeholder="Digite o e-mail do supervisor"
                      required
                    />
                  </div>

                  {/* Row 3 */}
                  <div className="md:col-span-2"> {/* This input spans both columns */}
                    <label htmlFor="cargoSupervisor" className="block text-sm font-medium text-gray-600">Cargo *</label>
                    <input
                      id="cargoSupervisor"
                      type="text"
                      value={cargo}
                      onChange={(e) => setCargo(e.target.value)}
                      className="w-full border rounded p-2 mt-1 text-gray-600 focus:ring-gray-500 focus:border-black-800"
                      placeholder="Digite o cargo do supervisor"
                      required
                    />
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
      </div>
    </DashboardLayout>
  );
}
