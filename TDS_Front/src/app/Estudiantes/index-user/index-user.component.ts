import { Component, OnInit } from '@angular/core';
import { Estudiante } from 'src/app/models/estudiante';
import { Usuario } from 'src/app/models/usuario';
import { APIService } from 'src/app/services/api.service';

@Component({
  selector: 'app-index-user',
  templateUrl: './index-user.component.html',
  styleUrls: ['./index-user.component.css']
})
export class IndexUserComponent implements OnInit {

  estudiante: Estudiante = new Estudiante();
  acumulados: {idClase: number, total: number, acumulado: number}[] = [];
  constructor(private API: APIService) { }

  ngOnInit(): void {
    let user: Usuario = JSON.parse(localStorage.getItem("usuario")||"") as Usuario;
    this.API.getEstudianteById(user.estudianteId || 0).subscribe( estudiante => {
      estudiante.estudiantesClases.forEach(ec => {
        this.API.getAcumulado(ec.claseId).subscribe(valores => {
          this.acumulados.push(valores);
        });
      })
      this.estudiante = estudiante;
    });
  }

  getAcumulado(idClase: number){
    return this.acumulados.filter(a => a.idClase == idClase)[0];
  }
}
