import Image from "next/image";
import Link from "next/link";

export default function HomePage() {
  return (
    <div className="relative min-h-screen flex flex-col  md:flex-row items-center justify-between bg-[#EDF3FF] overflow-hidden px-6 py-10 pl-20">
      <div className="absolute top-8 left-8 pl-12">
        <Image
          src={"/assets/image/logo.png"}
          alt="Logo"
          width={150}
          height={40}
        />
      </div>

      <div className="hidden md:block absolute rounded-bl-[300px] z-0 w-[700px] h-[400px] -top-10 right-0">
        <Image
          src={"/assets/image/textura.png"}
          alt="Textura"
          width={900}
          height={600}
        />
      </div>

      <div className="z-10 max-w-xl space-y-6 text-center md:text-left">
        <h1 className="text-3xl sm:text-4xl md:text-5xl font-bold text-blue-800 leading-tight">
          Descomplique o <br /> Cadastro de estágio <br /> com o cegistra
        </h1>
        <p className="text-gray-700 text-base md:text-lg">
          O Registra foi desenvolvido para modernizar o processo de solicitação e acompanhamento de estágios supervisionados no ensino superior. Com uma interface intuitiva e recursos automatizados, o sistema reduz a burocracia e garante mais eficiência, segurança e transparência.
        </p>
        <Link
          href="/login"
          className="bg-blue-600 hover:bg-blue-700 text-white px-6 py-3 rounded-md shadow-md transition"
        >
          Entrar →
        </Link>
      </div>

      <div className="relative z-19 mt-10 -bottom-20 -right-10 w-100%">
        <Image
          src={"/assets/image/cliente.png"}
          alt="Mulher com notebook"
          width={1200}
          height={500}
          className="w-full h-auto object-contain"
        />
      </div>
    </div>
  );
}
