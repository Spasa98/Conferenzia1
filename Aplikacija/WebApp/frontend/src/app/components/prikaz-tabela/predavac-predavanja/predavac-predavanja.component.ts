import { Component, OnInit } from '@angular/core';
import { predavanja } from 'src/app/predavanja';
import { PredavanjaServiceService } from 'src/app/services/predavanja-service.service';
import { AuthService } from 'src/app/services/auth.service';
import { NgModule }  from '@angular/core';

@Component({
  selector: 'app-predavac-predavanja',
  templateUrl: './predavac-predavanja.component.html',
  styleUrls: ['./predavac-predavanja.component.scss']
})
export class PredavacPredavanjaComponent implements OnInit {
  pre:string="Proslih"
  posle:string="Predstojecih"
  constructor(private predavanjeService:PredavanjaServiceService,public authService: AuthService) { }
  predavac:number=this.authService.userValue.id;
  predavanje: predavanja[]=[];
  
  ngOnInit(): void {
    this.predavanjeService.PredavanjaPredavaca("Predstojecih",this.predavac,1,3).subscribe((predavanje) => console.log(this.predavanje = predavanje));
  }
  radnja(prePosle:string){
    this.predavanjeService.PredavanjaPredavaca(prePosle,this.predavac,1,3).subscribe((predavanje) => console.log(this.predavanje = predavanje));
    
  }
}
