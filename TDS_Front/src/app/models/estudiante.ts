import { Entrega } from "./entrega";
import { EstudiantesClases } from "./estudiantes-clases";

export class Estudiante {
  constructor(public id: number = 0, public nombres: string = "", public apellidos: string = "", public correo: string = "", public codigo: string = "", public direccion: string = "", public telefono: string = "", public estado: boolean = false, public institucionId: number = 0, public estudiantesClases: EstudiantesClases[] = [], public entregas: Entrega[] = []){}
}
