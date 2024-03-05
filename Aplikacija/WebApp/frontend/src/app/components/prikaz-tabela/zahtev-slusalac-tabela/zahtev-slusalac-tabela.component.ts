import { Component, OnInit } from '@angular/core';
import { ZahtevServiceService } from 'src/app/services/zahtev-service.service';
import { zahtevslusalac } from 'src/app/zahtevslusalac';
import { AuthService } from 'src/app/services/auth.service';
import { ConfirmationService } from 'primeng/api';

@Component({
  selector: 'app-zahtev-slusalac-tabela',
  templateUrl: './zahtev-slusalac-tabela.component.html',
  styleUrls: ['./zahtev-slusalac-tabela.component.scss']
})
export class ZahtevSlusalacTabelaComponent implements OnInit {
  status:any[]=[];
  selected:string ="Neobradjene";
  constructor(private zahtevService:ZahtevServiceService,public authService: AuthService,
  private confirmationService: ConfirmationService)
  { 
    this.status= [
      {name: 'Neobradjeni', code: 'Neobradjene'},
      {name: 'Odobreni', code: 'Odobrene'},
      {name: 'Odbijeni', code: 'Odbijene'},
    ];
  }

  organizatorId:number=this.authService.userValue.id;
  zahtevslusalac: any[]=[];
  brstr:number=1;
  brstr2:number=1;
  predavanje:any=[];

  ngOnInit(): void {
    this.zahtevService.getBrojStrana(this.selected).subscribe((brstr2) => console.log(this.brstr2 = brstr2));
    this.Zahtevi(1);
    console.log(zahtevslusalac);
  }
  Zahtevi(page:number){
    this.zahtevService.getBrojStrana(this.selected).subscribe((brstr2) => console.log(this.brstr2 = brstr2));
    this.zahtevService.getZahtevSlusalac(page,this.selected).subscribe((zahtevslusalac) => console.log(this.zahtevslusalac = zahtevslusalac));
  }
  promena(event:any){
    this.zahtevService.getBrojStrana(this.selected).subscribe((brstr2) => console.log(this.brstr2 = brstr2));
    this.Zahtevi(this.brstr);
  }
  povecaj(event:any){
    this.Zahtevi(event.page+1);
    this.brstr=event.page+1;
  }
  obrada(pom:string,zahId:number){
    this.confirmationService.confirm({
      message: 'Da li ste sigurni da želite da izvršite ovu radnju?',
      accept: () => {
        this.zahtevService.obradaZahteva(pom,zahId).subscribe(p=>this.Zahtevi(this.brstr));
      }
    });
  }
}
