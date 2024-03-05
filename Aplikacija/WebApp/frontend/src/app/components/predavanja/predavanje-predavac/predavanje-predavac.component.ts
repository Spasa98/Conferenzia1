import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import {predavanja} from '../../../predavanja';
import {PredavanjaServiceService} from '../../../services/predavanja-service.service';
import {PaginatorModule} from 'primeng/paginator';
import { DialogService, DynamicDialogRef} from 'primeng/dynamicdialog';
import { MessageService } from 'primeng/api';
import { DodajpredavanjeComponent } from '../../dodajpredavanje/dodajpredavanje.component';
import { JednoPredavanjeComponent } from '../../jedno-predavanje/jedno-predavanje.component';
import { NumberValueAccessor } from '@angular/forms';

@Component({
  selector: 'app-predavanje-predavac',
  templateUrl: './predavanje-predavac.component.html',
  styleUrls: ['./predavanje-predavac.component.scss']
})
export class PredavanjePredavacComponent implements OnInit {
  
  value3!:string;
  showmore:number=1;
  hidebutton!:boolean
  disableMoreButton = false;
  predavanje:any=[];
  idPredavanja!: number;
  ref!: DynamicDialogRef;
  aftersearchpom:number=0;


  constructor(private predanjeServe:PredavanjaServiceService,
    public dialogService: DialogService,
    public messageService: MessageService) {
   }


  ngOnInit(): void {
    this.getPredavanja(1);
  }
  povecak(event:any): void {
     this.getPredavanja(event.page+1);
     console.log(event); 
  }
  showmoree()
  {
    this.showmore++;
    this.getPredavanja(this.showmore);
  }
  getPredavanja(pageId:number):void
  { 
  this.predanjeServe.getPredavanja(pageId,0).subscribe((predavanja) => {
    if(predavanja.length === 0 || predavanja.length <5)
    {
      this.disableMoreButton = true
    }
    this.predavanje = this.predavanje.concat(predavanja)
    console.log('GET TASKS',predavanja)
  });
  }
  // getPredavace(pageId:number):void
  // {
  //   this.predavacService.getPredavace(pageId).subscribe((predavac2) => {
  //     if(predavac2.length === 0 || predavac2.length <5){
       
  //     }
  //     this.predavac2 = this.predavac2.concat(predavac2)
  //   });
  // }
  getJednoPredavanje()
  {
    this.predanjeServe.getJednoPredavanje(this.idPredavanja).subscribe((predavanja) => {
      this.predavanje = predavanja
      console.log("id ovog predavanja",this.predavanje.id)
    });
  }
  pretrazi()
  {
    this.disableMoreButton = true;
    if(this.value3!==undefined && this.value3!==''){
    this.predanjeServe.searchPretragaPredavanja(this.value3).subscribe((predavanja) => {
      this.predavanje = predavanja
    });}
    else
    {
      this.predanjeServe.getPredavanja(1,0).subscribe((predavanja) => {
        this.predavanje = predavanja
        if(this.disableMoreButton===true)
        {
          this.disableMoreButton = false
          this.showmore=1;
        }
      });
    }
    console.log(this.value3)
  }
  show(predavanjeid:number,predavac:string) {
    
    this.ref = this.dialogService.open(JednoPredavanjeComponent, {
        data:
        {
          predavacime:predavac,
          idpomocni: predavanjeid,
          
        },
        header: 'Detaljnije o predavanju',
        width: '40%'
    });
    this.ref.onClose.subscribe(res=>
      {
        this.getPredavanja(1);
      })
  }

}
