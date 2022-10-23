import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Institucion } from '../models/institucion';

@Injectable({
  providedIn: 'root'
})
export class APIService {

  constructor(private http: HttpClient) { }

  private convertToRequestInstitucion(institucion: Institucion){
    let objeto = {
      Id: 0,
      Nombre: institucion.nombre,
      Descripcion: institucion.descripcion,
      Codigo: institucion.codigo,
      Direccion: institucion.direccion,
      Correo: institucion.correo,
      Telefono: institucion.telefono,
      Licencias: institucion.licencias,
      Estado: true
    }
    return objeto;
  }

  public getInstitucionByCodigo(codigo: string){
    return this.http.get<Institucion>(`https://localhost:7082/Institucion/GetInstitucionByCodigo/${codigo.toLocaleLowerCase()}`)
  }

  public insertInstitucion(institucion: Institucion){
    return this.http.post<Institucion>("https://localhost:7082/Institucion/InsertInstitucion", this.convertToRequestInstitucion(institucion));
  }
}
