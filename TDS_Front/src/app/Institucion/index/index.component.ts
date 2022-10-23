import { Component, OnInit } from '@angular/core';
import { Institucion } from 'src/app/models/institucion';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})
export class IndexComponent implements OnInit {

  institucion: Institucion = JSON.parse(localStorage.getItem("institucion")||"") as Institucion;

  constructor() { }

  ngOnInit(): void {
  }

}
