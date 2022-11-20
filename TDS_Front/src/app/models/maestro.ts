import { Clase } from "./clase";

export class Maestro {
  constructor(public id: number = 0, public nombres: string = "", public apellidos: string = "", public correo: string = "", public telefono: string = "", public direccion: string = "", public institucionId: number = 0, public estado: boolean = false, public codigo: string = "", public clases: Clase[] = []){}
}
