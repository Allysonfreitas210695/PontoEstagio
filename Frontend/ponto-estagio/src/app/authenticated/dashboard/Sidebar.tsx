import { Home, LogOut } from "lucide-react";
import Link from "next/link";

export default function Sidebar() {
  return (
    <aside className=" text-white h-screen w-14 flex flex-col items-center justify-between py-6 fixed left-0 top-0 "  style={{ backgroundColor: '#1D4ED8' }}> 
      {/* Topo - ícones */}
      <div className="flex flex-col gap-6">
        <Link href="/">
          <Home size={28} className="hover:text-gray-200 cursor-pointer Block" />
        </Link>
      </div>

      {/* Rodapé - logout */}
      <div>
        <Link href="/unauthenticated/login">
          <LogOut size={28} className="hover:text-gray-200 cursor-pointer" />
        </Link>
      </div>
    </aside>
  );
}
