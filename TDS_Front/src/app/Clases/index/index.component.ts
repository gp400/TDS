import { Component, OnInit } from '@angular/core';
import { Clase } from 'src/app/models/clase';
import { APIService } from 'src/app/services/api.service';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})
export class IndexComponent implements OnInit {

  clases: Clase[] = [];

  constructor(private API: APIService) { }

  ngOnInit(): void {
    this.API.getClases().subscribe( clases => {
      this.clases = clases;
    })
  }

  onDelete(id: number){
    this.API.deleteClase(id).subscribe(usuario => {
      this.API.getClases().subscribe( clases => {
        this.clases = clases;
      })
    })
  }

}
