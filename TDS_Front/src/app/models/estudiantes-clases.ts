import { Estudiante } from "./estudiante";

export class EstudiantesClases {
  constructor(public id: number = 0, public estudianteId: number = 0, public claseId: number = 0, public estudiante: Estudiante = new Estudiante()){}
}
