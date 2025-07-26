"use client";
import React, { useState } from 'react';
import Image from 'next/image';
import Sidebar from "@/app/(authenticated)/dashboard/Sidebar"; // Ajuste o caminho se necessário
import DashboardLayout from "@/app/(authenticated)/dashboard/DashboardLayout"; // Ajuste o caminho se necessário
import { useRouter } from 'next/navigation'; // Importe useRouter para redirecionamento
import Link from 'next/link';

export default function DadosEmpresaCompleto() {
  const router = useRouter();

  // State for Empresa Data
  const [nome, setNome] = useState(""); // Nome da empresa
  const [cnpjCpf, setCnpjCpf] = useState(""); // CNPJ/CPF da empresa
  const [uf, setUf] = useState(""); // Estado (UF)
  const [telefone, setTelefone] = useState(""); // Telefone da empresa
  const [email, setEmail] = useState(""); // E-mail da empresa
  const [cep, setCep] = useState(""); // CEP
  const [logradouro, setLogradouro] = useState(""); // Rua
  const [numero, setNumero] = useState(""); // Número
  const [bairro, setBairro] = useState(""); // Bairro
  const [municipio, setMunicipio] = useState(""); // Cidade
  const [complemento, setComplemento] = useState(""); // Complemento (adicionado, pois está no Swagger)
  const [registroProfissionalLiberal, setRegistroProfissionalLiberal] = useState(""); // Não mapeado diretamente no Swagger
  const [tipoPessoa, setTipoPessoa] = useState("Pessoa Jurídica"); // Default

  const [isSaving, setIsSaving] = useState(false);
  const [error, setError] = useState<string | null>(null);

  // Function to handle Cancel button click
  const handleCancel = () => {
    console.log("Operação de cancelamento acionada. Redirecionando para dashboard.");
    router.push('/dashboard'); // Redireciona para o dashboard ou página inicial
  };

  // Function to handle form submission
  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setIsSaving(true);
    setError(null);

    try {
      const token = localStorage.getItem("token"); // Obtém o token do localStorage
      if (!token) {
        throw new Error("Token de autenticação não encontrado. Por favor, faça login.");
      }

      // Mapear os dados do formulário para o payload esperado pela API POST /api/company
      const companyPayload = {
        request: { // O Swagger mostra que o corpo da requisição pode ser aninhado em 'request'
          name: nome,
          cnpj: cnpjCpf, // Assumindo que o campo é 'cnpj' na API
          email: email,
          phone: telefone,
          isActive: true, // Assumindo que a empresa é criada como ativa
          address: {
            street: logradouro,
            number: numero,
            district: bairro,
            city: municipio,
            state: uf,
            zipCode: cep,
            complement: complemento // Incluído o complemento
          }
         
          // professionalRegistration: registroProfissionalLiberal,
          // personType: tipoPessoa === "Pessoa Jurídica" ? "Legal" : "Physical", // Mapear para o enum da API
        }
      };

      console.log("Dados a serem enviados para a API (Empresa):", companyPayload);

      const response = await fetch('http://localhost:5019/api/company', { // URL da API para criar empresa
        method: 'POST',
        headers: {
          'Content-Type': 'application/json', // Importante: JSON
          'Authorization': `Bearer ${token}`, // Inclui o token de autenticação
        },
        body: JSON.stringify(companyPayload),
      });

      const result = await response.json(); // Tenta ler a resposta mesmo em caso de erro

      if (response.ok) {
        console.log("Dados da empresa cadastrados com sucesso:", result);
        // toast.success("Empresa cadastrada com sucesso!");
        // Opcional: Redirecionar para a próxima etapa, talvez passando o ID da empresa criada
        router.push(`/dados-empresa-supervisor?companyId=${result.id}`); // Exemplo de redirecionamento para a próxima etapa
      } else {
        setError(result.message || `Erro ao cadastrar dados da empresa: ${response.statusText}`);
        console.error("Erro ao cadastrar dados da empresa:", response.status, result);
      }
    } catch (err: any) {
      setError(err.message || "Ocorreu um erro na requisição da API.");
      console.error("Erro na requisição da API:", err);
    } finally {
      setIsSaving(false);
    }
  };

  return (
    <DashboardLayout>
      <div className="flex overflow-hidden">
        <Sidebar />
        <div className="flex-1 bg-[#FAF9F6] min-h-screen p-6 overflow-hidden">
          <div className="pt-1 px-14 mx-auto mt-12 w-full ">

            <div className="flex justify-center items-center sm:w-[200px] md:w-[700px] lg:w-[1350px] mb-10 ">
              {/* Etapa 1 */}
              <div className="flex items-center">
                <div className="w-10 h-10 rounded-full bg-blue-600 text-white flex items-center justify-center font-bold text-sm">1</div>
                <div className="h-1 bg-blue-600 sm:w-[50px] md:w-[120px] lg:w-[330px]"></div>
              </div>

              {/* Etapa 2 */}
              <div className="flex items-center">
                <div className="w-10 h-10 rounded-full bg-blue-600 text-white flex items-center justify-center font-bold text-sm">2</div> {/* Corrigido para blue-600 */}
                <div className="h-1 bg-gray-300 sm:w-[50px] md:w-[120px] lg:w-[330px]"></div>
              </div>

              {/* Etapa 3 */}
              <div className="flex items-center">
                <div className="w-10 h-10 rounded-full bg-gray-300 text-white flex items-center justify-center font-bold text-sm">3</div>
              </div>
            </div>

            {/* DADOS DA EMPRESA Card (Main Form) */}
            <div className="bg-white sm:w-[200px] md:w-[700px] lg:w-[1350px] rounded-lg shadow p-6 mb-8 max-w-7xl ">
              <h2 className="font-bold text-gray-800 mb-2 text-xl">DADOS DA EMPRESA</h2>
              <p className="text-sm text-gray-600 mb-6">
                Informe os dados da empresa ou do profissional liberal concedente do estágio.
              </p>

              {error && <div className="bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded relative mb-4" role="alert">
                <span className="block sm:inline">{error}</span>
              </div>}

              <form onSubmit={handleSubmit}>
                <div className="grid grid-cols-1 md:grid-cols-3 gap-x-6 gap-y-4 mb-6">
                  {/* Row 1 */}
                  <div>
                    <label htmlFor="nomeEmpresa" className="block text-sm font-medium text-gray-600">Nome</label>
                    <input
                      id="nomeEmpresa"
                      type="text"
                      value={nome}
                      onChange={(e) => setNome(e.target.value)}
                      className="w-full border rounded p-2 mt-1 text-gray-600 focus:ring-blue-500 focus:border-blue-500"
                      placeholder="Informe o nome da empresa"
                      required
                    />
                  </div>
                  <div>
                    <label htmlFor="cnpjCpf" className="block text-sm font-medium text-gray-600">CNPJ/CPF *</label>
                    <input
                      id="cnpjCpf"
                      type="text"
                      value={cnpjCpf}
                      onChange={(e) => setCnpjCpf(e.target.value)}
                      className="w-full border rounded p-2 mt-1 text-gray-600 focus:ring-blue-500 focus:border-blue-500"
                      placeholder="Informe o CNPJ/CPF"
                      required
                    />
                  </div>
                  <div>
                    <label htmlFor="uf" className="block text-sm font-medium text-gray-600">UF *</label>
                    <select
                      id="uf"
                      value={uf}
                      onChange={(e) => setUf(e.target.value)}
                      className="w-full border rounded p-2 mt-1 text-gray-600 focus:ring-blue-500 focus:border-blue-500"
                      required
                    >
                      <option value="">Selecione um Estado</option>
                      <option value="AC">AC</option>
                      <option value="AL">AL</option>
                      <option value="AM">AM</option>
                      <option value="AP">AP</option>
                      <option value="BA">BA</option>
                      <option value="CE">CE</option>
                      <option value="DF">DF</option>
                      <option value="ES">ES</option>
                      <option value="GO">GO</option>
                      <option value="MA">MA</option>
                      <option value="MG">MG</option>
                      <option value="MS">MS</option>
                      <option value="MT">MT</option>
                      <option value="PA">PA</option>
                      <option value="PB">PB</option>
                      <option value="PE">PE</option>
                      <option value="PI">PI</option>
                      <option value="PR">PR</option>
                      <option value="RJ">RJ</option>
                      <option value="RN">RN</option>
                      <option value="RO">RO</option>
                      <option value="RR">RR</option>
                      <option value="RS">RS</option>
                      <option value="SC">SC</option>
                      <option value="SE">SE</option>
                      <option value="SP">SP</option>
                      <option value="TO">TO</option>
                    </select>
                  </div>

                  {/* Row 2 */}
                  <div>
                    <label htmlFor="telefone" className="block text-sm font-medium text-gray-600">Telefone *</label>
                    <input
                      id="telefone"
                      type="tel"
                      value={telefone}
                      onChange={(e) => setTelefone(e.target.value)}
                      className="w-full border rounded p-2 mt-1 text-gray-600 focus:ring-blue-500 focus:border-blue-500"
                      placeholder="Digite seu telefone"
                      required
                    />
                  </div>
                  <div>
                    <label htmlFor="email" className="block text-sm font-medium text-gray-600">E-mail *</label>
                    <input
                      id="email"
                      type="email"
                      value={email}
                      onChange={(e) => setEmail(e.target.value)}
                      className="w-full border rounded p-2 mt-1 text-gray-600 focus:ring-blue-500 focus:border-blue-500"
                      placeholder="Digite seu e-mail"
                      required
                    />
                  </div>
                  <div>
                    <label htmlFor="cep" className="block text-sm font-medium text-gray-600">CEP *</label>
                    <input
                      id="cep"
                      type="text"
                      value={cep}
                      onChange={(e) => setCep(e.target.value)}
                      className="w-full border rounded p-2 mt-1 text-gray-600 focus:ring-blue-500 focus:border-blue-500"
                      placeholder="Digite o CEP"
                      required
                    />
                  </div>

                  {/* Row 3 */}
                  <div>
                    <label htmlFor="logradouro" className="block text-sm font-medium text-gray-600">Logradouro *</label>
                    <input
                      id="logradouro"
                      type="text"
                      value={logradouro}
                      onChange={(e) => setLogradouro(e.target.value)}
                      className="w-full border rounded p-2 mt-1 text-gray-600 focus:ring-blue-500 focus:border-blue-500"
                      placeholder="Informe o logradouro"
                      required
                    />
                  </div>
                  <div>
                    <label htmlFor="numero" className="block text-sm font-medium text-gray-600">N.° *</label>
                    <input
                      id="numero"
                      type="text"
                      value={numero}
                      onChange={(e) => setNumero(e.target.value)}
                      className="w-full border rounded p-2 mt-1 text-gray-600 focus:ring-blue-500 focus:border-blue-500"
                      placeholder="Informe o número"
                      required
                    />
                  </div>
                  <div>
                    <label htmlFor="bairro" className="block text-sm font-medium text-gray-600">Bairro *</label>
                    <input
                      id="bairro"
                      type="text"
                      value={bairro}
                      onChange={(e) => setBairro(e.target.value)}
                      className="w-full border rounded p-2 mt-1 text-gray-600 focus:ring-blue-500 focus:border-blue-500"
                      placeholder="Informe o bairro"
                      required
                    />
                  </div>

                  {/* Row 4 */}
                  <div>
                    <label htmlFor="municipio" className="block text-sm font-medium text-gray-600">Município</label>
                    <input
                      id="municipio"
                      type="text"
                      value={municipio}
                      onChange={(e) => setMunicipio(e.target.value)}
                      className="w-full border rounded p-2 mt-1 text-gray-600 focus:ring-blue-500 focus:border-blue-500"
                      placeholder="Digite o município"
                      required // Adicionado required
                    />
                  </div>
                  <div>
                    <label htmlFor="complemento" className="block text-sm font-medium text-gray-600">Complemento</label>
                    <input
                      id="complemento"
                      type="text"
                      value={complemento}
                      onChange={(e) => setComplemento(e.target.value)}
                      className="w-full border rounded p-2 mt-1 text-gray-600 focus:ring-blue-500 focus:border-blue-500"
                      placeholder="Digite o complemento (opcional)"
                    />
                  </div>
                  <div>
                    <label htmlFor="registroProfissionalLiberal" className="block text-sm font-medium text-gray-600">Registro Profissional Liberal</label>
                    <input
                      id="registroProfissionalLiberal"
                      type="text"
                      value={registroProfissionalLiberal}
                      onChange={(e) => setRegistroProfissionalLiberal(e.target.value)}
                      className="w-full border rounded p-2 mt-1 text-gray-600 focus:ring-blue-500 focus:border-blue-500"
                      placeholder="Digite o registro profissional liberal (opcional)"
                    />
                  </div>
                </div>

                {/* Tipo de Pessoa Radio Buttons */}
                <div className="flex items-center space-x-6 mt-4 mb-8">
                  <span className="text-sm font-medium text-gray-600">Tipo *</span>
                  <div className="flex items-center">
                    <input
                      id="pessoaJuridica"
                      type="radio"
                      name="tipoPessoa"
                      value="Pessoa Jurídica"
                      checked={tipoPessoa === "Pessoa Jurídica"}
                      onChange={(e) => setTipoPessoa(e.target.value)}
                      className="form-radio h-4 w-4 text-blue-600"
                      required
                    />
                    <label htmlFor="pessoaJuridica" className="ml-2 text-sm text-gray-700">Pessoa Jurídica</label>
                  </div>
                  <div className="flex items-center">
                    <input
                      id="pessoaFisica"
                      type="radio"
                      name="tipoPessoa"
                      value="Pessoa Física"
                      checked={tipoPessoa === "Pessoa Física"}
                      onChange={(e) => setTipoPessoa(e.target.value)}
                      className="form-radio h-4 w-4 text-blue-600"
                      required
                    />
                    <label htmlFor="pessoaFisica" className="ml-2 text-sm text-gray-700">Pessoa Física</label>
                  </div>
                </div>

                {/* Action Buttons */}
                <div className="flex justify-end space-x-4">
                  <button
                    type="button"
                    onClick={handleCancel}
                    className="border-2 border-blue-600 text-blue-600 hover:bg-blue-50 font-bold py-2 px-4 rounded-md flex items-center space-x-2 transition"
                  >
                    Cancelar
                  </button>
                  <button
                    type="submit"
                    disabled={isSaving}
                    className="bg-blue-600 hover:bg-blue-700 text-white font-bold py-2 px-6 rounded-md transition duration-150 ease-in-out disabled:opacity-50 disabled:cursor-not-allowed"
                  >
                    {isSaving ? "Cadastrando..." : "Cadastrar"}
                  </button>
                </div>
              </form>
            </div>
          </div>
        </div>
      </div>
    </DashboardLayout>
  );
  
} 
              