import axios from "axios";

export const api = axios.create({
  baseURL: process.env.NEXT_PUBLIC_API_URL,
  headers: {
    "Content-Type": "application/json",
  },
});

api.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response) {
      const message =
        error.response.data?.message ||
        error.response.data?.error ||
        `Erro ${error.response.status}: ${error.response.statusText}` ||
        "Erro desconhecido no servidor";

      return Promise.reject(new Error(message));
    }

    if (error.request) {
      return Promise.reject(
        new Error("Não foi possível conectar ao servidor.")
      );
    }
    return Promise.reject(new Error("Erro ao configurar a requisição."));
  }
);
