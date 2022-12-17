import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Location } from '@angular/common'
import { APIService } from 'src/app/services/api.service';
import { ActivatedRoute } from '@angular/router';
import { Entrega } from 'src/app/models/entrega';
import { doc, getDoc } from 'firebase/firestore';
import { db } from 'src/firebase/provider';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-entregas',
  templateUrl: './entregas.component.html',
  styleUrls: ['./entregas.component.css']
})
export class EntregasComponent implements OnInit {

  constructor(private location: Location, private API: APIService, private route: ActivatedRoute) { }

  entregas: Entrega[] = [];
  entrega: Entrega = new Entrega();
  @ViewChild("btnCerrar") btnCerrar!: ElementRef;

  ngOnInit(): void {
    let id = parseInt(this.route.snapshot.paramMap.get("idTarea") || "0");
    if (id != 0){
      this.API.getEntregas(id).subscribe( entregas => {
        this.entregas = entregas;
      })
    }
  }

  back(): void {
    this.location.back()
  }

  setEntrega(entrega: Entrega){
    this.entrega = entrega;
  }

  async getFile(entrega: Entrega){
    const docRef = doc(db, "entregas", entrega.documento);
    const docSnap = await getDoc(docRef);
    window.open(docSnap.data()?.['url']);
  }

  onSubmit(){
    if (this.entrega.calificacion >= 0){
      this.API.updateEntrega(this.entrega).subscribe(put => {
        this.btnCerrar.nativeElement.click();
      }, error => {
        Swal.fire(
          "Error",
          error.error,
          "error"
        )
      });
    }
  }
}
