"use client"; // ← ESSA LINHA É FUNDAMENTAL NO APP ROUTER

import Image from "next/image";
import logo from "../../app/assets/image/logo2.png"; // ajuste conforme necessário
import userImage from "../../app/assets/image/user.jpg";
import { useEffect, useState } from "react";
import Sidebar from "./Sidebar";
import DashboardLayout from "./DashboardLayout";

export default function Dashboard() {
  interface Atividade {
    data: string;
    status: string;
    atividade: string;
    supervisor: string;
  }

  const [atividades, setAtividades] = useState<Atividade[]>([]);

  useEffect(() => {
    // Chamada à API aqui (exemplo fictício):
    fetch("/api/atividades")
      .then(res => res.json())
      .then(data => setAtividades(data));
  }, []);

  return (
  
    <DashboardLayout>
      {/* Header */}
    <div className="flex items-center justify-between w-full p-5 shadow-md" style={{ backgroundColor: '#1D4ED8' }}>
      <Image src={logo} alt="Logo" width={130} height={70} />
    </div>
      <div className="p-6 space-y-6 bg-gray-100 min-h-screen text-gray-800">
        {/* Header */}
          <div className="flex-3 flex mt-4 p-4">
            <Image
              src={userImage}
              alt="User"
              width={30}
              className="rounded-full"
            />
            <p className="text-sm">Olá,</p>
            <p className="text-xl font-bold">Eriky Abreu Veloso</p>
        </div>
      
        
        {/* Resumo */}
        <div className="grid grid-cols-3 gap-4 mt-6 h40">
          <CardResumo titulo="Aprovada" cor="green" valor={64} />
          <CardResumo titulo="Pendente" cor="orange" valor={3} />
          <CardResumo titulo="Reprovada" cor="red" valor={1} />
        </div>

        {/* Tabela */}
        <div className="bg-white shadow-md rounded-lg p-4 mt-8">
          <table className="w-full text-left">
            <thead>
              <tr className="border-b">
                <th>Data</th>
                <th>Situação</th>
                <th>Atividade</th>
                <th>Supervisor</th>
              </tr>
            </thead>
            <tbody>
              {atividades.map((item, i) => (
                <tr key={i} className="border-b">
                  <td>{item.data}</td>
                  <td>
                    <span className={`text-${getColor(item.status)}-500`}>
                      {item.status}
                    </span>
                  </td>
                  <td>{item.atividade}</td>
                  <td>{item.supervisor}</td>
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
    <div className="bg-white shadow p-4 rounded-lg text-center flex items-center justify-center gap-2">
      {/* Bolinha com cor dinâmica */}
      <span
        className="w-3 h-3 rounded-full"
        style={{ backgroundColor: cor }}
      ></span>
      
      {/* Texto sempre preto */}
      <p className="text-black font-semibold">{titulo.toUpperCase()}</p>
      
      {/* Valor */}
      <p className="text-2xl font-bold">{valor.toString().padStart(2, '0')}</p>
    </div>
  );
}