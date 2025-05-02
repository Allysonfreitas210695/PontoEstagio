import Image from "next/image";
import Link from "next/link";
import cliente from "../app/assets/image/cliente.png"
import logo from "../app/assets/image/logo.png";
import textura from "../app/assets/image/textura.png";

export default function HomePage() {
  return (
    <div className="relative min-h-screen flex flex-col  md:flex-row items-center justify-between bg-[#EDF3FF] overflow-hidden px-6 py-10 pl-20">
      {/* Logo */}
      <div className="absolute top-8 left-8 pl-12">
        <Image src={logo} alt="Logo" width={150} height={40} />
      </div>

      {/* Forma azul (decoração) */}
      <div className="hidden md:block absolute rounded-bl-[300px] z-0 w-[700px] h-[400px] -top-10 right-0">  
        <Image src={textura} alt="Textura" />  
      </div>  

      {/* Conteúdo do texto */}
      <div className="z-10 max-w-xl space-y-6 text-center md:text-left">
        <h1 className="text-3xl sm:text-4xl md:text-5xl font-bold text-blue-800 leading-tight">
          Transforme o <br />
          Controle de Ponto <br />
          em Produtividade!
        </h1>
        <p className="text-gray-700 text-base md:text-lg">
          Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam eget ligula eu lectus lobortis condimentum.
        </p>
        <Link href="/auth/login">
          <button className="bg-blue-600 hover:bg-blue-700 text-white px-6 py-3 rounded-md shadow-md transition">
            Entrar →
          </button>
        </Link>
      </div>

      {/* Imagem da mulher */}
      <div className="relative z-19 mt-10 -bottom-20 -right-10 w-100%" >
        <Image
          src={cliente}
          alt="Mulher com notebook"
          width={1200}
          height={500}
          className="w-full h-auto object-contain"
        />
      </div>
    </div>
  );
}
