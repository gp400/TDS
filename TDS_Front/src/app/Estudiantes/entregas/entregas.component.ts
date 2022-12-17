import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Entrega } from 'src/app/models/entrega';
import { Usuario } from 'src/app/models/usuario';
import { APIService } from 'src/app/services/api.service';

@Component({
  selector: 'app-entregas',
  templateUrl: './entregas.component.html',
  styleUrls: ['./entregas.component.css']
})
export class EntregasComponent implements OnInit {

  entregas: Entrega[] = [];

  constructor(private API: APIService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    let user: Usuario = JSON.parse(localStorage.getItem("usuario")||"") as Usuario;
    this.API.getEstudianteById(user.estudianteId || 0).subscribe( data => {
      this.entregas = data.entregas;
    });
  }

}
