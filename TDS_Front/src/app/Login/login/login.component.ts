import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { APIService } from 'src/app/services/api.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  email: string = "";
  password: string = "";

  constructor(private API: APIService, private router: Router) { }

  ngOnInit(): void {
    localStorage.removeItem("institucion");
    localStorage.removeItem("usuario");
  }

  onSubmit(){
    this.API.login(this.email, this.password).subscribe(post => {
      localStorage.setItem("usuario", JSON.stringify(post));
      localStorage.setItem("institucion", JSON.stringify(post.institucion));
      let ruta = "";
      if (post.maestroId != null){
        ruta = "/maestroUser";
      } else {
        ruta = "/";
      }
      this.router.navigate([ruta])
    }, error => {
      Swal.fire(
        "Alerta",
        "Asegurese de ingresar las credenciales correctas",
        "warning"
      );
    })
  }

}
