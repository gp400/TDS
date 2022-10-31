import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Usuario } from 'src/app/models/usuario';
import { APIService } from 'src/app/services/api.service';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})
export class IndexComponent implements OnInit {

  usuarios!: Usuario[];

  constructor(private API: APIService, private router: Router) { }

  ngOnInit(): void {
    this.API.getUsuarios().subscribe(usuarios => {
      this.usuarios = usuarios;
    })
  }

  onDelete(id: number){
    this.API.deleteUsuario(id).subscribe(usuario => {
      this.API.getUsuarios().subscribe(users => {
        this.usuarios = users;
      })
    })
  }
}
