export type userTypeDTO =
  | "Intern"
  | "Supervisor"
  | "Coordinator"
  | "Admin"
  | "Coordinator"
  | "Advisor";

export type userRegisterDTO = {
  name: string;
  email: string;
  registration?: string;
  password: string;
  phone?: string;
  universityId: string;
  courseId?: string;
  isActive: boolean;
  verificationCode?: string;
  type: userTypeDTO;
};

export type userLoggedUserDTO = {
  name: string;
  type: string;
  accessToken: string;
  refreshToken: string;
};

export type checkUserDTO = {
  type: string;
  email: string;
  password: string;
};
