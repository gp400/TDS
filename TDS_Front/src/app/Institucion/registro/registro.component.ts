import { Component, OnInit, ViewChild } from '@angular/core';
import { catchError } from 'rxjs/operators';
import { Institucion } from 'src/app/models/institucion';
import { APIService } from 'src/app/services/api.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-registro',
  templateUrl: './registro.component.html',
  styleUrls: ['./registro.component.css']
})
export class RegistroComponent implements OnInit {

  institucion: Institucion = new Institucion();
  errores: string[] = [];

  constructor(private API: APIService) { }

  ngOnInit(): void {
    localStorage.removeItem("institucion");
  }

  onSubmit(){
    this.errores = [];
    if (this.institucion.nombre === ""){
      this.errores.push("El campo Nombre es requerido")
    }

    if (this.institucion.codigo === ""){
      this.errores.push("El campo Codigo es requerido")
    }

    if (this.errores.length == 0){
      this.API.insertInstitucion(this.institucion).subscribe(post => {
        if (post){
          Swal.fire(
            'Correcto',
            'La institucion fue registrada correctamente',
            'success'
          )
        }
      }, error => {
        Swal.fire(
          'Alerta',
          'Ya existe una institucion con ese codigo',
          'warning'
        )
      })
    }
  }
}
