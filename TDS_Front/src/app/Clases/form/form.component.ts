import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Clase } from 'src/app/models/clase';
import { Maestro } from 'src/app/models/maestro';
import { APIService } from 'src/app/services/api.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.css']
})
export class FormComponent implements OnInit {

  clase: Clase = new Clase();
  errores: string[] = [];
  maestros: Maestro[] = [];
  title = "Ingrese una clase";

  constructor(private API: APIService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.API.getMaestros().subscribe( maestros => {
      this.maestros = maestros;
    });

    let id = parseInt(this.route.snapshot.paramMap.get("id") || "0");
    if (id != 0){
      this.title = "Edite una clase";
      this.API.getClaseById(id).subscribe( clase => {
        this.clase = clase;
      })
    }
  }

  onSubmit(){
    this.errores = []
    if (this.clase.nombre.trim().length == 0){
      this.errores.push("Ingrese un nombre");
    }
    if (this.clase.maestroId == 0){
      this.errores.push("Seleccione un maestro");
    }
    if (this.clase.codigo.trim().length == 0){
      this.errores.push("Ingrese un codigo");
    }
    if (this.clase.horaInicio.trim().length == 0){
      this.errores.push("Ingrese una hora de inicio");
    }
    if (this.clase.horaFin.trim().length == 0){
      this.errores.push("Ingrese una hora de fin");
    }
    if (this.errores.length == 0){
      if(this.clase.id == 0){
        this.API.insertClase(this.clase).subscribe(post => {
          Swal.fire(
            'Correcto',
            'La clase fue creada correctamente',
            'success'
          )
          this.clase = new Clase();
        }, error => {
          Swal.fire(
            'Error',
            error.error,
            'error'
          )
        })
      } else {
        this.API.updateClase(this.clase).subscribe(put => {
          Swal.fire(
            'Correcto',
            'La clase fue editada correctamente',
            'success'
          )
          this.clase = new Clase();
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
