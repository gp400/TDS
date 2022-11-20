import { Component, OnInit } from '@angular/core';
import { Clase } from 'src/app/models/clase';
import { APIService } from 'src/app/services/api.service';

@Component({
  selector: 'app-index-user',
  templateUrl: './index-user.component.html',
  styleUrls: ['./index-user.component.css']
})
export class IndexUserComponent implements OnInit {

  clases: Clase[] = [];

  constructor(private API: APIService) { }

  ngOnInit(): void {
    this.API.getClasesByMaestro().subscribe( clases => {
      this.clases = clases;
    })
  }

}
