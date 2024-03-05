import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { predavanja } from '../../../../predavanja';
import { zahtevpredavac } from '../../../../zahtevpredavac';
import { PredavanjaServiceService } from '../../../../services/predavanja-service.service';
import { ZahtevServiceService } from '../../../../services/zahtev-service.service';
import {PaginatorModule } from 'primeng/paginator';
import { NumberSymbol } from '@angular/common';
import { AuthService } from 'src/app/services/auth.service';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-predavanja-moguca',
  templateUrl: './predavanja-moguca.component.html',
  styleUrls: ['./predavanja-moguca.component.scss']
})
export class PredavanjaMogucaComponent implements OnInit {

  constructor(private predavanjeServe:PredavanjaServiceService,
    private zahtevService:ZahtevServiceService,
    private messageService: MessageService,
    public authService: AuthService) { }

  predavanje:any=[];
  //promeniti akometo-zaut
  predavac:number=this.authService.userValue.id;


  ngOnInit(): void {
    console.log("id:",this.authService.userValue.id);
    this.getPredavanjaBezPredavacaPoOblasti(1,this.predavac);
  }
  povecak(event:any): void {
  
    // event.page===0?1
     this.getPredavanjaBezPredavacaPoOblasti(event.page+1,this.predavac);
     console.log(event);

}

getPredavanjaBezPredavacaPoOblasti(pageId:number,predavacId:number):void
  { 
  this.predavanjeServe.getPredavanjaBezPredavacaPoOblasti(pageId,this.predavac).subscribe((predavanja) => {
    this.predavanje = predavanja

  });
  }
  // getZahtev(predavacId:number):void
  // {
  //   this.predavacService.getPredavaca(predavacId).subscribe((predavaci) => {
  //     this.predavac = predavaci[0]
  //     console.log(this.predavac)
  //   });
  postNoviZahtev(predavanje:any)
  {
    console.log('GET TASKS',(predavanje.id))
    this.zahtevService.postNoviZahtev(this.authService?.userValue?.id,predavanje.id,`prijava`).subscribe({
      next: res=>{
        this.messageService.add({key: 'br', severity:'success', summary: 'Uspešno', detail: 'Uspešno ste poslali zahtev!'});
      },
      error: err=>{
        this.messageService.add({key: 'br', severity:'error', summary: 'Neuspešno', detail: 'Pokušajte ponovo, došlo je do greške!'});
      }
    });
  }
  // odbiZahtevPredavaca(ZahtevID:number)
  // {
    
  // }
}