import { Component, OnInit } from '@angular/core';
import { Institucion } from 'src/app/models/institucion';
import { APIService } from 'src/app/services/api.service';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})
export class IndexComponent implements OnInit {

  institucion: Institucion = JSON.parse(localStorage.getItem("institucion")||"") as Institucion;
  licencias = 0;

  constructor(private API: APIService) { }

  ngOnInit(): void {
    this.API.getInstitucionByCodigo(this.institucion.codigo).subscribe(insti => {
      this.licencias = insti.licencias;
    })
  }

}
