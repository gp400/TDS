import { Component, OnInit } from '@angular/core';
import { Maestro } from 'src/app/models/maestro';
import { APIService } from 'src/app/services/api.service';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})
export class IndexComponent implements OnInit {

  maestros: Maestro[] = [];

  constructor(private API: APIService) { }

  ngOnInit(): void {
    this.API.getMaestros().subscribe( maestros => {
      this.maestros = maestros;
    })
  }

  onDelete(id: number){}

}
