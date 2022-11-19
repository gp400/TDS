import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Clase } from 'src/app/models/clase';
import { Estudiante } from 'src/app/models/estudiante';
import { EstudiantesClases } from 'src/app/models/estudiantes-clases';
import { APIService } from 'src/app/services/api.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.css']
})
export class FormComponent implements OnInit {

  estudiante: Estudiante = new Estudiante();
  errores: string[] = [];
  clases: Clase[] = [];
  idClase: number = 0;
  title = "Ingrese un estudiante";

  constructor(private API: APIService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.API.getClases().subscribe(clases => {
      this.clases = clases;
    });

    let id = parseInt(this.route.snapshot.paramMap.get("id") || "0");
    if (id != 0){
      this.title = "Edite un estudiante";
      this.API.getEstudianteById(id).subscribe( student => {
        this.estudiante = student;
      })
    }
  }

  clasesValidas(){
    let ids: number[] = this.estudiante.estudiantesClases.map(ec => ec.claseId);
    return this.clases.filter( clase => {
      return !ids.includes(clase.id);
    })
  }

  agregarClase(){
    if (this.idClase != 0){
      this.estudiante.estudiantesClases.push(new EstudiantesClases (0, 0, parseInt(this.idClase.toString())));
      this.idClase = 0;
    }
  }

  eliminarClase(id: number){
    this.estudiante.estudiantesClases = this.estudiante.estudiantesClases.filter(clase => {
      return id != clase.claseId;
    })
  }

  getClase(id: number){
    let clase = this.clases.filter( clase => {
      return id == clase.id;
    })[0];
    return clase;
  }

  onSubmit(){
    this.errores = []
    if (this.estudiante.nombres.trim().length == 0){
      this.errores.push("Ingrese sus nombre(s)")
    }
    if (this.estudiante.apellidos.trim().length == 0){
      this.errores.push("Ingrese sus apellido(s)")
    }
    if (this.estudiante.correo.trim().length == 0){
      this.errores.push("Ingrese su correo")
    }
    if (this.estudiante.codigo.trim().length == 0){
      this.errores.push("Ingrese su codigo")
    }
    if (this.errores.length == 0){
      if(this.estudiante.id == 0){
        this.API.insertEstudiante(this.estudiante).subscribe(post => {
          Swal.fire(
            'Correcto',
            'El estudiante fue creado correctamente',
            'success'
          )
          this.estudiante = new Estudiante();
        }, error => {
          Swal.fire(
            'Error',
            error.error,
            'error'
          )
        })
      } else {
        this.API.updateEstudiante(this.estudiante).subscribe(put => {
          Swal.fire(
            'Correcto',
            'El estudiante fue editado correctamente',
            'success'
          )
          this.estudiante = new Estudiante();
        }, error => {
          Swal.fire(
            'Error',
            error.error,
            'error'
          )
        })
      }
    } else {
      window.scrollTo(0, 0);
    }
  }

}
