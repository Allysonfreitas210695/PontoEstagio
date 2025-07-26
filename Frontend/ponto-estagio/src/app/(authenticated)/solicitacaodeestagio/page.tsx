"use client";

import Link from "next/link";
import React, { useEffect, useState } from "react";
import Image from "next/image";
import { Plus } from "lucide-react";
import { fetchFromApi } from '@/api/api'; // Importando a função de fetch da API
// Componentes do seu layout
import Sidebar from "../dashboard/Sidebar";
import DashboardLayout from "@/app/(authenticated)/dashboard/DashboardLayout";

// Logos
import userImage from "@/../public/assets/image/user.jpg";

// Interface para os dados de projeto retornados pela API (baseado no Swagger)
interface ResponseProjectJson {
  id: string;
  name: string;
  description: string;
  status: string; // Ex: 'Pending', 'Rejected', 'Approved'
  startDate: string;
  endDate: string;
  createdAt: string;
  
}

// Interface para os dados de solicitação de estágio a serem exibidos na tabela
interface InternshipRequest {
  id: string;
  contrato: string;
  situacao: string;
  concedente: string;
  atividade: string;
  inicio: string;
  encerramento: string;
}

export default function SolicitacoesEstagio() {
  const [internshipRequests, setInternshipRequests] = useState<InternshipRequest[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  // Função para mapear o status da API (em inglês) para o texto em português
  const mapProjectStatusToPortuguese = (status: string): string => {
    switch (status) {
      case 'Pending': return 'Pendente';
      case 'Rejected': return 'Não Compatível';
      case 'Approved': return 'Aprovado';
      // Adicione outros mapeamentos de status da API se existirem
      default: return 'Desconhecido';
    }
  };

  // Função para obter a cor do status com base no texto em português
  const getProjectStatusColor = (status: string): string => {
    switch (status) {
      case "Aprovado": return "green";
      case "Pendente": return "amber"; // Usando amber para Pendente
      case "Não Compatível": return "red";
      case "Aguardando Aprovação": return "blue"; // Se você tiver este status específico da API
      default: return "gray";
    }
  };

  // Efeito para buscar os dados de solicitações de estágio da API
  useEffect(() => {
    const fetchInternshipRequests = async () => {
      try {
        const response = await fetchFromApi("/api/project");

        if (!response.ok) {
          throw new Error(`Erro HTTP! Status: ${response.status}`);
        }

        const data: ResponseProjectJson[] = await response.json();

        // Mapeia a resposta da API para a interface InternshipRequest
        const mappedRequests: InternshipRequest[] = data.map((item) => ({
          id: item.id,
          contrato: item.name || "N/A", // Usando o nome do projeto como "Contrato"
          situacao: mapProjectStatusToPortuguese(item.status),
          concedente: "Empresa Concedente (N/A)", 
          atividade: item.description || "N/A", // Usando a descrição como "Atividade"
          inicio: new Date(item.startDate).toLocaleDateString('pt-BR'),
          encerramento: new Date(item.endDate).toLocaleDateString('pt-BR'),
        }));

        setInternshipRequests(mappedRequests);
      } catch (err: any) {
        console.error("Erro ao buscar solicitações de estágio:", err);
        setError("Não foi possível carregar as solicitações de estágio.");
      } finally {
        setLoading(false);
      }
    };

    fetchInternshipRequests();
  }, []); // Array de dependências vazio para executar apenas uma vez na montagem

  return (
    <DashboardLayout>
      <div className="flex">
        <Sidebar />
        
        <div className="ml-9 mx-auto mt-2 bg-[#FAF9F6] ">

          {/* Conteúdo principal */}
          <div className="pt-20 sm:w-[200px] md:w-[700px] lg:w-[1350px]"> 
            <div className="bg-white rounded-lg shadow p-6 mb-8 flex items-center justify-between max-w-7xl mx-auto mt-4">
              <div className="flex items-center">
                <div className="w-12 h-12 rounded-full bg-gray-300 flex items-center justify-center mr-4">
                  <Image
                    src={userImage}
                    alt="Profile"
                    width={48}
                    height={48}
                    className="rounded-full"
                  />
                </div>
                <div>
                  <p className="font-semibold text-gray-800">Olá,</p>
                  {/* O nome do usuário deve vir dinamicamente de uma API de perfil do usuário */}
                  <p className="text-gray-600">Eriky</p>
                </div>
              </div>

              <Link
                href="/dados-1" // Link para a página de cadastro de dados
                className="bg-blue-600 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded-lg shadow-md flex items-center space-x-2"
              >
                <Plus size={20} />
                <span>Cadastrar</span>
              </Link>
            </div>

            {/* Card: Informações sobre Cadastro de Solicitação e Status */}
            <div className="bg-white rounded-lg shadow p-6 mb-6 max-w-7xl mx-auto">
              <h1 className="font-bold text-gray-600 mb-4">
                SOLICITAÇÕES DE ESTÁGIO
              </h1>

              <h2 className="font-bold text-blue-600 mb-2 ">
                CADASTRO DE SOLICITAÇÃO DE ESTÁGIO
              </h2>
              <p className="text-sm text-gray-600 mb-4">
                Bem-vindo à área de solicitação de estágio! Aqui você poderá
                cadastrar as informações do seu estágio supervisionado para
                análise e validação pelos responsáveis institucionais.
              </p>
              <p className="text-sm text-gray-600 mb-2">
                Para iniciar uma nova solicitação, clique no botão{" "}
                <span className="font-semibold">"+ Cadastrar"</span>.
              </p>
              <p className="text-sm text-gray-600">
                Se você já possui uma solicitação recusada ou que necessita
                de ajustes, clique sobre ela para editar e reenviar com as
                devidas correções.
              </p> <br />
            
              <h2 className="font-bold text-blue-600 mb-2 ">
                STATUS DA SOLICITAÇÃO
              </h2>
              <p className="text-sm text-gray-600 mb-4">
                Após o envio, sua solicitação passará por etapas de avaliação
                e poderá assumir os seguintes status:
              </p>
              <ul className="list-disc list-inside text-sm text-gray-600 space-y-2">
                <li>
                  <span className="font-semibold">PENDENTE:</span> Sua
                  solicitação foi recebida e está em processo de avaliação.
                </li>
                <li>
                  <span className="font-semibold">NÃO COMPATÍVEL:</span> A
                  solicitação contém informações que precisam ser ajustadas.
                  Verifique as observações fornecidas, realize as correções e
                  reenvie.
                </li>
                <li>
                  <span className="font-semibold">AGUARDANDO APROVAÇÃO:</span>{" "}
                  A solicitação foi aprovada pelo setor de estágios e agora
                  está sob análise final da coordenação do curso.
                </li>
                <li>
                  <span className="font-semibold">APROVADO:</span> Solicitação
                  aprovada! Imprima o termo de compromisso em 4 vias e colete
                  todas as assinaturas necessárias conforme orientação
                  institucional.
                </li>
              </ul>
              <p className="text-sm text-gray-600 mt-4">
                Fique atento ao status da sua solicitação e siga as
                orientações exibidas para garantir o andamento regular do seu
                processo de estágio.
              </p>
            </div>

            {/* Tabela de Solicitações */}
            <div className="bg-white rounded-lg shadow p-6 overflow-x-auto max-w-7xl mx-auto">
              <h2 className="text-xl font-bold text-gray-800 mb-4">Minhas Solicitações</h2>
              {loading ? (
                <p className="text-center text-gray-600">Carregando solicitações...</p>
              ) : error ? (
                <p className="text-center text-red-600">{error}</p>
              ) : internshipRequests.length === 0 ? (
                <p className="text-center text-gray-600">Nenhuma solicitação de estágio encontrada.</p>
              ) : (
                <table className="min-w-full divide-y divide-blue-600">
                  <thead>
                    <tr>
                      <th className="px-6 py-3 text-left text-xs font-medium text-blue-600 uppercase tracking-wider">
                        Contrato
                      </th>
                      <th className="px-6 py-3 text-left text-xs font-medium text-blue-600 uppercase tracking-wider">
                        Situação
                      </th>
                      <th className="px-6 py-3 text-left text-xs font-medium text-blue-600 uppercase tracking-wider">
                        Concedente
                      </th>
                      <th className="px-6 py-3 text-left text-xs font-medium text-blue-600 uppercase tracking-wider">
                        Atividade
                      </th>
                      <th className="px-6 py-3 text-left text-xs font-medium text-blue-600 uppercase tracking-wider">
                        Início
                      </th>
                      <th className="px-6 py-3 text-left text-xs font-medium text-blue-600 uppercase tracking-wider">
                        Encerramento
                      </th>
                    </tr>
                  </thead>
                  <tbody className="bg-white divide-y divide-gray-200">
                    {internshipRequests.map((request) => (
                      <tr key={request.id} className="hover:bg-gray-50">
                        <td className="px-6 py-4 whitespace-nowrap text-sm font-medium text-gray-900">
                          {request.contrato}
                        </td>
                        <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-700 flex items-center">
                          <span 
                            className={`w-2 h-2 rounded-full mr-2`} 
                            style={{ backgroundColor: getProjectStatusColor(request.situacao) }}
                          ></span>
                          {request.situacao}
                        </td>
                        <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-700">
                          {request.concedente}
                        </td>
                        <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-700">
                          {request.atividade}
                        </td>
                        <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-700">
                          {request.inicio}
                        </td>
                        <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-700">
                          {request.encerramento}
                        </td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              )}
            </div>
          </div>
        </div>
      </div>
    </DashboardLayout>
  );
}
