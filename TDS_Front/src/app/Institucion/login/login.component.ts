import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Institucion } from 'src/app/models/institucion';
import { APIService } from 'src/app/services/api.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  institucion: Institucion = new Institucion();
  errores: string[] = [];

  constructor(private API: APIService, private router: Router) { }

  ngOnInit(): void {
    localStorage.removeItem("institucion");
    localStorage.removeItem("usuario");
  }

  onSubmit(){
    this.errores = []
    if (this.institucion.codigo.trim().length == 0){
      this.errores.push("Ingrese un codigo");
      return;
    }
    this.API.getInstitucionByCodigo(this.institucion.codigo).subscribe(post => {
      localStorage.setItem("institucion", JSON.stringify(post));
      this.router.navigate(["institucion"])
    }, error => {
      Swal.fire(
        'Alerta',
        `No existe una institucion con codigo ${this.institucion.codigo}`,
        'warning'
      )
    })
  }

}
