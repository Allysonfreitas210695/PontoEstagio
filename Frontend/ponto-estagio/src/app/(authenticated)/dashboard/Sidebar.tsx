import { Home, LogOut, ClipboardList, CalendarCheck } from "lucide-react";
import Link from "next/link";

export default function Sidebar() {
  return (
    <aside className=" text-white h-screen w-14 flex flex-col items-center justify-between py-6 fixed left-0 top-0 "  style={{ backgroundColor: '#ABC8E2' }}> 
      {/* Topo - ícones */}
      <div className="flex flex-col gap-6 COLOR-[#1D4ED8]">
        <Link href="/">
          <Home size={28} className="cursor-pointer hover:text-blue-600 " />
        </Link>
        <Link href="/solicitacaodeestagio">
          <ClipboardList size={24} className="cursor-pointer hover:text-blue-600" />
        </Link>
        <Link href="/agenda">
          <CalendarCheck size={24} className="cursor-pointer hover:text-blue-600" /> 
        </Link>
      </div>

      {/* Rodapé - ícone de logout */}
      <div>
        <Link href="/login">
          <LogOut size={28} className="hover:text-gray-200 cursor-pointer" />
        </Link>
      </div>
    </aside>
  );
}
