import { checkUserDTO, userLoggedUserDTO, userRegisterDTO } from "@/types/user";
import { api } from "./api";
import { LoginFormData } from "@/app/(unauthenticated)/login/page";

export async function registerUser({
  email,
  isActive,
  name,
  password,
  type,
  universityId,
  courseId,
  phone,
  registration,
  verificationCode,
}: userRegisterDTO): Promise<userLoggedUserDTO> {
  try {
    const { data } = await api.post<userLoggedUserDTO>("/users", {
      email,
      isActive,
      name,
      password,
      type,
      universityId,
      courseId,
      phone,
      registration,
      verificationCode,
    });
    return data;
  } catch (error) {
    throw error;
  }
}

export async function login({
  email,
  senha,
}: LoginFormData): Promise<userLoggedUserDTO> {
  try {
    const { data } = await api.post<userLoggedUserDTO>("/auth/login", {
      email,
      password: senha,
    });

    return data;
  } catch (error) {
    throw error;
  }
}

export async function CheckUser({
  email,
  password,
  type,
}: checkUserDTO): Promise<checkUserDTO> {
  try {
    const { data } = await api.post<checkUserDTO>("/users/check-user", {
      email,
      password,
      type,
    });
    return data;
  } catch (error) {
    throw error;
  }
}
