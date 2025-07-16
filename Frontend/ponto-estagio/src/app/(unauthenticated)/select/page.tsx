"use client";

import Image from "next/image";
import CoordenadorImg from "../../../../public/assets/image/coordenador.png";
import AlunoImg from "../../../../public/assets/image/aluno.png";
import { ArrowRight } from "lucide-react";
import { Button } from "@/components/ui/button";
import { useRouter } from "next/navigation";
import Header from "@/app/components/header/page";
import Footer from "@/app/components/footer/page";
import { useState } from "react";

type UserType = "Intern" | "Coordinator";

export default function SelectPage() {
  const router = useRouter();
  const [isNavigating, setIsNavigating] = useState(false);

  const handleNavigation = (userType: UserType) => {
    // Armazena o tipo de usuário no localStorage antes de navegar
    setIsNavigating(true);
    localStorage.setItem('userType', userType);
    router.push("/register");
  };

  return (
    <section className="flex flex-col items-center min-h-screen bg-white px-4 py-10">
      {/* Logo */}
      <Header />

      {/* Cards */}
      <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
        {/* Coordenador */}
        <div
          className="bg-white border rounded-2xl shadow p-6 flex flex-col items-center max-w-sm text-center"
          style={{ minWidth: "100px", maxWidth: "300px", height: "auto" }}
        >
          <Image
            src={CoordenadorImg}
            alt="Coordenador"
            width={250}
            height={250}
          />
          <h2 className="text-xl font-semibold mt-4">Sou coordenador</h2>
          <p className="text-sm text-gray-600 mt-2">
            Quero aprovar as solicitações de cadastro de estágio, simplificar o
            processo e muito mais!
          </p>
          <Button
            onClick={() => handleNavigation("Coordinator")}
            disabled={isNavigating}
            variant="outline"
            className="mt-6 flex items-center gap-2 text-blue-600 border-blue-600 hover:bg-blue-50"
          >
            Avançar <ArrowRight className="w-4 h-4" />
          </Button>
        </div>

        {/* Aluno */}
        <div
          className="bg-white border rounded-2xl shadow p-6 flex flex-col items-center max-w-sm text-center"
          style={{ minWidth: "100px", maxWidth: "300px", height: "auto" }}
        >
          <Image src={AlunoImg} alt="Aluno" width={250} height={250} />
          <h2 className="text-xl font-semibold mt-4">Sou aluno</h2>
          <p
            className="text-sm text-gray-600 mt-2"
            style={{ minWidth: "100", maxWidth: "200" }}
          >
            Quero registrar meu estágio, cadastrar meus pontos, acompanhar as
            aprovações e muito mais!
          </p>
          <Button
            onClick={() => handleNavigation("Intern")}
            disabled={isNavigating}
            variant="outline"
            className="mt-6 flex items-center gap-2 text-blue-600 border-blue-600 hover:bg-blue-50"
          >
            Avançar <ArrowRight className="w-4 h-4" />
          </Button>
        </div>
      </div>
      <Footer />
    </section>
  );
}