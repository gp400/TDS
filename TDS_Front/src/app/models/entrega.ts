export class Entrega {
  constructor(public id: number = 0, public documento: string = "", public tareaId: number = 0, public estudianteId: number | null = 0, public estado: boolean = false, public calificacion: number = 0){}
}
