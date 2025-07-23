import { courceDTO } from "@/types/cource";
import { api } from "./api";

export async function getAllCources(): Promise<courceDTO[]> {
  try {
    const { data } = await api.get<courceDTO[]>("/cource");
    return data;
  } catch (error) {
    throw error;
  }
}
