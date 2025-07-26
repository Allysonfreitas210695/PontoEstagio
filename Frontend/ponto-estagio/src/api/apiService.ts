// src/services/apiService.ts
// Define a URL base da API a partir das variáveis de ambiente
const API_BASE_URL = process.env.NEXT_PUBLIC_API_BASE_URL || "http://localhost:5019/api";

// Função genérica para lidar com requisições HTTP
async function request<T>(
  endpoint: string,
  method: string,
  body?: any,
  token?: string
): Promise<T> {
  const url = `${API_BASE_URL}${endpoint}`;
  const headers: HeadersInit = {
    'Content-Type': 'application/json',
  };

  if (token) {
    headers['Authorization'] = `Bearer ${token}`;
  }

  const config: RequestInit = {
    method,
    headers,
  };

  if (body) {
    config.body = JSON.stringify(body);
  }

  try {
    const response = await fetch(url, config);

    // Se a resposta não for OK, tenta ler o erro do corpo e lança
    if (!response.ok) {
      const errorData = await response.json().catch(() => ({ message: response.statusText }));
      throw new Error(errorData.message || `Erro na requisição: ${response.status}`);
    }

    // Retorna o JSON da resposta. Se não houver corpo (ex: 204 No Content), retorna um objeto vazio.
    const data = await response.json().catch(() => ({}));
    return data as T;
  } catch (error) {
    console.error(`Erro na API para ${method} ${endpoint}:`, error);
    throw error;
  }
}

// Funções específicas para cada tipo de requisição
export const apiService = {
  get: <T>(endpoint: string, token?: string) => request<T>(endpoint, 'GET', undefined, token),
  post: <T>(endpoint: string, body: any, token?: string) => request<T>(endpoint, 'POST', body, token),
  put: <T>(endpoint: string, body: any, token?: string) => request<T>(endpoint, 'PUT', body, token),
  patch: <T>(endpoint: string, body: any, token?: string) => request<T>(endpoint, 'PATCH', body, token),
  delete: <T>(endpoint: string, token?: string) => request<T>(endpoint, 'DELETE', undefined, token),
};

// --- Funções específicas para os seus endpoints ---
// Interfaces para os DTOs (Data Transfer Objects) - ajuste conforme seus tipos reais

// User DTOs
interface UserRegisterDTO { // Corresponds to RequestRegisterUserJson
  email: string;
  isActive: boolean;
  name: string;
  password?: string;
  type: string;
  universityId?: string;
  courseId?: string;
  phone?: string;
  registration?: string;
  verificationCode?: string;
  companyId?: string; // For updating user
  supervisorId?: string; // For updating user
}

interface ResponseCheckUserUserJson {
  // Ajuste as propriedades conforme a resposta real do CheckIfUserExists
  exists: boolean;
  message?: string;
}

interface RequestCheckUserExistsUserJson {
  email: string;
  password: string;
  type: string;
}

interface ResponseShortUserJson {
  id: string;
  name: string;
  email: string;
  type: string;
  isActive: boolean;
  createdAt: string;
}

interface UniversityDTO { // Corresponds to ResponseUniversityJson
  id: string;
  name: string;
  createdAt: string;
}
interface ResponseUserJson extends ResponseShortUserJson {
  registration?: string;
  phone?: string;
  university?: UniversityDTO;
  course?: CourseDTO;
  company?: CompanyResponseDTO;
  supervisor?: LegalRepresentativeResponseDTO;
  coordinator?: CoordinatorDTO;
  // Adicione outras propriedades específicas de um usuário completo
}


interface UserLoggedUserDTO { // Corresponds to ResponseLoggedUserJson
  id: string;
  email: string;
  name: string;
  token: string;
  refreshToken?: string; // Adicionado com base na resposta de login
  type?: string; // Adicionado com base na resposta de login
  // As propriedades aninhadas como company, supervisor, etc. viriam do getUserMe
  company?: CompanyResponseDTO;
  supervisor?: LegalRepresentativeResponseDTO;
  university?: UniversityDTO;
  course?: CourseDTO;
  coordinator?: CoordinatorDTO;
}

// Company DTOs
interface CompanyRegisterDTO { // Corresponds to RequestRegisterCompanyJson
  name: string;
  cnpj: string;
  email: string;
  phone: string;
  isActive: boolean;
  address: {
    street: string;
    number: string;
    district: string;
    city: string;
    state: string;
    zipCode: string;
    complement?: string;
  };
}

interface CompanyResponseDTO {
  cpf: string | undefined; // Corresponds to ResponseCompanyJson
  id: string;
  name: string;
  cnpj?: string;
  email?: string;
  phone?: string;
  isActive?: boolean;
  address?: {
    id: string;
    street: string;
    number: string;
    district: string;
    city: string;
    state: string;
    zipCode: string;
    complement?: string;
    createdAt: string;
  };
  createdAt?: string;
}

// Legal Representative DTOs
interface LegalRepresentativeRegisterDTO { // Corresponds to RequestRegisterLegalRepresentativeJson
  fullName: string;
  cpf: string;
  position: string;
  email: string;
  companyId: string;
}

interface LegalRepresentativeResponseDTO { // Corresponds to ResponseLegalRepresentativeJson
  id: string;
  fullName: string;
  cpf?: string;
  position?: string;
  email?: string;
  company?: CompanyResponseDTO;
  createdAt?: string;
}

// University DTOs
interface RequestRegisterUniversityJson {
  name: string;
}

interface ResponseUniversityJson {
  id: string;
  name: string;
  createdAt: string;
}

// Course DTOs
interface CourseDTO { // Corresponds to ResponseCourceJson
  id: string;
  name: string;
}

// Coordinator DTO (Assuming it's a User with type 'Coordinator')
interface CoordinatorDTO {
    id: string;
    name: string;
    email?: string;
    cpf?: string;
    // ... outras propriedades relevantes para um coordenador
}

// Project DTOs
interface ProjectRegisterDTO { // Corresponds to RequestRegisterProjectJson
  name: string;
  description: string;
  status: string;
  startDate: string;
  endDate: string;
  companyId: string;
  userId: string;
  legalRepresentativeId: string;
  weeklyHours?: number;
  scholarshipValue?: number;
  advisorUserId?: string;
}

interface ResponseShortProjectJson {
  id: string;
  name: string;
  description: string;
  status: string;
  startDate: string;
  endDate: string;
  createdAt: string;
}

interface ResponseProjectJson extends ResponseShortProjectJson {
  attendances?: any[]; // Tipo mais específico se souber a estrutura de attendances
  company?: CompanyResponseDTO;
  user?: ResponseUserJson; // Ou ResponseShortUserJson, dependendo do detalhe retornado
  legalRepresentative?: LegalRepresentativeResponseDTO;
  advisorUser?: CoordinatorDTO;
}

interface RequestAssignUserToProjectJson {
  userId: string;
  supervisorId: string;
}

interface RequestUpdateProjectStatusJson {
  status: string; // Ex: "Active", "Completed", etc.
}

// Report DTOs
interface ResponseReportMonthlyJson {
  reference: string;
  status: string;
  totalHours: number;
}


// Funções de API para os seus casos de uso
export const authApi = {
  // POST api/Auth/login
  login: async (credentials: { email: string; password: string }): Promise<UserLoggedUserDTO> => {
    return apiService.post<UserLoggedUserDTO>("/Auth/login", credentials);
  },

  // POST api/Users (Register) - CORRIGIDO: SEM 'request' ANINHADO
  registerUser: async (userData: UserRegisterDTO): Promise<UserLoggedUserDTO> => {
    return apiService.post<UserLoggedUserDTO>("/Users", userData);
  },

  // POST api/Users/check-user
  checkUser: async (checkData: RequestCheckUserExistsUserJson): Promise<ResponseCheckUserUserJson> => {
    return apiService.post<ResponseCheckUserUserJson>("/Users/check-user", checkData);
  },

  // GET api/Users (GetAllUsers)
  getAllUsers: async (token: string): Promise<ResponseShortUserJson[]> => {
    return apiService.get<ResponseShortUserJson[]>("/Users", token);
  },

  // GET api/Users/{id} (GetUserById)
  getUserById: async (id: string, token: string): Promise<ResponseUserJson> => {
    return apiService.get<ResponseUserJson>(`/Users/${id}`, token);
  },

  // GET api/Users/me (GetCurrentUser - assumed from previous conversation)
  getUserMe: async (token: string): Promise<UserLoggedUserDTO> => {
    return apiService.get<UserLoggedUserDTO>("/users/me", token); // Assumindo que /users/me ainda funciona
  },

  // PUT api/Users/{id} (Update) - CORRIGIDO: SEM 'request' ANINHADO
  updateUser: async (id: string, userData: Partial<UserRegisterDTO>, token: string): Promise<void> => {
    // Retorna 204 No Content
    await apiService.put<void>(`/Users/${id}`, userData, token);
  },

  // PATCH api/Users/{id}/activate
  activateUser: async (id: string, token: string): Promise<void> => {
    await apiService.patch<void>(`/Users/${id}/activate`, {}, token);
  },

  // PATCH api/Users/{id}/deactiveted
  deactivateUser: async (id: string, token: string): Promise<void> => {
    await apiService.patch<void>(`/Users/${id}/deactiveted`, {}, token);
  },
};

export const companyApi = {
  // Função para cadastrar uma nova empresa (POST /company)
  createCompany: async (companyData: CompanyRegisterDTO, token: string): Promise<CompanyResponseDTO> => {
    return apiService.post<CompanyResponseDTO>("/Company", { request: companyData }, token);
  },

  // Função para buscar universidades (GET /university) - DEPRECATED, MOVED TO universityApi
  // getUniversities: async (token: string): Promise<UniversityDTO[]> => {
  //   return apiService.get<UniversityDTO[]>("/university", token);
  // },
};

export const courseApi = {
  // Função para buscar cursos (GET /cource)
  getCourses: async (token: string): Promise<CourseDTO[]> => {
    return apiService.get<CourseDTO[]>("/Cource", token); // Cource com 'C' maiúsculo
  },
};

export const legalRepresentativeApi = {
  // Função para cadastrar um representante legal (supervisor) (POST /legalrepresentative)
  createLegalRepresentative: async (supervisorData: LegalRepresentativeRegisterDTO, token: string): Promise<LegalRepresentativeResponseDTO> => {
    return apiService.post<LegalRepresentativeResponseDTO>("/LegalRepresentative", supervisorData, token); // LegalRepresentative com 'L' e 'R' maiúsculos
  },
};

export const projectApi = {
  // GET api/Project
  getAllProjects: async (token: string): Promise<ResponseShortProjectJson[]> => {
    return apiService.get<ResponseShortProjectJson[]>("/Project", token);
  },

  // GET api/Project/{id}
  getProjectById: async (id: string, token: string): Promise<ResponseProjectJson> => {
    return apiService.get<ResponseProjectJson>(`/Project/${id}`, token);
  },

  // GET api/Project/GetCurrentProjectForUser (Admin role)
  getCurrentProjectForUser: async (token: string): Promise<ResponseProjectJson> => {
    return apiService.get<ResponseProjectJson>("/Project/GetCurrentProjectForUser", token);
  },

  // POST api/Project (Admin role)
  createProject: async (projectData: ProjectRegisterDTO, token: string): Promise<ResponseShortProjectJson> => {
    return apiService.post<ResponseShortProjectJson>("/Project", projectData, token);
  },

  // PUT api/Project/{id} (Admin role)
  updateProject: async (id: string, projectData: ProjectRegisterDTO, token: string): Promise<void> => {
    await apiService.put<void>(`/Project/${id}`, projectData, token);
  },

  // POST api/Project/{projectId}/users (Admin role)
  assignUserToProject: async (projectId: string, assignData: RequestAssignUserToProjectJson, token: string): Promise<void> => {
    await apiService.post<void>(`/Project/${projectId}/users`, assignData, token);
  },

  // PATCH api/Project/{id}/status
  updateProjectStatus: async (id: string, statusData: RequestUpdateProjectStatusJson, token: string): Promise<void> => {
    await apiService.patch<void>(`/Project/${id}/status`, statusData, token);
  },

  // DELETE api/Project/{projectId}/intern/{Intern_Id}/supervisor/{Supervisor_Id} (Admin role)
  removeUserFromProject: async (projectId: string, internId: string, supervisorId: string, token: string): Promise<void> => {
    await apiService.delete<void>(`/Project/${projectId}/intern/${internId}/supervisor/${supervisorId}`, token);
  },
};

export const reportApi = {
  // GET api/Report
  generateMonthlyReport: async (periodo: string, token: string): Promise<ResponseReportMonthlyJson[]> => {
    return apiService.get<ResponseReportMonthlyJson[]>(`/Report?periodo=${periodo}`, token);
  },
};

// NOVO: API para Universidades
export const universityApi = {
  // GET api/University
  getAllUniversities: async (token: string): Promise<ResponseUniversityJson[]> => {
    return apiService.get<ResponseUniversityJson[]>("/University", token);
  },

  // GET api/University/{id}
  getUniversityById: async (id: string, token: string): Promise<ResponseUniversityJson> => {
    return apiService.get<ResponseUniversityJson>(`/University/${id}`, token);
  },

  // POST api/University (Admin role)
  createUniversity: async (universityData: RequestRegisterUniversityJson, token: string): Promise<ResponseUniversityJson> => {
    return apiService.post<ResponseUniversityJson>("/University", universityData, token);
  },

  // PUT api/University/{id} (Admin role)
  updateUniversity: async (id: string, universityData: RequestRegisterUniversityJson, token: string): Promise<void> => {
    await apiService.put<void>(`/University/${id}`, universityData, token);
  },
};


// Exportar interfaces para uso nos componentes
export type {
  UserRegisterDTO,
  UserLoggedUserDTO,
  CompanyRegisterDTO,
  CompanyResponseDTO,
  LegalRepresentativeRegisterDTO,
  LegalRepresentativeResponseDTO,
  UniversityDTO,
  CourseDTO,
  CoordinatorDTO,
  ProjectRegisterDTO,
  ResponseShortProjectJson,
  ResponseProjectJson,
  RequestAssignUserToProjectJson,
  RequestUpdateProjectStatusJson,
  ResponseReportMonthlyJson,
  RequestRegisterUniversityJson,
  ResponseUniversityJson,
  ResponseShortUserJson,
  ResponseUserJson,
  RequestCheckUserExistsUserJson,
  ResponseCheckUserUserJson,
};
