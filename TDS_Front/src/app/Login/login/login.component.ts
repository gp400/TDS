import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Usuario } from 'src/app/models/usuario';
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
    localStorage.removeItem("usuario");
  }

  onSubmit(){
    this.API.login(this.email, this.password).subscribe(post => {
      localStorage.setItem("usuario", JSON.stringify(post))
    }, error => {
      Swal.fire(
        "Alerta",
        "Asegurese de ingresar las credenciales correctas",
        "warning"
      );
    })
  }

}
