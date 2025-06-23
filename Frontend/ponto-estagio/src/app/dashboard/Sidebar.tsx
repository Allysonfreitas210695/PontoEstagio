import { Home, LogOut, ClipboardList, CalendarCheck } from "lucide-react";
import Link from "next/link";

export default function Sidebar() {
  return (
<<<<<<< Updated upstream:Frontend/ponto-estagio/src/app/dashboard/Sidebar.tsx
    <aside className=" text-white h-screen w-14 flex flex-col items-center justify-between py-6 fixed left-0 top-0 "  style={{ backgroundColor: '#ABC8E2' }}> 
=======
    <aside className=" text-white h-screen w-14 flex flex-col items-center justify-between py-16 fixed left-0"  style={{ backgroundColor: '#E5EEF5', left: 0, top: 20, bottom: 0 }}> 
>>>>>>> Stashed changes:Frontend/ponto-estagio/src/app/(authenticated)/dashboard/Sidebar.tsx
      {/* Topo - ícones */}
      <div className="flex flex-col gap-6 COLOR-[#1D4ED8]">
        <Link href="/">
          <Home size={28} className="cursor-pointer hover:text-blue-600 " />
        </Link>
        <Link href="/dados-1">
          <ClipboardList size={24} className="cursor-pointer hover:text-blue-600" />
        </Link>
        <Link href="/agenda">
          <CalendarCheck size={24} className="cursor-pointer hover:text-blue-600" /> 
        </Link>
      </div>

      {/* Rodapé - ícone de logout */}
      <div>
<<<<<<< Updated upstream:Frontend/ponto-estagio/src/app/dashboard/Sidebar.tsx
        <Link href="auth/login">
          <LogOut size={28} className="hover:text-gray-200 cursor-pointer" />
=======
        <Link href="/unauthenticated/login">
          <LogOut size={24} className="hover:text-gray-200 cursor-pointer" />
>>>>>>> Stashed changes:Frontend/ponto-estagio/src/app/(authenticated)/dashboard/Sidebar.tsx
        </Link>
      </div>
    </aside>
  );
}
