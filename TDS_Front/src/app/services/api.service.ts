import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Clase } from '../models/clase';
import { Entrega } from '../models/entrega';
import { Estudiante } from '../models/estudiante';
import { Institucion } from '../models/institucion';
import { Maestro } from '../models/maestro';
import { Tarea } from '../models/tarea';
import { Usuario } from '../models/usuario';

@Injectable({
  providedIn: 'root'
})
export class APIService {

  constructor(private http: HttpClient) { }

  private getInstitucionId(){
    let institucion: Institucion = JSON.parse(localStorage.getItem("institucion")||"") as Institucion;
    return institucion.id;
  }

  private getMaestroId(){
    let user: Usuario = JSON.parse(localStorage.getItem("usuario")||"") as Usuario;
    return user.maestroId;
  }

  private getEstudianteId(){
    let user: Usuario = JSON.parse(localStorage.getItem("usuario")||"") as Usuario;
    return user.estudianteId;
  }

  public getInstitucionByCodigo(codigo: string){
    return this.http.get<Institucion>(`https://localhost:7082/Institucion/GetInstitucionByCodigo/${codigo.toLocaleLowerCase()}`)
  }

  public insertInstitucion(institucion: Institucion){
    return this.http.post<Institucion>("https://localhost:7082/Institucion/InsertInstitucion", institucion);
  }

  public agregarLicencias(cantidad: number){
    return this.http.put<Institucion>("https://localhost:7082/Institucion/AgregarLicencias", {id: this.getInstitucionId(), cantidad})
  }

  public getMaestros(){
    return this.http.get<Maestro[]>(`https://localhost:7082/Maestro/GetMaestros/${this.getInstitucionId()}`);
  }

  public getMaestroById(id: number){
    return this.http.get<Maestro>(`https://localhost:7082/Maestro/GetMaestroById/${this.getInstitucionId()}/${id}`);
  }

  public insertMaestro(maestro: Maestro){
    maestro.institucionId = this.getInstitucionId();
    return this.http.post<Estudiante>(`https://localhost:7082/Maestro/InsertMaestro`, maestro);
  }

  public updateMaestro(maestro: Maestro){
    maestro.institucionId = this.getInstitucionId();
    return this.http.put<Estudiante>(`https://localhost:7082/Maestro/UpdateMaestro`, maestro);
  }

  public deleteMaestro(id: number){
    return this.http.delete<Estudiante>(`https://localhost:7082/Maestro/DeleteMaestro/${this.getInstitucionId()}/${id}`);
  }

  public getEstudiantes(){
    return this.http.get<Estudiante[]>(`https://localhost:7082/Estudiantes/GetEstudiantes/${this.getInstitucionId()}`);
  }

  public getEstudianteById(id: number){
    return this.http.get<Estudiante>(`https://localhost:7082/Estudiantes/GetEstudianteById/${this.getInstitucionId()}/${id}`);
  }

  public insertEstudiante(estudiante: Estudiante){
    estudiante.institucionId = this.getInstitucionId();
    return this.http.post<Estudiante>(`https://localhost:7082/Estudiantes/InsertEstudiante`, estudiante);
  }

  public updateEstudiante(estudiante: Estudiante){
    estudiante.institucionId = this.getInstitucionId();
    return this.http.put<Estudiante>(`https://localhost:7082/Estudiantes/UpdateEstudiante`, estudiante);
  }

  public deleteEstudiante(id: number){
    return this.http.delete<Estudiante>(`https://localhost:7082/Estudiantes/DeleteEstudiante/${this.getInstitucionId()}/${id}`);
  }

  public getClases(){
    return this.http.get<Clase[]>(`https://localhost:7082/Clase/GetClases/${this.getInstitucionId()}`);
  }

  public getClaseById(id: number){
    return this.http.get<Clase>(`https://localhost:7082/Clase/GetClasesById/${this.getInstitucionId()}/${id}`);
  }

  public getClasesByMaestro(){
    return this.http.get<Clase[]>(`https://localhost:7082/Clase/GetClasesByMaestroId/${this.getInstitucionId()}/${this.getMaestroId()}`);
  }

  public getAcumulado(idClase: number){
    return this.http.get<{idClase: number, total: number, acumulado: number}>(`https://localhost:7082/Clase/GetAcumulado/${idClase}/${this.getEstudianteId()}`);
  }

  public insertClase(clase: Clase){
    clase.institucionId = this.getInstitucionId();
    return this.http.post<Clase>(`https://localhost:7082/Clase/InsertClase`, clase);
  }

  public updateClase(clase: Clase){
    clase.institucionId = this.getInstitucionId();
    return this.http.put<Clase>(`https://localhost:7082/Clase/UpdateClase`, clase);
  }

  public deleteClase(id: number){
    return this.http.delete<Clase>(`https://localhost:7082/Clase/DeleteClase/${this.getInstitucionId()}/${id}`);
  }

  public getTareas(idClase: number){
    return this.http.get<Tarea[]>(`https://localhost:7082/Tarea/GetTareas/${idClase}`);
  }

  public getTareaById(idClase: number, id: number){
    return this.http.get<Tarea>(`https://localhost:7082/Tarea/GetTareaById/${idClase}/${id}`);
  }

  public insertTarea(tarea: Tarea){
    return this.http.post<Tarea>(`https://localhost:7082/Tarea/InsertTarea`, tarea)
  }

  public updateTarea(tarea: Tarea){
    return this.http.put<Tarea>(`https://localhost:7082/Tarea/UpdateTarea`, tarea)
  }

  public deleteTarea(idClase: number, id: number){
    return this.http.delete<Tarea>(`https://localhost:7082/Tarea/DeleteTarea/${idClase}/${id}`)
  }

  public getEntregas(idTarea: number){
    return this.http.get<Entrega[]>(`https://localhost:7082/Entrega/GetEntregas/${idTarea}`)
  }

  public insertEntrega(entrega: Entrega){
    entrega.estudianteId = this.getEstudianteId();
    entrega.estado = true;
    entrega.tarea = null;
    entrega.estudiante = null;
    return this.http.post<Entrega>(`https://localhost:7082/Entrega/InsertEntrega`, entrega);
  }

  public updateEntrega(entrega: Entrega){
    return this,this.http.put<Entrega>(`https://localhost:7082/Entrega/UpdateEntrega`, entrega);
  }

  public login(correo: string, password: string){
    return this.http.get<Usuario>(`https://localhost:7082/Usuario/Login/${correo}/${password}`);
  }

  public getUsuarios(){
    return this.http.get<Usuario[]>(`https://localhost:7082/Usuario/GetUsuarios/${this.getInstitucionId()}`);
  }

  public getUsuarioById(id: number){
    return this.http.get<Usuario>(`https://localhost:7082/Usuario/GetUsuarioById/${this.getInstitucionId()}/${id}`);
  }

  public insertUsuario(usuario: Usuario){
    usuario.institucionId = this.getInstitucionId();
    usuario.estudiante = null;
    usuario.maestro = null;
    return this.http.post<Usuario>(`https://localhost:7082/Usuario/InsertUsuario`, usuario);
  }

  public updateUsuario(usuario: Usuario){
    usuario.institucionId = this.getInstitucionId();
    usuario.estudiante = null;
    usuario.maestro = null;
    return this.http.put<Usuario>(`https://localhost:7082/Usuario/UpdateUsuario`, usuario);
  }

  public deleteUsuario(id: number){
    return this.http.delete<Usuario>(`https://localhost:7082/Usuario/DeleteUsuario/${this.getInstitucionId()}/${id}`);
  }
}
