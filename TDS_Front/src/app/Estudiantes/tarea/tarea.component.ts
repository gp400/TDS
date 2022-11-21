import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Tarea } from 'src/app/models/tarea';
import { Usuario } from 'src/app/models/usuario';
import { APIService } from 'src/app/services/api.service';

@Component({
  selector: 'app-tarea',
  templateUrl: './tarea.component.html',
  styleUrls: ['./tarea.component.css']
})
export class TareaComponent implements OnInit {

  tareas: Tarea[] = [];
  constructor(private API: APIService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    let id = parseInt(this.route.snapshot.paramMap.get("id") || "0");
    if (id == 0){
      let user: Usuario = JSON.parse(localStorage.getItem("usuario")||"") as Usuario;
      this.API.getEstudianteById(user.estudianteId || 0).subscribe( data => {
        data.estudiantesClases.forEach(ec => {
          ec.clase.tareas.forEach(t => {
            let fecha = new Date(t.fechaEntrega)
            t.fechaEntrega = `${fecha.getDate()}/${fecha.getMonth()+1}/${fecha.getFullYear()}`;
            this.tareas.push(t);
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

}
