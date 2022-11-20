export class Tarea {
  constructor(public id:number = 0, public titulo:string = "", public descripcion: string = "", public fechaEntrega: string = "", public idClase: number = 0, public estado: boolean = false, public codigo: string = "", public calificacion: number = 0){}
}
