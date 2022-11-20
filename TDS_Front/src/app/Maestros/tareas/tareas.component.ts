import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Clase } from 'src/app/models/clase';
import { APIService } from 'src/app/services/api.service';

@Component({
  selector: 'app-tareas',
  templateUrl: './tareas.component.html',
  styleUrls: ['./tareas.component.css']
})
export class TareasComponent implements OnInit {

  clase: Clase = new Clase();

  constructor(private API: APIService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    let id = parseInt(this.route.snapshot.paramMap.get("id") || "0");
    if (id != 0){
      this.API.getClaseById(id).subscribe( clase => {
        clase.tareas.forEach( tarea => {
          let fecha = new Date(tarea.fechaEntrega)
          tarea.fechaEntrega = `${fecha.getDate()}/${fecha.getMonth()+1}/${fecha.getFullYear()}`;
        });
        this.clase = clase;
      })
    }
  }

}
