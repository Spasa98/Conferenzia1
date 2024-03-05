import { Component, OnInit } from '@angular/core';
import { ConfirmationService, MessageService } from 'primeng/api';
import { ZahtevServiceService } from 'src/app/services/zahtev-service.service';
import { zahtevpredavac } from 'src/app/zahtevpredavac';

@Component({
  selector: 'app-zahtev-predavac-tabela',
  templateUrl: './zahtev-predavac-tabela.component.html',
  styleUrls: ['./zahtev-predavac-tabela.component.scss']
})
export class ZahtevPredavacTabelaComponent implements OnInit {

  constructor(private zahtevService:ZahtevServiceService,
    private confirmationService: ConfirmationService,
    private messageService: MessageService)
  { }

  zahtevpredavac: zahtevpredavac[]=[];
  numberofpages!:number;
  // idz:number=zahtevpredavac.id;
  ngOnInit(): void {
    this.zahtevService.getSveZahtevePredavacaPerPage(1).subscribe((zahtevpredavac) => (this.zahtevpredavac = zahtevpredavac));



  }
  odobriZahtev(zahtevpredavac:any)
  {
    this.confirmationService.confirm({
      message: 'Da li ste sigurni da želite da izvršite ovu radnju?',
      accept: () => {
        this.zahtevService.odobriZahtevPredavaca(zahtevpredavac.idzahtev).subscribe({
          next: res=>{
            zahtevpredavac
            this.messageService.add({key: 'br', severity:'success', summary: 'Uspešno', detail: 'Prihvatili ste zahtev predavača!'});
           this.getSveZahtevePredavacaPerPage(1).subscribe(()=> (this.zahtevpredavac = zahtevpredavac));
          },
          error: err=>{
            this.messageService.add({key: 'br', severity:'error', summary: 'Neuspešno', detail: 'Pokušajte ponovo, došlo je do greške!'});
          }
        });
      }
    });
  }
  odbijZahtevPredavaca(zahtevpredavac:any)
  {
    this.confirmationService.confirm({
      message: 'Da li ste sigurni da želite da izvršite ovu radnju?',
      accept: () => {
        this.zahtevService.odbijZahtevPredavaca(zahtevpredavac.idzahtev).subscribe({
          next: res=>{
          zahtevpredavac
          this.messageService.add({key: 'br', severity:'success', summary: 'Uspešno', detail: 'Odbili ste zahtev predavača!'});
          this.getSveZahtevePredavacaPerPage(1).subscribe(()=> (this.zahtevpredavac = zahtevpredavac));
        },
        error: err=>{
          this.messageService.add({key: 'br', severity:'error', summary: 'Neuspešno', detail: 'Pokušajte ponovo, došlo je do greške!'});
        }});
      }
    });
  }

  povecaj(event:any): void {
    this.getSveZahtevePredavacaPerPage(event.page+1);

  }
  getSveZahtevePredavacaPerPage(pageId:number):any {
    this.zahtevService.getSveZahtevePredavacaPerPage(pageId).subscribe((zahtevpredavac) => {
      this.zahtevpredavac = zahtevpredavac
      console.log('GET TASKS', zahtevpredavac)
    });
  }
}
