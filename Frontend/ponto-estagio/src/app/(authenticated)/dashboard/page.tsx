"use client";

import Image from "next/image";
import logo from "../../../../public/assets/image/logo2.png";
import userImage from "../../../../public/assets/image/user.jpg";
import { useEffect, useState } from "react";
import Sidebar from "./Sidebar";
import DashboardLayout from "./DashboardLayout";
import { date } from "zod";

export default function Dashboard() {
  interface Atividade {
    data: string;
    status: string;
    atividade: string;
    supervisor: string;
  }

  const [atividades, setAtividades] = useState<Atividade[]>([]);

  useEffect(() => {
    fetch("/api/atividades")
      .then((res) => res.json())
      .then((data) => setAtividades(data));
  }, []);

  return (
    <DashboardLayout>
      <Sidebar />
      
      {/* Conteúdo */}
      <div className="px-6 ml-12 mx-auto mt-24 bg-[#FAF9F6] ">
        {/* Saudação */}
        <div className="flex items-center space-x-4 mt-4">
          <Image src={userImage} alt="User" width={40}  className="rounded-full" />
          <div>
            <p className="text-sm text-gray-600">Olá,</p>
            <p className="text-lg font-semibold text-black">Eriky Abreu Veloso</p>
          </div>
        </div>

        {/* Card com Cargo e barra */}
        <div className="bg-white shadow w-full sm:w-[200px] md:w-[700px] lg:w-[1350px] rounded-lg mt-6 p-4">

            <h2 className="font-semibold text-black">BRISACAT - DESIGNER II</h2>

            {/* Barra de progresso */}
            <div className="flex items-center mt-3 space-x-2 ">
              <div className="flex-1 h-2 bg-gray-300 rounded-full relative">
                <div className="absolute top-0 left-0 h-2 bg-blue-600 rounded-full" style={{ width: '80%' }} />
              </div>
              <span className="text-sm text-gray-500">180 hrs</span>
            </div>
        
          {/* Resumo */}
          <div className="flex justify-between items-center mt-5 mr-20">
            <CardResumo titulo="Aprovada" cor="green" valor={64} />
            <CardResumo titulo="Pendente" cor="orange" valor={3} />
            <CardResumo titulo="Reprovada" cor="red" valor={1} />
          </div>
          <p className="text-sm text-gray-500 mt-1">Última atualização: 25/04/2025 | 17hrs</p>
        </div>
        

        {/* Filtros e Tabela */}
       <div className="bg-white shadow-md sm:w-[200px] md:w-[700px] lg:w-[1350px] rounded-lg p-6 mt-8">
          <div className="flex flex-wrap justify-between items-center mb-4">
              <div className="flex gap-4">
                <select className="border border-gray-500 rounded px-3 py-1 text-sm text-black">
                  <option className="text-black" value="todas">Todas</option>
                  <option className="text-black" value="aprovadas">Aprovadas</option>
                  <option className="text-black" value="pendentes">Pendentes</option>
                </select>
                <select className="border rounded px-3 py-1 text-sm text-black">
                  <option className="text-black" value="semana">Semana atual</option>
                  <option className="text-black" value="mes">Mês atual</option>
                  <option className="text-black" value="ano">Ano atual</option>
                </select>
              </div>
            </div>
          
          <table className="w-full text-left">
            <thead className="bg-gray-100 text-gray-700 text-sm">
              <tr>
                <th className="p-2">Data</th>
                <th className="p-2">Situação</th>
                <th className="p-2">Atividade</th>
                <th className="p-2">Supervisor</th>
              </tr>
            </thead>
            <tbody>
              {atividades.map((item, i) => (
                <tr key={i} className="border-b hover:bg-gray-50 text-sm">
                  <td className="p-2">{item.data}</td>
                  <td className="p-2">
                    <span className={`text-${getColor(item.status)}-600 font-medium`}>
                      {item.status}
                    </span>
                  </td>
                  <td className="p-2">{item.atividade}</td>
                  <td className="p-2">{item.supervisor}</td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </div>
    </DashboardLayout>
  );
}

interface CardResumoProps {
  titulo: string;
  cor: string;
  valor: number;
}

function getColor(status: string): string {
  switch (status) {
    case "Aprovada": return "green";
    case "Pendente": return "orange";
    case "Reprovada": return "red";
    default: return "gray";
  }
}

function CardResumo({ titulo, cor, valor }: CardResumoProps) {
  return (
    <div className="bg-white p-4 rounded-lg text-center flex flex-col items-center justify-center gap-1">
      <div className="flex items-center gap-2">
        <span className="w-3 h-3 rounded-full" style={{ backgroundColor: cor }}></span>
        <p className="text-black font-semibold text-sm">{titulo.toUpperCase()}</p>
      </div>
      <p className="text-3xl font-bold text-black">{valor.toString().padStart(2, '0')}</p>
    </div>
  );
}
