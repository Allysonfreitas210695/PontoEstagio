import { api } from "./api";
import { UniversityDTO } from "@/types/university";

export async function getAllUniversities(): Promise<UniversityDTO[]> {
  try {
    const { data } = await api.get<UniversityDTO[]>("/university");
    return data;
  } catch (error) {
    throw error;
  }
}
