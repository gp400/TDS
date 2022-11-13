import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Estudiante } from 'src/app/models/estudiante';
import { Maestro } from 'src/app/models/maestro';
import { Usuario } from 'src/app/models/usuario';
import { APIService } from 'src/app/services/api.service';
import Swal from 'sweetalert2';
import { NavbarComponent } from '../../Institucion/navbar/navbar.component';

@Component({
  selector: 'app-registro',
  templateUrl: './registro.component.html',
  styleUrls: ['./registro.component.css'],
})
export class RegistroComponent implements OnInit {

  usuario: Usuario = new Usuario();
  radio = "maestro";
  errores: string[] = [];
  maestros: Maestro[] = [];
  estudiantes: Estudiante[] = [];
  title = "Registre un usuario";
  btnText = "Registrar";

  constructor(private API: APIService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.API.getMaestros().subscribe(datos => {
      this.maestros = datos;
    });

    this.API.getEstudiantes().subscribe(datos => {
      this.estudiantes = datos;
    })

    let id = parseInt(this.route.snapshot.paramMap.get("id") || "0");
    if (id != 0){
      this.title = "Edite un usuario";
      this.btnText = "Editar";
      this.API.getUsuarioById(id).subscribe(user => {
        this.usuario = user;
        this.usuario.password = "";
        this.radio = user.maestroId == null ? "estudiante" : "maestro";
      })
    }
  }

  onSubmit(){
    this.errores = []
    if (this.usuario.password.trim().length == 0){
      this.errores.push("Ingrese una contraseña")
    }
    if (this.radio == "estudiante" && (this.usuario.estudianteId == 0 || this.usuario.estudianteId == null)){
      this.errores.push("Seleccione un estudiante")
    }
    if (this.radio == "maestro" && (this.usuario.maestroId == 0 || this.usuario.maestroId == null)){
      this.errores.push("Seleccione un maestro")
    }
    if (this.errores.length == 0){
      this.usuario.estudianteId = (this.radio == "estudiante") ? this.usuario.estudianteId : null;
      this.usuario.maestroId = (this.radio == "maestro") ? this.usuario.maestroId : null;
      this.usuario.rolId = (this.radio == "maestro") ? 2 : 3;
      if(this.usuario.id == 0){
        this.API.insertUsuario(this.usuario).subscribe(post => {
          Swal.fire(
            'Correcto',
            'El usuario fue registrado correctamente',
            'success'
          )
          this.usuario = new Usuario();
          this.radio = "maestro";
        }, error => {
          Swal.fire(
            'Error',
            'Ocurrio un error creando el usuario',
            'error'
          )
        })
      } else {
        this.API.updateUsuario(this.usuario).subscribe(put => {
          Swal.fire(
            'Correcto',
            'El usuario fue editado correctamente',
            'success'
          )
          this.usuario = new Usuario();
          this.radio = "maestro";
        }, error => {
          Swal.fire(
            'Error',
            'Ocurrio un error editando el usuario',
            'error'
          )
        })
      }
    } else {
      window.scrollTo(0, 0);
    }
  }

}
