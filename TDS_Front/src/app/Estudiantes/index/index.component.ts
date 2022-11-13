import { Component, OnInit } from '@angular/core';
import { Estudiante } from 'src/app/models/estudiante';
import { APIService } from 'src/app/services/api.service';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})
export class IndexComponent implements OnInit {

  estudiantes: Estudiante[] = [];

  constructor(private API: APIService) {
    this.API.getEstudiantes().subscribe( estudiantes => {
      this.estudiantes = estudiantes;
    })
  }

  ngOnInit(): void {
  }

  onDelete(id: number){
    this.API.deleteEstudiante(id).subscribe(usuario => {
      this.API.getEstudiantes().subscribe(students => {
        this.estudiantes = students;
      })
    })
  }

}
