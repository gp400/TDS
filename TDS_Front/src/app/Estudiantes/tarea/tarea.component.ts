import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Entrega } from 'src/app/models/entrega';
import { Tarea } from 'src/app/models/tarea';
import { Usuario } from 'src/app/models/usuario';
import { APIService } from 'src/app/services/api.service';
import { doc, setDoc } from "firebase/firestore";
import { db } from "../../../firebase/provider";
import Swal from 'sweetalert2';
import { setTimeout } from 'timers';

@Component({
  selector: 'app-tarea',
  templateUrl: './tarea.component.html',
  styleUrls: ['./tarea.component.css']
})
export class TareaComponent implements OnInit {

  tareas: Tarea[] = [];
  titulo: string = "";
  descripcion: string = "";
  tareaId: number = 0;
  url: string = "";
  @ViewChild('cerrar') btnCerrar!: ElementRef;
  constructor(private API: APIService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    let id = parseInt(this.route.snapshot.paramMap.get("id") || "0");
    if (id == 0){
      let user: Usuario = JSON.parse(localStorage.getItem("usuario")||"") as Usuario;
      this.API.getEstudianteById(user.estudianteId || 0).subscribe( data => {
        const entregasId = data.entregas.map(e => e.tareaId);
        data.estudiantesClases.forEach(ec => {
          ec.clase.tareas.forEach(t => {
            if (!entregasId.includes(t.id)){
              let fecha = new Date(t.fechaEntrega)
              t.fechaEntrega = `${fecha.getDate()}/${fecha.getMonth()+1}/${fecha.getFullYear()}`;
              this.tareas.push(t);
            }
          })
        })
      });
    } else {
      this.API.getClaseById(id).subscribe(clase => {
        clase.tareas.forEach(t => {
          let fecha = new Date(t.fechaEntrega)
          t.fechaEntrega = `${fecha.getDate()}/${fecha.getMonth()+1}/${fecha.getFullYear()}`;
          this.tareas.push(t);
        })
      })
    }
  }

  getSortedList(){
    return this.tareas.sort((a, b) => (new Date(a.fechaEntrega) > new Date(b.fechaEntrega)) ? 1 : -1);
  }

  isValidDate(fecha: string){
    let [dia, mes, year] = fecha.split("/");
    return new Date() > new Date(`${mes}/${dia}/${year}`);
  }

  setModal(titulo: string, descripcion: string, tareaId: number){
    this.titulo = titulo;
    this.descripcion = descripcion;
    this.tareaId = tareaId;
  }

  onFileSelected(event: any){
    const file:File = event.target.files[0];
    if (file){
      var reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = (event: any) => {
        this.url = event.target.result;
      }
    }
  }

  onSubmit(){
    if (this.url == ""){
      Swal.fire(
        "Alerta",
        "Seleccione un archivo",
        "warning"
      )
    } else {
      let entrega = new Entrega();
      let user: Usuario = JSON.parse(localStorage.getItem("usuario")||"") as Usuario;
      let docu = `${user.estudiante?.nombres}_${user.estudiante?.apellidos}_${this.tareaId}`;
      entrega.documento = docu;
      entrega.tareaId = this.tareaId;
      setDoc(doc(db, "entregas", docu), {
        url: this.url
      });
      this.API.insertEntrega(entrega).subscribe(post => {
        Swal.fire(
          'Correcto',
          'La tarea fue entregada correctamente',
          'success'
        )
      }, error => {
        Swal.fire(
          'Error',
          error.error,
          'error'
        )
      })
      this.tareas = [];
      this.btnCerrar.nativeElement.click();
      this.API.getEstudianteById(user.estudianteId || 0).subscribe( data => {
        const entregasId = data.entregas.map(e => e.tareaId);
        data.estudiantesClases.forEach(ec => {
          ec.clase.tareas.forEach(t => {
            if (!entregasId.includes(t.id)){
              let fecha = new Date(t.fechaEntrega)
              t.fechaEntrega = `${fecha.getDate()}/${fecha.getMonth()+1}/${fecha.getFullYear()}`;
              this.tareas.push(t);
            }
          })
        })
      });
    }
  }
}
