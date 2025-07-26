'use client';
import Image from "next/image";
import userImage from "@/../public/assets/image/user.jpg";
import { useEffect, useState } from "react";
import Sidebar from "./Sidebar";
import DashboardLayout from "./DashboardLayout";

interface ResponseProjectJson {
  id: string;
  name: string;
  description: string;
  status: string;
  startDate: string;
  endDate: string;
  createdAt: string;
  attendances?: any[]; 
}

interface ResponseAttendanceJson {
  id: string;
  userId: string;
  date: string;
  createdAt: string;
  checkIn: string;
  checkOut: string;
  status: 'Approved' | 'Pending' | 'Rejected'; 
  project: ResponseProjectJson;
}

// Interface para os dados exibidos na tabela
interface Atividade {
  data: string;
  status: string;
  atividade: string;
  supervisor: string; 
}

export default function Dashboard() {
  const [atividades, setAtividades] = useState<Atividade[]>([]);
  const [approvedCount, setApprovedCount] = useState(0);
  const [pendingCount, setPendingCount] = useState(0);
  const [rejectedCount, setRejectedCount] = useState(0);

// Função auxiliar para mapear o status em inglês da API para português para exibição
  const mapStatusToPortuguese = (status: 'Approved' | 'Pending' | 'Rejected'): string => {
    switch (status) {
      case 'Approved': return 'Aprovada';
      case 'Pending': return 'Pendente';
      case 'Rejected': return 'Reprovada';
      default: return 'Desconhecido';
    }
  };

  // Função para buscar dados de frequência
 useEffect(() => {
    const fetchAttendanceData = async () => {
      try {
       const authenticatedUserId = '61a59179-0d07-488b-be36-6809c10de8bf'; // ID do admin@admin.com 
        const token = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IkFkbWluIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvc2lkIjoiNjFhNTkxNzktMGQwNy00ODhiLWJlMzYtNjgwOWMxMGRlOGJmIiwicm9sZSI6IkFkbWluIiwibmJmIjoxNzUyODA0NjM0LCJleHAiOjE3NTI4MDgyMzQsImlhdCI6MTc1MjgwNDYzNH0.t-kh2dEiC6XT6uT1ZkoGqhdborzMxbHRs_bU-haILCg'; // <--- Se precisar de autenticação, obtenha o token aqui

        // Constrói os query parameters para a requisição GET
        const queryParams = new URLSearchParams({
          userId: authenticatedUserId,
          startDate: '2022-01-01', 
          endDate: '2025-12-31',
        }).toString();

        const response = await fetch(`${process.env.NEXT_PUBLIC_API_BASE_URL}/attendance?${queryParams}`, {
          method: 'GET',
          headers: {
            'Content-Type': 'application/json', 
             'Authorization': `Bearer ${token}`, // Se necessário, adicione o token de autenticação
          },
        });
        if (!response.ok) {
          throw new Error(`HTTP error! status: ${response.status}`);
        }
        console.log("API URL", process.env.NEXT_PUBLIC_API_BASE_URL);
        console.log("API Token", token);
        const data: ResponseAttendanceJson[] = await response.json();

        const mappedAtividades: Atividade[] = data.map((item) => ({
          data: new Date(item.date).toLocaleDateString('pt-BR'), // Format date to Brazilian standard
          status: mapStatusToPortuguese(item.status), // Map English status to Portuguese
          atividade: item.project?.name || item.project?.description || "Atividade não especificada",
          
          // Por enquanto, está codificado como 'N/A'. Considere atualizar sua API de backend
          // para incluir o nome do supervisor diretamente na resposta de frequência para maior eficiência.
          supervisor: "N/A",
        }));

        setAtividades(mappedAtividades);
        // Calcula as contagens para os cartões de resumo
        const approved = mappedAtividades.filter(a => a.status === 'Aprovada').length;
        const pending = mappedAtividades.filter(a => a.status === 'Pendente').length;
        const rejected = mappedAtividades.filter(a => a.status === 'Reprovada').length;

        setApprovedCount(approved);
        setPendingCount(pending);
        setRejectedCount(rejected);

      } catch (error) {
        console.error("Erro ao buscar atividades:", error);
        // Implemente a exibição de erro amigável ao usuário aqui (por exemplo, uma notificação toast)
      }
    };

    fetchAttendanceData();
  }, []); 
  // Array de dependências vazio significa que este efeito é executado uma vez na montagem

  return (
    <DashboardLayout>
      <Sidebar />
      {/* Área de Conteúdo Principal */}
      <div className="px-6 ml-12 mx-auto mt-24 bg-[#FAF9F6] ">
        {/* Seção de Saudação */}
        <div className="flex items-center space-x-4 mt-4">
          <Image src={userImage} alt="User" width={40} height={40} className="rounded-full" />
          <div>
            <p className="text-sm text-gray-600">Olá,</p>
            {/* O nome do usuário idealmente deve vir de um endpoint de usuário autenticado (por exemplo, /api/logged-user) */}
            <p className="text-lg font-semibold text-black">Eriky Abreu Veloso</p>
          </div>
        </div>

        {/* Cartão de Cargo com Barra de Progresso e Resumo */}
        <div className="bg-white shadow w-full sm:w-[200px] md:w-[700px] lg:w-[1350px] rounded-lg mt-6 p-4">
          {/* Cargo e Empresa - Isso também deve ser dinâmico, possivelmente de uma API de perfil de usuário */}
          <h2 className="font-semibold text-black">BRISACAT - DESIGNER II</h2>

          {/* Barra de Progresso - Horas e porcentagem devem ser dinâmicas de uma API de relatório (por exemplo, /api/report/monthly) */}
          <div className="flex items-center mt-3 space-x-2 ">
            <div className="flex-1 h-2 bg-gray-300 rounded-full relative">
              <div className="absolute top-0 left-0 h-2 bg-blue-600 rounded-full" style={{ width: '80%' }} /> {/* Hardcoded width */}
            </div>
            <span className="text-sm text-gray-500">180 hrs</span> {/* Hardcoded hours */}
          </div>
          {/* Cartões de Resumo - Agora atualizados dinamicamente com base nos dados buscados */}
          <div className="flex justify-between items-center mt-5 mr-20">
            <CardResumo titulo="Aprovada" cor="green" valor={approvedCount} />
            <CardResumo titulo="Pendente" cor="orange" valor={pendingCount} />
            <CardResumo titulo="Reprovada" cor="red" valor={rejectedCount} />
          </div>
          {/* A data/hora da última atualização também deve ser dinâmica, talvez da atividade mais recente ou de um relatório */}
          <p className="text-sm text-gray-500 mt-1">Última atualização: 25/04/2025 | 17hrs</p>
        </div>
        
        {/* Filters and Table */}
        <div className="bg-white shadow-md sm:w-[200px] md:w-[700px] lg:w-[1350px] rounded-lg p-6 mt-8">
          <div className="flex flex-wrap justify-between items-center mb-4">
            <div className="flex gap-4">
              {/* Filter dropdowns - You'll need to add state and logic to filter 'atividades' based on selections */}
              {/* Dropdowns de Filtro - Você precisará adicionar estado e lógica para filtrar 'atividades' com base nas seleções */}
              <select className="border border-gray-500 rounded px-3 py-1 text-sm text-black">
                <option className="text-black" value="todas">Todas</option>
                <option className="text-black" value="aprovadas">Aprovadas</option>
                <option className="text-black" value="pendentes">Pendentes</option>
                <option className="text-black" value="reprovadas">Reprovadas</option> {/* Added Reprovadas option */}
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
              {atividades.length > 0 ? (
                atividades.map((item, i) => (
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
                ))
              ) : (
                <tr>
                  <td colSpan={4} className="p-4 text-center text-gray-500">Nenhuma atividade encontrada.</td>
                </tr>
              )}
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

// Helper function to get color based on status (Portuguese)
// Função auxiliar para obter a cor com base no status (Português)
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
