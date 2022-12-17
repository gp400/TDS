import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Institucion } from 'src/app/models/institucion';
import { APIService } from 'src/app/services/api.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})
export class IndexComponent implements OnInit {

  institucion: Institucion = JSON.parse(localStorage.getItem("institucion")||"") as Institucion;
  licencias = 0;
  cantidad = 0;
  @ViewChild("btnCerrar") btnCerrar!: ElementRef;

  constructor(private API: APIService) { }

  ngOnInit(): void {
    this.API.getInstitucionByCodigo(this.institucion.codigo).subscribe(insti => {
      this.licencias = insti.licencias;
    })
  }

  pagar(){
    if (this.cantidad <= 0){
      Swal.fire(
        "Alerta",
        "La cantidad de licencias debe ser mayor a 0",
        "warning"
      )
    } else {
      this.btnCerrar.nativeElement.click();
      this.licencias += this.cantidad;
      this.API.agregarLicencias(this.cantidad).subscribe();
      this.cantidad = 0;
    }
  }
}
