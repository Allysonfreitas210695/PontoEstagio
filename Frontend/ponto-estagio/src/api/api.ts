import axios from "axios";

// Configuração base do axios com tratamento de erros melhorado
export const api = axios.create({
  baseURL: 'https://localhost:5019/',
  headers: {
    'Content-Type': 'application/json'
  }
});

// Adicionando interceptors para tratamento global de erros
api.interceptors.response.use(
  response => response,
  error => {
    if (error.response) {
      // Erros 4xx/5xx
      const message = error.response.data?.message || 
                     error.response.data?.error || 
                     'Erro desconhecido no servidor';
      return Promise.reject(new Error(message));
    } else if (error.request) {
      // Request foi feito mas não houve resposta
      return Promise.reject(new Error('Sem resposta do servidor'));
    } else {
      // Erro ao configurar a requisição
      return Promise.reject(new Error('Erro ao configurar a requisição'));
    }
  }
);

export interface AlunoData {
  name: string;
  email: string;
  registration: string;
  password: string;
  phone: string;
  universityId: string;
  courseId: string;
  isActive: boolean;
  type: string;
}

export class RegisterAluno {
  static async register(alunoData: AlunoData) {
    try {
      const response = await api.post("/api/users", alunoData);
      return response.data;
    } catch (error) {
      // O interceptor já tratou o erro, só precisamos propagá-lo
      throw error;
    }
  }
}