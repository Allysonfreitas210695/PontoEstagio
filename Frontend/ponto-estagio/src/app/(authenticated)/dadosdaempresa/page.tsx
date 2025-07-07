"use client";
import React, { useState } from 'react';
import Image from 'next/image';
import logo from "../../../../public/assets/image/logo2.png"; // Assuming this is the correct path for the logo
import Sidebar from "../dashboard/Sidebar";
import DashboardLayout from "../dashboard/DashboardLayout";

export default function DadosEmpresaCompleto() {
  // State for Empresa Data
  const [nome, setNome] = useState("AltoTech");
  const [cnpjCpf, setCnpjCpf] = useState("24.529.265/0001-40");
  const [uf, setUf] = useState("RN");
  const [telefone, setTelefone] = useState("+55 (84) 98147-1097");
  const [email, setEmail] = useState("erikyabreu@alunos.ufersa.edu.br");
  const [cep, setCep] = useState("59900-000");
  const [logradouro, setLogradouro] = useState("Rua João Pessoa");
  const [numero, setNumero] = useState("09-A");
  const [bairro, setBairro] = useState("Centro");
  const [municipio, setMunicipio] = useState("Pau dos Ferros");
  const [registroProfissionalLiberal, setRegistroProfissionalLiberal] = useState("");
  const [tipoPessoa, setTipoPessoa] = useState("Pessoa Jurídica"); // Default to Pessoa Jurídica

  // Function to handle Cancel button click
  const handleCancel = () => {
    console.log("Operação de cancelamento acionada.");
   
    console.log("Formulário resetado ou navegação de volta.");
  };

  // Function to handle form submission
  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault(); // Prevent default form submission behavior

    const formData = {
      nomeEmpresa: nome, // Example: your API might expect 'nomeEmpresa' instead of 'nome'
      cnpjOuCpf: cnpjCpf, // Example: your API might expect 'cnpjOuCpf' instead of 'cnpjCpf'
      estadoUf: uf, // Example: your API might expect 'estadoUf' instead of 'uf'
      telefoneContato: telefone,
      emailContato: email,
      cepEndereco: cep,
      logradouroEndereco: logradouro,
      numeroEndereco: numero,
      bairroEndereco: bairro,
      municipioEndereco: municipio,
      registroProfissional: registroProfissionalLiberal,
      tipoPessoa: tipoPessoa, // Will send "Pessoa Jurídica" or "Pessoa Física"
    };

    console.log("Dados a serem enviados:", formData);

    try {
     
      const response = await fetch('/api/empresas', {
        method: 'POST', // Or 'PUT' if updating an existing entry
        headers: {
          'Content-Type': 'application/json',
         
        },
        body: JSON.stringify(formData),
      });

      if (response.ok) { // Check if the response status is 2xx (e.g., 200 OK, 201 Created)
        const result = await response.json(); // Parse the JSON response
        console.log("Dados cadastrados com sucesso:", result);
        // You might want to show a success message to the user here using a custom modal
        console.log("Dados cadastrados com sucesso! (Simulação de sucesso na API)");
        // Optional: Clear form or redirect
      } else {
        // If the response is not OK (e.g., 400 Bad Request, 500 Internal Server Error)
        const errorData = await response.json(); // Attempt to parse error details
        console.error("Erro ao cadastrar dados da empresa:", response.status, errorData);
        // Show a more informative error message to the user
        console.log(`Erro ao cadastrar dados: ${errorData.message || response.statusText || 'Erro desconhecido'}`);
      }
    } catch (error) {
      // Catch network errors or issues with the fetch operation itself
      console.error("Erro na requisição da API:", error);
      console.log("Ocorreu um erro ao tentar se conectar com o servidor.");
    }
  };

  return (
    <DashboardLayout>
      <div className="flex overflow-hidden">
        <Sidebar />
        <div className="flex-1 bg-[#FAF9F6] min-h-screen p-8 overflow-hidden"> 
          <div className="pt-1 px-6 mx-auto mt-12 w-full "> 
           {/* <div className="flex justify-center items-center gap-4 mb-10 max-w-lg mx-auto"> */}
           
            <div className="flex justify-center items-center mb-10 ">
              {/* Etapa 1 */}
              <div className="flex items-center">
                <div className="w-10 h-10 rounded-full bg-blue-600 text-white flex items-center justify-center font-bold text-sm">1</div>
                <div className="h-1 bg-blue-600 w-80"></div>
              </div>

              {/* Etapa 2 */}
              <div className="flex items-center">
                <div className="w-10 h-10 rounded-full bg-blue-600 text-white flex items-center justify-center font-bold text-sm">2</div>
                <div className="h-1 bg-gray-300 w-80"></div>
              </div>

              {/* Etapa 3 */}
              <div className="flex items-center">
                <div className="w-10 h-10 rounded-full bg-gray-300 text-white flex items-center justify-center font-bold text-sm">3</div>
              </div>
            </div>

            {/* DADOS DA EMPRESA Card (Main Form) */}
            <div className="bg-white rounded-lg shadow p-6 mb-8 max-w-7xl "> 
              <h2 className="font-bold text-gray-800 mb-2 text-xl">DADOS DA EMPRESA</h2>
              <p className="text-sm text-gray-600 mb-6">
                Informe os dados da empresa ou do profissional liberal concedente do estágio.
              </p>

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
                      <option value="RN">RN</option>
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
                    className="bg-blue-600 hover:bg-blue-700 text-white font-bold py-2 px-6 rounded-md transition duration-150 ease-in-out"
                  >
                    Cadastrar
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