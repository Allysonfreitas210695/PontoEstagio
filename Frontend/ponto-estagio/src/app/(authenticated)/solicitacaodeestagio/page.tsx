"use client";


import { ArrowRight } from 'lucide-react';

import React from 'react';
import Image from 'next/image';
import { Plus } from 'lucide-react'; // Assuming Plus icon for "Cadastrar"

// Assuming these paths are correct relative to where this new file will be
import Sidebar from "../dashboard/Sidebar";
import DashboardLayout from "../dashboard/DashboardLayout";

// You'll need to create or adjust your logo if "REGISTRA" is an image,
// or just use text as shown in the image. For now, I'll use text.
import logo from "../../../../public/assets/image/logo2.png"; // Not used directly as "REGISTRA" is text

export default function SolicitacoesEstagio() {
  return (
    <DashboardLayout>
      <div className="flex"> {/* This flex container holds the Sidebar and the main content */}
        <Sidebar />
        <div className="flex-1 bg-[#FAF9F6] min-h-screen p-8 pl-24"> {/* Added pl-24 for content offset from sidebar */}
          {/* Header Bar */}
          <div className="flex items-center justify-between w-full p-4 fixed top-0 left-0 bg-white shadow-md z-10" style={{ paddingLeft: 'calc(1rem + 80px)' }}> {/* Adjust paddingLeft based on sidebar width */}
          <div className="text-blue-700 font-bold text-xl ml-4"><Image src={logo} alt="Logo" width={130} height={70} />
            </div>
          </div>

          {/* Main content area, adjusted to be below the fixed header */}
          <div className="pt-20"> {/* Adjust padding-top to account for fixed header height */}
            {/* User Profile and Cadastrar Button */}
            <div className="bg-white rounded-lg shadow p-6 mb-8 flex items-center justify-between max-w-4xl mx-auto mt-4">
              <div className="flex items-center">
                {/* Placeholder for profile image */}
                <div className="w-12 h-12 rounded-full bg-gray-300 flex items-center justify-center mr-4">
                  <Image src="/images/profile-placeholder.png" alt="Profile" width={48} height={48} className="rounded-full" />
                </div>
                <div>
                  <p className="font-semibold text-gray-800">Olá,</p>
                  <p className="text-gray-600">Eriky Abreu Veloso</p>
                </div>
              </div>
              <button className="bg-blue-600 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded-md flex items-center space-x-2">
                <Plus size={20} />
                <span>Cadastrar</span>
              </button>
            </div>

            {/* SOLICITAÇÕES DE ESTÁGIO Section */}
            <div className="max-w-4xl mx-auto mb-8">
              <h1 className="text-2xl font-bold text-gray-800 mb-4">SOLICITAÇÕES DE ESTÁGIO</h1>

              {/* CADASTRO DE SOLICITAÇÃO DE ESTÁGIO Card */}
              <div className="bg-white rounded-lg shadow p-6 mb-6">
                <h2 className="font-bold text-gray-800 mb-2">CADASTRO DE SOLICITAÇÃO DE ESTÁGIO</h2>
                <p className="text-sm text-gray-600 mb-4">
                  Bem-vindo à área de solicitação de estágio! Aqui você poderá cadastrar as informações do seu estágio supervisionado para análise e validação pelos responsáveis institucionais.
                </p>
                <p className="text-sm text-gray-600 mb-2">
                  Para iniciar uma nova solicitação, clique no botão "<span className="font-semibold">+ Cadastrar</span>".
                </p>
                <p className="text-sm text-gray-600">
                  Se você já possui uma solicitação recusada ou que necessita de ajustes, clique sobre ela para editar e reenviar com as devidas correções.
                </p>
              </div>

              {/* STATUS DA SOLICITAÇÃO Card */}
              <div className="bg-white rounded-lg shadow p-6 mb-6">
                <h2 className="font-bold text-gray-800 mb-2">STATUS DA SOLICITAÇÃO</h2>
                <p className="text-sm text-gray-600 mb-4">
                  Após o envio, sua solicitação passará por etapas de avaliação e poderá assumir os seguintes status:
                </p>
                <ul className="list-disc list-inside text-sm text-gray-600 space-y-2">
                  <li><span className="font-semibold">PENDENTE:</span> Sua solicitação foi recebida e está em processo de avaliação.</li>
                  <li><span className="font-semibold">NÃO COMPATÍVEL:</span> A solicitação contém informações que precisam ser ajustadas. Verifique as observações fornecidas, realize as correções e reenvie.</li>
                  <li><span className="font-semibold">AGUARDANDO APROVAÇÃO:</span> A solicitação foi aprovada pelo setor de estágios e agora está sob análise final da coordenação do curso.</li>
                  <li><span className="font-semibold">APROVADO:</span> Solicitação aprovada! Imprima o termo de compromisso em 4 vias e colete todas as assinaturas necessárias conforme orientação institucional.</li>
                </ul>
                <p className="text-sm text-gray-600 mt-4">
                  Fique atento ao status da sua solicitação e siga as orientações exibidas para garantir o andamento regular do seu processo de estágio.
                </p>
              </div>

              {/* Internship Table */}
              <div className="bg-white rounded-lg shadow p-6">
                <table className="min-w-full divide-y divide-gray-200">
                  <thead>
                    <tr>
                      <th scope="col" className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                        Contrato
                      </th>
                      <th scope="col" className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                        Situação
                      </th>
                      <th scope="col" className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                        Concedente
                      </th>
                      <th scope="col" className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                        Atividade
                      </th>
                      <th scope="col" className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                        Início
                      </th>
                      <th scope="col" className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                        Encerramento
                      </th>
                    </tr>
                  </thead>
                  <tbody className="bg-white divide-y divide-gray-200">
                    <tr>
                      <td className="px-6 py-4 whitespace-nowrap text-sm font-medium text-gray-900">
                        Estágio OBRIGATÓRIO
                      </td>
                      <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500 flex items-center">
                        <span className="w-2 h-2 rounded-full bg-orange-500 mr-2"></span>Pendente
                      </td>
                      <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                        AltoTech
                      </td>
                      <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                        Designer II
                      </td>
                      <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                        05/05/2025
                      </td>
                      <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
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