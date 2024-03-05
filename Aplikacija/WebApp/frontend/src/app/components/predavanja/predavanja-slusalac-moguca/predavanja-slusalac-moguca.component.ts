import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { PredavanjaServiceService } from 'src/app/services/predavanja-service.service';
import { ZahtevServiceService } from 'src/app/services/zahtev-service.service';

@Component({
  selector: 'app-predavanja-slusalac-moguca',
  templateUrl: './predavanja-slusalac-moguca.component.html',
  styleUrls: ['./predavanja-slusalac-moguca.component.scss']
})
export class PredavanjaSlusalacMogucaComponent implements OnInit {

  constructor(private zahtevServe:ZahtevServiceService,private predavanjeServe:PredavanjaServiceService,public authService: AuthService) { }
  predavanje:any[]=[];
  brstr:number =1;
  slusalac:number=this.authService?.userValue?.id;


  ngOnInit(): void {
    console.log("aaa",this.slusalac);
  this.PredavanjaBezZahtevaa(1);
  }

  PredavanjaBezZahtevaa(pageId:number)
  { 
  this.predavanjeServe.PredavanjaBezZahteva(pageId,this.slusalac).subscribe((predavanja) => {console.log(this.predavanje = predavanja)});
  
  }
  povecaj(event:any): void {
    
    this.PredavanjaBezZahtevaa(event.page+1);
    this.brstr=event.page+1;
  }
  saljiZahtev(id:any,event:any){
    this.zahtevServe.postNoviZahtevSlusalac(id,this.slusalac).subscribe(res=>this.PredavanjaBezZahtevaa(this.brstr));
    
    
  }
}
