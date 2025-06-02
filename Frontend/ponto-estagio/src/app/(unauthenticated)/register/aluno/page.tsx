"use client";
import { useState } from "react";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import Footer from "@/app/components/footer/page";
import Header from "@/app/components/header/page";

export default function RegistraSignup() {
  const [phone, setPhone] = useState("");

  const handlePhoneChange = (e: { target: { value: string } }) => {
    // Filtra apenas números
    const onlyNumbers = e.target.value.replace(/\D/g, "");
    setPhone(onlyNumbers);
  };
  return (
    <div className="bg-white min-h-screen flex flex-col items-center relative px-4">
      {/* Logo */}
      <Header />

      {/* Main Content */}
      <main className="flex flex-col items-center justify-center flex-1 w-full max-w-md px-4 py-8">
        <Card className="w-full">
          <CardHeader className="text-center">
            <CardTitle className="text-2xl text-gray-900">
              Cadastre-se
            </CardTitle>
            <CardDescription className="text-sm text-gray-600 mt-2 text-center">
              Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam
              eget ligula eu lectus lobortis.
            </CardDescription>
          </CardHeader>
          <CardContent className="space-y-4">
            <form className="space-y-4">
              {/* Universidade Dropdown */}
              <div className="space-y-2">
                <Label
                  htmlFor="university"
                  className="text-sm font-medium text-gray-700"
                >
                  Universidade
                </Label>
                <Select>
                  <SelectTrigger className="w-full border border-gray-300 rounded-md bg-white px-3 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent">
                    <SelectValue placeholder="Selecione..." />
                  </SelectTrigger>
                  <SelectContent className="max-h-60 overflow-y-auto border border-gray-300 rounded-md mt-1 bg-white z-50">
                    <SelectItem value="usp">
                      Universidade Federal Rural do Semi-Árido (UFERSA)
                    </SelectItem>
                    <SelectItem value="unicamp">
                      Universidade Estadual de Campinas (UNICAMP)
                    </SelectItem>
                    <SelectItem value="ufrj">
                      Universidade Federal do Rio de Janeiro (UFRJ)
                    </SelectItem>
                    <SelectItem value="ufmg">
                      Universidade Federal de Minas Gerais (UFMG)
                    </SelectItem>
                    <SelectItem value="puc">
                      Pontifícia Universidade Católica (PUC)
                    </SelectItem>
                  </SelectContent>
                </Select>
              </div>

              {/* Nome */}
              <div className="space-y-2">
                <Label
                  htmlFor="name"
                  className="text-sm font-medium text-gray-700"
                >
                  Nome
                </Label>
                <Input
                  id="name"
                  type="text"
                  placeholder="Digite seu nome"
                  className="w-full border border-gray-300 rounded-md px-3 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent"
                />
              </div>

              {/* Matrícula */}
              <div className="space-y-2">
                <Label
                  htmlFor="registration"
                  className="text-sm font-medium text-gray-700"
                >
                  Matrícula
                </Label>
                <Input
                  id="registration"
                  type="number"
                  placeholder="Número de matrícula"
                  className="w-full border border-gray-300 rounded-md px-3 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent"
                />
              </div>

              {/* Telefone */}
              <div className="space-y-2">
                <Label
                  htmlFor="phone"
                  className="text-sm font-medium text-gray-700"
                >
                  Telefone
                </Label>
                <Input
                  id="phone"
                  type="tel"
                  placeholder="(xx)xxxxx-xxxx"
                  inputMode="numeric"
                  value={phone}
                  onChange={handlePhoneChange}
                  className="w-full border border-gray-300 rounded-md px-3 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent"
                />
              </div>

              {/* Botão */}
              <Button
                type="submit"
                className="w-full bg-blue-600 hover:bg-blue-700 text-white font-medium py-2 px-4 rounded-md mt-6"
              >
                Finalizar
              </Button>
            </form>
          </CardContent>
        </Card>
      </main>

      {/* Footer */}
      <Footer />
    </div>
  );
}
