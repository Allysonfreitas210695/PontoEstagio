"use client";
import React, { useState } from 'react';
import Image from 'next/image';
import { Plus, ChevronLeft, ChevronRight } from 'lucide-react'; // Import icons
import logo from "../../../../../public/assets/image/logo2.png"; // Assuming this is the correct path for the logo
// Assuming these paths are correct relative to where this new file will be
import Sidebar from "../../../dashboard/Sidebar";
import DashboardLayout from "../../../dashboard/DashboardLayout";
import router from 'next/router';
import Link from 'next/link';

export default function DadosEmpresaSupervisor() {
  // State for Empresa Data
  const [cnpjCpfEmpresa, setCnpjCpfEmpresa] = useState("24.529.265/0001-40");
  const [nomeEmpresa, setNomeEmpresa] = useState("AltoTech");

  // State for Supervisor Data
  const [cpfSupervisor, setCpfSupervisor] = useState("047.038.038-74");
  const [nomeSupervisor, setNomeSupervisor] = useState("Allyson Bruno de Freitas Fernandes");

  return (
    <DashboardLayout>
      <div className="flex">
        <Sidebar />
        <div className="flex-1 bg-[#FAF9F6] min-h-screen p-8 pl-20">
          {/* Header Bar */}
          <div className="flex items-center justify-start w-full p-4 fixed top-0 left-0 bg-white shadow-md z-10 " style={{ paddingLeft: '1rem', backgroundColor: '#1D4ED8'}}> 
           <Image src={logo} alt="Logo" width={130} height={70} />
          </div>

          <div className="pt-1 px-6 mx-auto mt-12 w-full"> 
            {/* Progress Indicator */}
          <div className="flex justify-center items-center mb-10">
          {/* Etapa 1 */}
          <div className="flex items-center">
            <div className="w-10 h-10 rounded-full bg-blue-600 text-white flex items-center justify-center font-bold text-sm">1</div>
            <div className="h-1 bg-blue-600 w-80"></div>
          </div>

          {/* Etapa 2 */}
          <div className="flex items-center">
            <div className="w-10 h-10 rounded-full bg-blue-600 text-white flex items-center justify-center font-bold text-sm">2</div>
            <div className="h-1 bg-gray-300 w-80"></div>
          </div>

          {/* Etapa 3 */}
          <div className="flex items-center">
            <div className="w-10 h-10 rounded-full bg-gray-300 text-white flex items-center justify-center font-bold text-sm">3</div>
          </div>
        </div>

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
                    />
                    <button className="ml-3 mt-1 bg-blue-600 hover:bg-blue-700 text-white p-2 rounded-md flex items-center justify-center w-10 h-10">
                      <Plus size={24} />
                    </button>
                  </div>
                </div>
              </div>
            </div>

            {/* DADOS DO SUPERVISOR Card */}
            <div className="bg-white rounded-lg shadow p-6 mb-8 max-w-7xl">
              <h2 className="font-bold text-gray-800 mb-2 text-xl">DADOS DO SUPERVISOR</h2>
              <p className="text-sm text-gray-600 mb-6">
                Informe o CPF do responsável pelo estágio. Se o campo "Supervisor" não for preenchido automaticamente, clique no botão
                "<span className="font-semibold">+</span>" para realizar o cadastro do responsável.
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
                  />
                </div>
                <div>
                  <label htmlFor="nomeSupervisor" className="text-sm font-medium text-gray-600">Supervisor</label>
                  <div className="flex items-center">
                    <input
                      id="nomeSupervisor"
                      type="text"
                      value={nomeSupervisor}
                      onChange={(e) => setNomeSupervisor(e.target.value)}
                      className="w-full border rounded p-2 mt-1 text-gray-600 focus:ring-blue-500 focus:border-blue-500"
                    />
                    <button className="ml-3 mt-1 bg-blue-600 hover:bg-blue-700 text-white p-2 rounded-md flex items-center justify-center w-10 h-10">
                      <Plus size={24} />
                    </button>
                  </div>
                </div>
              </div>
            </div>

            {/* Navigation Buttons */}
            <div className="flex justify-between max-w-4xl mx-auto mt-8">
              <Link
                title="Voltar para Dados Pessoais"
                href="/dados-1" //link de voltar
                className="bg-gray-200 hover:bg-gray-300 text-gray-800 font-bold py-2 px-4 rounded-md flex items-center space-x-2"
              >
                <ChevronLeft size={20} />
                <span>Voltar</span>
              </Link>
              <Link
                href="/dadosdaempresa" //link de avançar
                title="Avançar para Dados da Empresa"
                 className="bg-blue-600 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded-md flex items-center space-x-2"
              >
                <span>Avançar</span>
                <ChevronRight size={20} />
              </Link>
            </div>

          </div>
        </div>
      </div>
    </DashboardLayout>
  );
}