import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Maestro } from 'src/app/models/maestro';
import { APIService } from 'src/app/services/api.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.css']
})
export class FormComponent implements OnInit {

  maestro: Maestro = new Maestro();
  errores: string[] = [];
  title = "Ingrese un maestro";

  constructor(private API: APIService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    let id = parseInt(this.route.snapshot.paramMap.get("id") || "0");
    if (id != 0){
      this.title = "Edite un maestro";
      this.API.getMaestroById(id).subscribe( teacher => {
        this.maestro = teacher;
      })
    }
  }

  onSubmit(){
    this.errores = [];
    if (this.maestro.nombres.trim().length == 0){
      this.errores.push("Ingrese sus nombre(s)")
    }
    if (this.maestro.apellidos.trim().length == 0){
      this.errores.push("Ingrese sus apellido(s)")
    }
    if (this.maestro.correo.trim().length == 0){
      this.errores.push("Ingrese sus correo")
    }
    if (this.maestro.telefono.trim().length == 0){
      this.errores.push("Ingrese sus telefono")
    }
    if (this.maestro.direccion.trim().length == 0){
      this.errores.push("Ingrese sus direccion")
    }
    if (this.maestro.codigo.trim().length == 0){
      this.errores.push("Ingrese sus codigo")
    }
    if (this.errores.length == 0){
      if(this.maestro.id == 0){
        this.API.insertMaestro(this.maestro).subscribe(post => {
          Swal.fire(
            'Correcto',
            'El maestro fue creado correctamente',
            'success'
          )
          this.maestro = new Maestro();
        }, error => {
          Swal.fire(
            'Error',
            error.error,
            'error'
          )
        })
      } else {
        this.API.updateMaestro(this.maestro).subscribe(put => {
          Swal.fire(
            'Correcto',
            'El maestro fue editado correctamente',
            'success'
          )
          this.maestro = new Maestro();
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
