import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Clase } from 'src/app/models/clase';
import { Tarea } from 'src/app/models/tarea';
import { APIService } from 'src/app/services/api.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-tareas',
  templateUrl: './tareas.component.html',
  styleUrls: ['./tareas.component.css']
})
export class TareasComponent implements OnInit {

  clase: Clase = new Clase();
  tarea: Tarea = new Tarea();
  title: string = ""
  @ViewChild("btnCerrar") btnCerrar!: ElementRef;

  constructor(private API: APIService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    let id = parseInt(this.route.snapshot.paramMap.get("id") || "0");
    if (id != 0){
      this.API.getClaseById(id).subscribe( clase => {
        this.getClase(id)
        this.tarea.idClase = clase.id;
        this.clase = clase;
      })
    }
  }

  getClase(id: number){
    this.API.getClaseById(id).subscribe( clase => {
      clase.tareas.forEach( tarea => {
        let fecha = new Date(tarea.fechaEntrega)
        tarea.fechaEntrega = `${fecha.getDate()}/${fecha.getMonth()+1}/${fecha.getFullYear()}`;
      });
      this.tarea.idClase = clase.id;
      this.clase = clase;
    })
  }

  setTarea(id: number){
    if (id == 0){
      this.tarea = new Tarea();
      this.title = "Agregar Tarea"
    } else {
      this.API.getTareaById(this.clase.id, id).subscribe(tarea => {
        this.title = "Editar Tarea"
        let fecha = new Date(tarea.fechaEntrega)
        tarea.fechaEntrega = `${fecha.getFullYear()}-${(fecha.getMonth()+1).toString().length == 1 ? "0"+(fecha.getMonth()+1).toString(): (fecha.getMonth()+1).toString()}-${fecha.getDate().toString().length == 1 ? "0"+fecha.getDate().toString() : fecha.getDate().toString()}`;
        this.tarea = tarea;
      })
    }
  }

  onDelete(id: number){
    this.API.deleteTarea(this.clase.id, id).subscribe( data => {
      setTimeout(() => {
        this.getClase(this.clase.id)
      }, 300);
    })
  }

  onSubmit(){
    if (this.tarea.titulo.trim().length == 0 || this.tarea.descripcion.trim().length == 0
    ||  this.tarea.fechaEntrega.trim().length == 0 || this.tarea.codigo.trim().length == 0){
      Swal.fire({
        title: "Alerta",
        text: 'Todos los campos son requeridos',
        icon: 'warning'
      })
    } else {
      this.tarea.idClase = this.clase.id;
      if (this.tarea.id == 0){
        this.API.insertTarea(this.tarea).subscribe( post => {
          setTimeout(() => {
            this.getClase(this.clase.id)
          }, 300);
          this.btnCerrar.nativeElement.click();
        }, error => {
          Swal.fire({
            title: "Alerta",
            text: error.error,
            icon: 'warning'
          })
        })
      } else {
        this.API.updateTarea(this.tarea).subscribe( put => {
          setTimeout(() => {
            this.getClase(parseInt(this.route.snapshot.paramMap.get("id") || "0"))
          }, 500);
          this.btnCerrar.nativeElement.click();
        }, error => {
          Swal.fire({
            title: "Alerta",
            text: error.error,
            icon: 'warning'
          })
        })
      }
    }
  }
}
