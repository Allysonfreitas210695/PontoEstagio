"use client";
import React, { useState } from 'react';
import Image from 'next/image';
import { ChevronLeft, ChevronRight } from 'lucide-react'; // For navigation buttons
import logo from "../../../../public/assets/image/logo2.png"; // Assuming this is the correct path for the logo
// Assuming these paths are correct relative to where this new file will be
import Sidebar from "../dashboard/Sidebar";
import DashboardLayout from "../dashboard/DashboardLayout";

export default function DadosSupervisor() {
  // State for Supervisor Data
  const [nome, setNome] = useState("Allyson Bruno de Freitas Fernandes");
  const [cpf, setCpf] = useState("845.948.846-84");
  const [telefone, setTelefone] = useState("+55 (84) 98147-1097");
  const [email, setEmail] = useState("allyson@alto.ufersa.edu.br");
  const [cargo, setCargo] = useState("Gerente de Projetos");

  const handleCancel = () => {
    // Implement cancel logic, e.g., router.back() or router.push('/')
    console.log("Cancel button clicked!");
    alert("Operação cancelada.");
  };

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    // Implement form submission logic here
    console.log("Cadastrar button clicked!");
    console.log({
      nome, cpf, telefone, email, cargo
    });
    alert("Dados do supervisor cadastrados com sucesso! (Simulação)");
  };

  return (
    <DashboardLayout>
      <div className="flex">
        <Sidebar />
        <div className="flex-1 bg-[#FAF9F6] min-h-screen p-8 pl-24"> {/* Added pl-24 for content offset from sidebar */}

          {/* Main content area, adjusted to be below the fixed header */}
          <div className="pt-20"> {/* Adjust padding-top to account for fixed header height */}

            {/* Progress Indicator (Step 2 active) */}
            <div className="flex justify-center items-center gap-4 mb-10 max-w-lg mx-auto">
              {/* Etapa 1 */}
              <div className="flex items-center">
                <div className="w-10 h-10 rounded-full bg-blue-600 text-white flex items-center justify-center font-bold text-lg">1</div>
                {/* Line after step 1, should be blue to connect to step 2 */}
                <div className="flex-grow h-1 bg-blue-600 w-20 sm:w-24 md:w-32"></div>
              </div>

              {/* Etapa 2 */}
              <div className="flex items-center">
                <div className="w-10 h-10 rounded-full bg-blue-600 text-white flex items-center justify-center font-bold text-lg">2</div>
                {/* Line after step 2, should be gray as step 3 is not yet active/completed */}
                <div className="flex-grow h-1 bg-gray-300 w-20 sm:w-24 md:w-32"></div>
              </div>

              {/* Etapa 3 */}
              <div className="flex items-center">
                <div className="w-10 h-10 rounded-full bg-gray-300 text-white flex items-center justify-center font-bold text-lg">3</div>
              </div>
            </div>

            {/* DADOS DO SUPERVISOR Card (Main Form) */}
            <div className="bg-white rounded-lg shadow p-6 mb-8 max-w-4xl mx-auto">
              <h2 className="font-bold text-gray-800 mb-2 text-xl">DADOS DO SUPERVISOR</h2>
              <p className="text-sm text-gray-600 mb-6">
                Informe os dados do supervisor responsável pelo estágio.
              </p>

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
                      className="w-full border rounded p-2 mt-1 text-gray-600 focus:ring-blue-500 focus:border-blue-500"
                      required
                    />
                  </div>
                </div>

                {/* Action Buttons */}
                <div className="flex justify-end space-x-4 mt-8">
                  <button
                    type="button"
                    onClick={handleCancel}
                    className="bg-gray-200 hover:bg-gray-300 text-gray-800 font-bold py-2 px-6 rounded-md transition duration-150 ease-in-out"
                  >
                    Cancelar
                  </button>
                  <button
                    type="submit"
                    className="bg-blue-600 hover:bg-blue-700 text-white font-bold py-2 px-6 rounded-md transition duration-150 ease-in-out"
                  >
                    Cadastrar
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