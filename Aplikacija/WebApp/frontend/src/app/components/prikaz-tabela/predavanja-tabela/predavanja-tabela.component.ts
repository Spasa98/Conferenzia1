import { Component, OnInit, ViewChild } from '@angular/core';
import { predavanja } from 'src/app/predavanja';
import { PredavanjaServiceService } from 'src/app/services/predavanja-service.service';
import  {DodajpredavanjeComponent} from './../../dodajpredavanje/dodajpredavanje.component'
import {Paginator, PaginatorModule} from 'primeng/paginator';
import {DropdownModule} from 'primeng/dropdown';
import {DynamicDialogModule} from 'primeng/dynamicdialog';
import {DialogService} from 'primeng/dynamicdialog';
import {ConfirmationService, MessageService} from 'primeng/api';
import {DynamicDialogRef} from 'primeng/dynamicdialog'
import { IzmeniPredavanjeComponent } from '../../izmeni-predavanje/izmeni-predavanje.component';
import { AddpredavaconpredavanjeComponent } from '../../addpredavaconpredavanje/addpredavaconpredavanje.component';


@Component({
  selector: 'app-predavanja-tabela',
  templateUrl: './predavanja-tabela.component.html',
  styleUrls: ['./predavanja-tabela.component.scss']
})
export class PredavanjaTabelaComponent implements OnInit {
  @ViewChild('p' ,{static:true}) paginator!:Paginator;

  pomo:number=150
  selected!:number//koristi se za order
  selectedFP!:string//filtrirani prikaz
  sort:any[]=[];
  datumi:any[]=[];

  constructor(private predavanjeService:PredavanjaServiceService,
    public dialogService: DialogService,
    public messageService: MessageService,
    private confirmationService: ConfirmationService) 
  {
    this.sort= [
      {name: 'nesortirani prikaz ', code: '0'},
      {name: 'sortiraj po predavacima', code: '2'},
      {name: 'sortiraj po oblastima', code: '1'},
      {name: 'sortiraj po datumu', code: '3'}
    ];
    this.datumi= [
      {name: 'Sva predavanja ', code: 'VratiSvaPredavanja'},
      {name: 'Prosla predavanja', code: 'PrikaziProslaPredavanja'},
      {name: 'Predstojeca predavanja', code: 'PrikazPredstojecihPredavanja'}
    ];
    console.log(this.selected)
  }

  //index=["ID","Naziv","Oblast","Predavac","Sala","Datum","Pocetak predavanja","Kraj predavanja"];
  predavanje: predavanja[]=[];
  pomocna:number=1;
  ref!: DynamicDialogRef;
  pomocnaidPredavanja!:number

  ngOnInit(): void {
    this.predavanjeService.getPredavanja(1,0).subscribe((predavanje) => (this.predavanje = predavanje));
    console.log(predavanja);
  }

  povecaj(event:any): void {
  
    // event.page===0?1
    if(this.selectedFP!=='PrikaziProslaPredavanja' && this.selectedFP!=='PrikazPredstojecihPredavanja')
    {
     this.getPredavanja(event.page+1,this.selected);
    }
    this.getFuturePast(event.page+1,this.selectedFP);
     console.log(event.page);
     this.pomocna=event.page;//pomocna bitna za dropdovn change
     console.log("ovdee",this.pomocna)
  }

  getPredavanja(pageId:number,sortiranje:number):void
  {   
  this.predavanjeService.getPredavanja(pageId,this.selected).subscribe((predavanja) => {
    this.predavanje = predavanja
    console.log('GET TASKS',predavanja)
  });
  }

  getFuturePast(pageId:number,FP:string):void
  { 
  this.predavanjeService.getPastFutur(pageId,this.selectedFP).subscribe((predavanja) => {
    this.predavanje = predavanja
    console.log('GET TASKS',predavanja)
  });
  
  }
  promena(event:any):void
  {
    if(this.selectedFP!=='PrikaziProslaPredavanja' && this.selectedFP!=='PrikazPredstojecihPredavanja')
    {
    this.paginator.changePage(0);
    this.getPredavanja(1,this.selected)
    }
  }

  proslapredstojeca(event:any):void
  {
    this.paginator.changePage(0);
    this.getFuturePast(1,this.selectedFP)  
  }

  deletePredavanje(predavanja:any)
  {
    this.confirmationService.confirm({
      message: 'Da li ste sigurni da želite da izvršite ovu radnju?',
      accept: () => {
        console.log(predavanja.id);
        this.predavanjeService.IzbrisatiPredavanje(predavanja.id).subscribe({
              next: res=>{
                this.getPredavanja(this.pomocna,this.selected);
                this.messageService.add({key: 'br', severity:'success', summary: 'Uspešno', detail: 'Uspešno ste izbrisali predavanje!'});
              },
              error: err=>{
                this.messageService.add({key: 'br', severity:'error', summary: 'Neuspešno', detail: 'Pokušajte ponovo, došlo je do greške!'});
              }
        });
      }
    });
  }

  show() {
    this.ref = this.dialogService.open(DodajpredavanjeComponent, {
        header: 'Dodaj predavanje',
        width: '40%'
    });
    this.ref.onClose.subscribe(res=>
      {
        this.getPredavanja(this.pomocna,this.selected);
      })
  }
  Ukloni(id:number)
  {
    this.confirmationService.confirm({
      message: 'Da li ste sigurni da želite da izvršite ovu radnju?',
      accept: () => {
        this.predavanjeService.ukloniPredavaca(id).subscribe({
              next: res=>{
                this.getPredavanja(this.pomocna,this.selected);
                this.messageService.add({key: 'br', severity:'success', summary: 'Uspešno', detail: 'Uspešno ste uklonili predavaca!'});
              },
              error: err=>{
                this.messageService.add({key: 'br', severity:'error', summary: 'Neuspešno', detail: 'Pokušajte ponovo, došlo je do greške!'});
              }
        });
      }
    });
  }
  IzmeniPredavanje(predavanjeid:number,predavacIme:string) {
    
    this.ref = this.dialogService.open(IzmeniPredavanjeComponent, {
        data:
        {
          predavacIme :predavacIme,  
          idpomocni: predavanjeid,
          
        },
        header: 'Izmeni predavanje',
        width: '50%'
    });
    this.ref.onClose.subscribe(res=>
      {
        this.getPredavanja(this.pomocna,this.selected);
      })
  }
  DodajPredavacaNaPredavanje(predavanjeid:number) {
    
    this.ref = this.dialogService.open(AddpredavaconpredavanjeComponent, {
        data:
        {
          
          idpomocni: predavanjeid, 
        },
        header: 'Dodaj predavaca na predavanje',
        width: '40%',
    });
    this.ref.onClose.subscribe(res=>
      {
        this.getPredavanja(this.pomocna,this.selected);
      })
  }
 
}

