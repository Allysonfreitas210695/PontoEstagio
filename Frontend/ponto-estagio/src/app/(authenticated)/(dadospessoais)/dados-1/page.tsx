"use client";
import React, { useEffect, useState } from 'react';
import Image from 'next/image';
import { useRouter } from 'next/navigation';

import logo from "../../../../../public/assets/image/logo2.png";
import Sidebar from "../../../dashboard/Sidebar";
import DashboardLayout from "../../../dashboard/DashboardLayout";
import { ArrowRight, Link as LinkIcon } from 'lucide-react';
import Link from 'next/link';


export default function DadosPessoais() {
  const router = useRouter();

  // Estados para os inputs controlados
  const [nome, setNome] = useState("Eriky Abreu Veloso");
  const [matricula, setMatricula] = useState("2025011494");
  const [telefone, setTelefone] = useState("+55 (84) 98147-1097");
  const [email, setEmail] = useState("erikyabreu@alunos.ufersa.edu.br");
  const [universidade, setUniversidade] = useState("Universidade Federal Rural do Semi-Árido");
  const [curso, setCurso] = useState("Engenharia de Software");
  const [coordenador, setCoordenador] = useState("João Batista de Souza Neto");

  return (
    <DashboardLayout>
      <div className="flex">
      <Sidebar />
      <div className="flex-1 bg-[#FAF9F6] min-h-screen p-8 pl-20"> 
         <div className="flex items-center justify-start w-full p-4 fixed top-0 left-0 bg-white shadow-md z-10 " style={{ paddingLeft: '1rem', backgroundColor: '#1D4ED8'}}> 
           <Image src={logo} alt="Logo" width={130} height={70} />
          </div>

          {/* Footer de navegação de etapas */}
          <div className="pt-1 px-6 mx-auto mt-12 w-full"> 
           {/* Etapa 1 */}
          <div className="flex justify-center items-center mb-10">
            {/* Etapa 1 */}
            <div className="flex items-center">
              <div className="w-10 h-10 rounded-full bg-blue-600 text-white flex items-center justify-center font-bold text-sm">1</div>
              <div className="h-1 bg-gray-300 w-80"></div>
            </div>

            {/* Etapa 2 */}
            <div className="flex items-center">
              <div className="w-10 h-10 rounded-full bg-gray-300 text-white flex items-center justify-center font-bold text-sm">2</div>
              <div className="h-1 bg-gray-300 w-80"></div>
            </div>

            {/* Etapa 3 */}
            <div className="flex items-center">
              <div className="w-10 h-10 rounded-full bg-gray-300 text-white flex items-center justify-center font-bold text-sm">3</div>
            </div>
           </div>
          </div>

          {/* Card principal */}
          <div className="bg-white rounded-lg shadow p-6 mb-8 max-w-7xl">
            <h2 className="font-bold text-gray-800 mb-2 text-xl">DADOS PESSOAIS</h2>
            <p className="text-sm text-gray-600 mb-6">Confirme e/ou atualize os seus dados pessoais</p>

            <div className="grid grid-cols-1 md:grid-cols-2 gap-4 items-end">
              <div>
                <label className="text-sm font-medium text-gray-600">Nome</label>
                <input
                  type="text"
                  value={nome}
                  onChange={(e) => setNome(e.target.value)}
                  className="w-full border rounded p-2 mt-1 text-gray-600"
                />
              </div>

              <div>
                <label className="text-sm font-medium text-gray-800">Matrícula</label>
                <input
                  type="text"
                  value={matricula}
                  onChange={(e) => setMatricula(e.target.value)}
                  className="w-full border rounded p-2 mt-1 text-gray-600"
                />
              </div>

              <div>
                <label className="text-sm font-medium text-gray-600">Telefone</label>
                <input
                  type="tel"
                  value={telefone}
                  onChange={(e) => setTelefone(e.target.value)}
                  className="w-full border rounded p-2 mt-1 text-gray-600"
                />
              </div>

              <div>
                <label className="text-sm font-medium text-gray-600">E-mail</label>
                <input
                  type="email"
                  value={email}
                  onChange={(e) => setEmail(e.target.value)}
                  className="w-full border rounded p-2 mt-1 text-gray-600"
                />
              </div>

              <div className="md:col-span-2">
                <label className="text-sm font-medium text-gray-600">Universidade</label>
                <input
                  type="text"
                  value={universidade}
                  onChange={(e) => setUniversidade(e.target.value)}
                  className="w-full border rounded p-2 mt-1 text-gray-600"
                />
              </div>
              
              <div>
                <label className="text-sm font-medium text-gray-600">Curso</label>
                <select
                  value={curso}
                  onChange={(e) => setCurso(e.target.value)}
                  className="w-full border rounded p-2 mt-1 text-gray-600"
                >
                  <option>Engenharia de Software</option>
                  <option>Ciência da Computação</option>
                </select>
              </div>

              <div>
                <label className="text-sm font-medium text-gray-600">Coordenador</label>
                <select
                  value={coordenador}
                  onChange={(e) => setCoordenador(e.target.value)}
                  className="w-full border rounded p-2 mt-1 text-gray-600"
                >
                  <option>João Batista de Souza Neto</option>
                  <option>Outro coordenador</option>
                </select>
              </div>
            </div>
          </div>
          <div className="flex relative justify-end mt-6 max-w-7xl">
            <Link
              href="/dados-2"
              title="Avançar para Dados Pessoais 2"
              className="bg-blue-500 hover:bg-blue-600 text-white py-2 px-4 rounded flex items-center space-x-2"
            >
              <span>Avançar</span>
              <ArrowRight size={24} className="hover:text-gray-200 cursor-pointer" />
            </Link>
        </div>
      </div>
    </div>
    </DashboardLayout>
  );
}
