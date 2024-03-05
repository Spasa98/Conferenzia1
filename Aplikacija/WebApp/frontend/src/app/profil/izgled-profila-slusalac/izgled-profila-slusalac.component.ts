import { Component, OnInit } from '@angular/core';
import { ZahtevServiceService } from 'src/app/services/zahtev-service.service';
import { SlusalacService } from 'src/app/services/slusalac.service';
import { AuthService } from 'src/app/services/auth.service';
import { ActivatedRoute } from '@angular/router';
import { PredavanjaServiceService } from 'src/app/services/predavanja-service.service';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-izgled-profila-slusalac',
  templateUrl: './izgled-profila-slusalac.component.html',
  styleUrls: ['./izgled-profila-slusalac.component.scss']
})
export class IzgledProfilaSlusalacComponent implements OnInit {
  selected:string ="Neobradjen";
  status:any[]=[];
  id!: number;
  brstr2: number=1;
  brstrZ: number=1;
  brstrP: number=1;
  dalJeProfil:boolean=false;
  zahtevslusalac: any[]=[];
  predavanja: any[]=[];
  stranaP:number=1;
  stranaZ:number=1;
  constructor(public messageService:MessageService ,private predavanjeService:PredavanjaServiceService,public zahtevService:ZahtevServiceService,public authService: AuthService,public route: ActivatedRoute,private slusalacService:SlusalacService) {
    this.status= [
      {name: 'Neobradjeni', code: 'Neobradjen'},
      {name: 'Odobreni', code: 'Odobren'},
      {name: 'Odbijeni', code: 'Odbijen'},
      
    ];
   }

  ngOnInit(): void {
    this.id = this.route.snapshot.params['id']
    if(this.id==this.authService?.userValue?.id){this.dalJeProfil=true}
    this.TrazeniZahtevi(1);
    this.getPredavanja(1)
  }
  TrazeniZahtevi(page:number){
    this.zahtevService.getBrojStranaZ(this.selected,this.id).subscribe((brstr) => console.log(this.brstrZ = brstr,"BROJ STRANICA"));
    this.zahtevService.getZahtevTrazeni(this.id,this.selected,page).subscribe((zahtevslusalac) => console.log(this.zahtevslusalac = zahtevslusalac));
  }
  promena(event:any){
    this.TrazeniZahtevi(this.brstr2);
    
  }
  povecajZ(event:any){
    this.stranaZ=event.page+1;
    this.TrazeniZahtevi(event.page+1);
    
  }
  getPredavanja(page:number){
    this.zahtevService.getBrojStranaZ("Odobren",this.id).subscribe((brstr) => console.log(this.brstrP = brstr,"BROJ STRANICA"));
    this.slusalacService.PredavanjaSlusaoca(this.id,page).subscribe((predavanja) => console.log(this.predavanja = predavanja));
  
  }
  povecajP(event:any){
    this.stranaP=event.page+1;
    this.getPredavanja(event.page+1);
    
  }
  izbrisiZahtev(id:number):any{
    console.log(id);
    this.zahtevService.izbrisiZahtevSlusaoca(id).subscribe({
          next: res=>{
            this.TrazeniZahtevi(this.stranaZ);
            this.messageService.add({key: 'br', severity:'success', summary: 'Uspešno', detail: 'Uspešno ste izbrisali zahtev!'});
          },
          error: err=>{
            this.messageService.add({key: 'br', severity:'error', summary: 'Neuspešno', detail: 'Pokušajte ponovo, došlo je do greške!'});
          }
    });

  }
}
