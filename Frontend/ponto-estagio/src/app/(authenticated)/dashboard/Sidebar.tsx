"use client"; // Necessário para usar useState e manipuladores de evento

import React, { useState, useEffect } from "react";
import { Home, ClipboardList, Camera, LogOut, ChevronRight } from "lucide-react"; // Adicionado CheckSquare, Camera e ChevronRight
import Link from "next/link";
import { usePathname } from 'next/navigation'; 
import Image from "next/image";
import logo from "../../../../public/assets/image/logo2.png"; 
 
export default function Sidebar() {
  const pathname = usePathname(); 
  const [isExpanded, setIsExpanded] = useState(false);

  const isActive = (path: string) => {
    if (path === '/solicitacaodeestagio') {
      return pathname.startsWith('/solicitacaodeestagio') || pathname.startsWith('/dados-1') || pathname.startsWith('/dados-2') || pathname.startsWith('/dadosdaempresa') || pathname.startsWith('/dadosdosupervisor' ) || pathname.startsWith('/dadosdorepresentante') || pathname.startsWith('/dadosdoestagio'); 
    }
    // Para outras rotas, a correspondência exata ou parcial é suficiente
    return pathname === path || pathname.startsWith(path + '/');
  };

  return (
    
    <aside
      className={`text-white h-screen flex flex-col items-center justify-between py-10 fixed left-0 top-0 transition-all duration-300 ease-in-out ${isExpanded ? 'w-64' : 'w-15'}`}
      style={{ backgroundColor: '#ABC8E2' }}
      
    >
      {/* Logo */}
      
      <div className="flex items-center justify-start w-full p-4 fixed top-0 left-0 bg-white shadow-md z-10 " style= {{ paddingLeft: '1rem', backgroundColor: '#1D4ED8'}}> 
        <Image src={logo} alt="Logo" width={130} height={70} />
      </div>
      
      {/* Seta para Expandir/Recolher */}
      <div className={`absolute top-4 ${isExpanded ? 'right-4' : 'right-0 -translate-x-1/2'} cursor-pointer transition-transform duration-300 ease-in-out`}>
        <ChevronRight
          size={24}
          className={`text-white transform ${isExpanded ? 'rotate-180' : ''}`}
          onClick={() => setIsExpanded(!isExpanded)}
        />
      </div>

      {/* Topo - ícones de navegação */}
      <div className="flex flex-col gap-2 mt-8 w-full items-center">
        {/* Home */}
        <Link href="/dashboard" className="w-full flex justify-center">
          <div
            className={`flex items-center justify-center p-2 rounded-lg cursor-pointer transition-colors duration-200 ease-in-out
              ${isActive('/dashboard') ? 'bg-blue-600 text-white' : 'hover:bg-blue-600 hover:bg-opacity-10'}`}
          >
            <Home size={24}className="text-white" />
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
              hover:bg-blue-600 acity-10`}
          >
            <LogOut size={24}className="text-white" />
          </div>
        </Link>
      </div>
    </aside>
  );
}