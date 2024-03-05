import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';
import {predavaci} from '../../predavaci';
import {PredavacSelectOServiceService} from '../../services/predavac-select-o-service.service';
import { ConfirmationService, Message, MessageService, PrimeNGConfig } from 'primeng/api';
import { DialogService, DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { DodajpredavacaComponent } from '../dodajpredavaca/dodajpredavaca.component';

@Component({
  selector: 'app-predavac-select',
  templateUrl: './predavac-select.component.html',
  styleUrls: ['./predavac-select.component.scss']
})
export class PredavacSelectComponent implements OnInit {
  ref: any;

  
  constructor(private predavacService:PredavacSelectOServiceService, 
              public authService: AuthService,
              private messageService: MessageService,
              private primengConfig: PrimeNGConfig,
              public dialogService: DialogService,
              private confirmationService: ConfirmationService) { }
  
  index=["ID","Ime","Opis","Email","Telefon","Oblast"];
  predavac:predavaci[]=[];

  
  ngOnInit(): void {
    this.getPredavace(1);

    console.log('OVO JE IZ HOME',this.authService.userValue)
  }
  
  obirisPredavaca(id:number):void
  {
    this.confirmationService.confirm({
      message: 'Da li ste sigurni da želite da izvršite ovu radnju?',
      accept: () => {
        this.predavacService.obrisiPredavaca(id).subscribe({
              next: res=>{
                this.getPredavace(1);
                this.messageService.add({key: 'br', severity:'success', summary: 'Uspešno', detail: 'Uspešno ste izbrisali predavaca!'});
              },
              error: err=>{
                this.messageService.add({key: 'br', severity:'error', summary: 'Neuspešno', detail: 'Pokušajte ponovo, došlo je do greške!'});
              }
        });
      }
    });
  }
  

  povecaj(event:any): void {
    this.getPredavace(event.page+1);
    console.log(event);
  }
  getPredavace(pageId:number):void {
    this.predavacService.getPredavace(pageId).subscribe((predavac) => {
      this.predavac = predavac
      console.log('GET TASKS',predavac)
  });
 }
  dodajPredavaca() {
      
    this.ref = this.dialogService.open(DodajpredavacaComponent, {
        header: 'Dodaj predavača',
        width: '50%'
    });
    this.ref.onClose.subscribe(()=>
    {
      this.getPredavace(1);
    })
  }
}
