import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Clase } from '../models/clase';
import { Estudiante } from '../models/estudiante';
import { Institucion } from '../models/institucion';
import { Maestro } from '../models/maestro';
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

  public getInstitucionByCodigo(codigo: string){
    return this.http.get<Institucion>(`https://localhost:7082/Institucion/GetInstitucionByCodigo/${codigo.toLocaleLowerCase()}`)
  }

  public insertInstitucion(institucion: Institucion){
    return this.http.post<Institucion>("https://localhost:7082/Institucion/InsertInstitucion", institucion);
  }

  public getMaestros(){
    return this.http.get<Maestro[]>(`https://localhost:7082/Maestro/GetMaestros/${this.getInstitucionId()}`);
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
