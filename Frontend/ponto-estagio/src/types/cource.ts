import { UniversityDTO } from "./university";

export type courceDTO = {
    id: string;
    name: string;
    workloadHours: number;
    university: UniversityDTO; 
    createdAt: string;
};