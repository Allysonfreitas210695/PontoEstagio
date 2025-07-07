"use client";

import  Link  from "next/link";
import React from "react";
import Image from "next/image";
import { Plus } from "lucide-react";

// Componentes do seu layout
import Sidebar from "../dashboard/Sidebar";
import DashboardLayout from "../dashboard/DashboardLayout";

// Logo
import logo from "../../../../public/assets/image/logo2.png";
import userImage from "../../../../public/assets/image/user.jpg"; 
export default function SolicitacoesEstagio() {
  return (
    <DashboardLayout>
      <div className="flex">
        <Sidebar />
       
        <div className="flex-1 bg-white min-h-screen px-4 sm:px-8 md:pl-24 py-8">

          {/* Header fixo */}
          <div
            className="flex items-center justify-between w-full fixed top-0 left-0 bg-white shadow-md z-10"
            style={{ paddingLeft: "calc(1rem + 80px)" }}
          >
            <div className="flex items-center justify-start w-full p-4 fixed top-0 left-0 bg-white shadow-md z-10 " style={{ paddingLeft: '1rem', backgroundColor: '#1D4ED8'}}> 
                       <Image src={logo} alt="Logo" width={130} height={70} />
                      </div>
          </div>

          {/* Conteúdo principal */}
          <div className="pt-20">
            {/* Perfil do Usuário + Botão Cadastrar */}
            <div className="bg-white rounded-lg shadow p-6 mb-8 flex items-center justify-between max-w-7xl x-auto mt-4">
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
                  <p className="text-gray-600">Eriky</p>
                </div>
              </div>

              <Link
                href="/dados-1"
                className="bg-blue-600 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded-lg shadow-md flex items-center space-x-2"
              >
                <Plus size={20} />
                <span>Cadastrar</span>
              </Link>
            </div>

            {/* Título */}
            <div className="max-w-7xl mb-8">
              

              {/* Card: Cadastro de Solicitação */}
              <div className="bg-white rounded-lg shadow p-6 mb-6">
                <h1 className=" font-bold text-gray-600 mb-">
                SOLICITAÇÕES DE ESTÁGIO
                </h1><br />

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
              <div className="bg-white rounded-lg shadow p-6 overflow-x-auto">
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
                    <tr>
                      <td className="px-6 py-4 whitespace-nowrap text-sm font-medium text-gray-900">
                        Estágio OBRIGATÓRIO
                      </td>
                      <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-700 flex items-center">
                        <span className="w-2 h-2 rounded-full bg-amber-500 mr-2"></span>
                        Pendente
                      </td>
                      <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-700">
                        AltoTech
                      </td>
                      <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-700">
                        Designer II
                      </td>
                      <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-700">
                        05/05/2025
                      </td>
                      <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-700">
                        05/12/2025
                      </td>
                    </tr>
                  </tbody>
                </table>
              </div>
            </div>
          </div>
        </div>
      </div>
    </DashboardLayout>
  );
}
