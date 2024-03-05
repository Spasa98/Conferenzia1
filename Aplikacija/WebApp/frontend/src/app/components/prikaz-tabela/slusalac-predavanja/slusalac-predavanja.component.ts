import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { predavanja } from 'src/app/predavanja';
import { PredavanjaServiceService } from 'src/app/services/predavanja-service.service';
import { FormGroup, FormControl, Validators} from '@angular/forms';
import { PredavacSelectOServiceService } from 'src/app/services/predavac-select-o-service.service';
import { AuthService } from 'src/app/services/auth.service';
import { predavaci } from 'src/app/predavaci';
@Component({
  selector: 'app-slusalac-predavanja',
  templateUrl: './slusalac-predavanja.component.html',
  styleUrls: ['./slusalac-predavanja.component.scss']
})
export class SlusalacPredavanjaComponent implements OnInit {
  
  
  pree:string="Odslusana"
  poslee:string="Predstojeca"
  constructor(private predavanjeService:PredavanjaServiceService,private predavacService:PredavacSelectOServiceService,public authService: AuthService) { }
  slusalac:number=this.authService.userValue.id;
  predavanje: predavanja[]=[];
  predavac!:any;
  text: string = "a";
  ngOnInit(): void {
    
    this.predavanjeService.PredavanjaSlusaoca("Predstojeca",this.slusalac).subscribe((predavanje) => this.predavanje = predavanje);
  }
  radnja(prePosle:string){
    this.text=prePosle;
    this.predavanjeService.PredavanjaSlusaoca(prePosle,this.slusalac).subscribe((predavanje) =>this.predavanje = predavanje);
    
  }
  vratiPredavaca(predavanjeID:number){
    
    
  }

}
