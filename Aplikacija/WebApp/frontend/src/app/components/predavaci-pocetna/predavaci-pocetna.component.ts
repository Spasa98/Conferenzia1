import { Component, OnInit } from '@angular/core';
import { PredavacSelectOServiceService } from 'src/app/services/predavac-select-o-service.service';
import {predavaci} from '../../predavaci';
import {MenuItem, PrimeIcons} from 'primeng/api';

@Component({
  selector: 'app-predavaci-pocetna',
  templateUrl: './predavaci-pocetna.component.html',
  styleUrls: ['./predavaci-pocetna.component.scss']
})
export class PredavaciPocetnaComponent implements OnInit {
  vrednostdugmeta:number=1;
  predavac:any=[];
  predavac2:any=[];
  showmore:number=1;
  hidebutton!:boolean
  disableMoreButton = false;
  value3!:string;
  constructor(private predavacService:PredavacSelectOServiceService) { }


  ngOnInit(): void {
    this.getPredavace(this.vrednostdugmeta);
  }

  show(){
    this.showmore++;
    this.getPredavace(this.showmore);
  }
  getPredavace(pageId:number):void
  {
    this.predavacService.getPredavace(pageId).subscribe((predavac2) => {
      if(predavac2.length === 0 || predavac2.length <5)
      {
        this.disableMoreButton = true
      }
      this.predavac2 = this.predavac2.concat(predavac2)
    });
  }

  pretrazi()
  {
    this.disableMoreButton=true;
    if(this.value3!==undefined && this.value3!==''){
      this.predavacService.searchPretragaPredavaca(this.value3).subscribe((predavac2) => {
        this.predavac2 = predavac2
      });}
      else
      {
        this.predavacService.getPredavace(1).subscribe((predavac2) => {
          this.predavac2 = predavac2
          if(this.disableMoreButton===true)
          {
            this.disableMoreButton = false
            this.showmore=1;
          }
        });

      }

  }
}
