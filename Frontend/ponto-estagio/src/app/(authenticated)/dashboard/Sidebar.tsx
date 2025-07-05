"use client"; // Necessário para usar useState e manipuladores de evento

import React, { useState, useEffect } from "react";
import { Home, ClipboardList, Camera, LogOut, ChevronRight } from "lucide-react"; // Adicionado CheckSquare, Camera e ChevronRight
import Link from "next/link";
import { usePathname } from 'next/navigation'; // Hook para obter a rota atual no App Router

export default function Sidebar() {
  const pathname = usePathname(); // Obtém o caminho atual da URL

  // Estado para controlar se a sidebar está expandida ou recolhida
  const [isExpanded, setIsExpanded] = useState(false);

  // Função para determinar se um link está ativo
  const isActive = (path: string) => {
    // Para a rota de Solicitações de Estágio, verificamos se a rota atual começa com '/solicitacao'
    if (path === '/solicitacaodeestagio') {
      return pathname.startsWith('/solicitacao');
    }
    // Para outras rotas, a correspondência exata ou parcial é suficiente
    return pathname === path || pathname.startsWith(path + '/');
  };

  return (
    // A classe `w-14` define a largura padrão (recolhida).
    // `w-64` (ou outra largura) seria para o estado expandido.
     <aside
      className={`text-white h-screen flex flex-col items-center justify-between py-6 fixed left-0 top-0 transition-all duration-300 ease-in-out ${isExpanded ? 'w-64' : 'w-14'}`}
      style={{ backgroundColor: '#ABC8E2' }}
    >
      {/* Seta para Expandir/Recolher */}
      <div className={`absolute top-4 ${isExpanded ? 'right-4' : 'right-0 -translate-x-1/2'} cursor-pointer transition-transform duration-300 ease-in-out`}>
        <ChevronRight
          size={24}
          className={`text-white transform ${isExpanded ? 'rotate-180' : ''}`}
          onClick={() => setIsExpanded(!isExpanded)}
        />
      </div>

      {/* Topo - ícones de navegação */}
      <div className="flex flex-col gap-4 mt-8 w-full items-center">
        {/* Home */}
        <Link href="/dashboard" className="w-full flex justify-center">
          <div
            className={`flex items-center justify-center p-2 rounded-lg cursor-pointer transition-colors duration-200 ease-in-out
              ${isActive('/dashboard') ? 'bg-blue-600 text-white' : 'hover:bg-blue-600 hover:bg-opacity-10'}`}
          >
            <Home size={28} className="text-white" />
          </div>
        </Link>

        {/* Solicitações de Estágio (CheckSquare) - AGORA COM CONDIÇÃO MÚLTIPLA */}
        <Link href="/solicitacaodeestagio" className="w-full flex justify-center">
          <div
            className={`flex items-center justify-center p-2 rounded-lg cursor-pointer transition-colors duration-200 ease-in-out
              ${isActive('/solicitacaodeestagio') ? 'bg-blue-600 text-white' : 'hover:bg-blue-600 hover:bg-opacity-10'}`}
          >
            <ClipboardList size={24} className="text-white" />
          </div>
        </Link>

        {/* Agenda (Camera) */}
        <Link href="/agenda" className="w-full flex justify-center">
          <div
            className={`flex items-center justify-center p-2 rounded-lg cursor-pointer transition-colors duration-200 ease-in-out
              ${isActive('/agenda') ? 'bg-blue-600 text-white' : 'hover:bg-blue-600 hover:bg-opacity-10'}`}
          >
            <Camera size={24} className="text-white" />
          </div>
        </Link>
      </div>

      {/* Rodapé - ícone de logout */}
      <div className="w-full flex justify-center mb-8">
        <Link href="/login">
          <div
            className={`flex items-center justify-center p-2 rounded-lg cursor-pointer transition-colors duration-200 ease-in-out
              hover:bg-white hover:bg-opacity-10`}
          >
            <LogOut size={28} className="text-white" />
          </div>
        </Link>
      </div>
    </aside>
  );
}