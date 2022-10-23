import { Component, OnInit } from '@angular/core';
import { Institucion } from 'src/app/models/institucion';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  institucion: Institucion = JSON.parse(localStorage.getItem("institucion")||"") as Institucion;

  constructor() { }

  ngOnInit(): void {
  }

}
