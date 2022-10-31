import { Estudiante } from "./estudiante";
import { Maestro } from "./maestro";

export class Usuario {
  constructor(public id: number = 0, public password: string = "", public estudianteId: number | null = 0, public maestroId: number | null = 0, public rolId: number = 0, public institucionId: number = 0, public estado: boolean = false, public estudiante: Estudiante | null = new Estudiante(), public maestro: Maestro | null = new Maestro()){}
}
