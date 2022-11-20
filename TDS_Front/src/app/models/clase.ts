import { EstudiantesClases } from "./estudiantes-clases";
import { Maestro } from "./maestro";
import { Tarea } from "./tarea";

export class Clase {
  constructor(public id: number = 0, public nombre: string = "", public descripcion: string = "", public maestroId: number = 0, public institucionId: number = 0, public codigo: string = "", public estado: boolean = false, public horaInicio: string = "", public horaFin: string = "", public maestro: Maestro= new Maestro(), public estudiantesClases: EstudiantesClases[] = [], public tareas: Tarea[] = []){}
}
