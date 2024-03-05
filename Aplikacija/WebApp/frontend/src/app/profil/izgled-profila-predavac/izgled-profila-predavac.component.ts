import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ZahtevServiceService } from 'src/app/services/zahtev-service.service';
import { zahtevpredavac } from 'src/app/zahtevpredavac';
import { Observable, Subscription } from 'rxjs'
import { HttpClient } from '@angular/common/http';
import { AuthService } from 'src/app/services/auth.service';
import { CommonModule } from '@angular/common';  
import { BrowserModule } from '@angular/platform-browser';
import { predavanja } from '../../predavanja';
import { PredavanjaServiceService } from '../../services/predavanja-service.service';
import { FeedbackService } from 'src/app/services/feedback.service';
import { ActivatedRoute } from '@angular/router';
import {MenuItem, MessageService, ConfirmationService, PrimeIcons} from 'primeng/api';
import { NumberSymbol } from '@angular/common';
import { Paginator } from 'primeng/paginator';
import { ThisReceiver } from '@angular/compiler';
import { __core_private_testing_placeholder__ } from '@angular/core/testing';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { ReportFormComponent } from './report-form/report-form.component';


@Component({
  selector: 'app-izgled-profila-predavac',
  templateUrl: './izgled-profila-predavac.component.html',
  styleUrls: ['./izgled-profila-predavac.component.scss']

})



export class IzgledProfilaPredavacComponent implements OnInit, OnDestroy {
  subscriptions: Subscription[] = [];
  id!: number;
  zahtevpredavac: any=[];
  predavanje:any=[];
  feedback:any=[];
  predavac:any=[];
  slusalac:number=this.authService.userValue.id;
  strana:number=1;
  nesto:boolean=false;
  dalJeProfil:boolean=false;
  logovaniPredavac:boolean=false;//da proverim da li moze da se odjavljuje sa predavanja
  sort:any[]=[];
  selected!:number//koristi se za order
  brstranicePredavanja:number=1;
  zahtevPred:zahtevpredavac[]=[];
  resetstranice:number=1;
  tenutnastranica:number=1;
  ref!: DynamicDialogRef;

  @ViewChild('p' ,{static:true}) paginator!:Paginator;
  
  constructor(
    private zahtevService:ZahtevServiceService,
    private predavanjeServe:PredavanjaServiceService, 
    private httpClient: HttpClient,
    public authService: AuthService,
    public messageService: MessageService,
    private feedbackService:FeedbackService,
    private route: ActivatedRoute, 
    private confirmationService: ConfirmationService,
    public dialogService: DialogService
    ) 
  {
    this.sort= [
      {name: 'Predavanja predavaca ', code: '0'},
      {name: 'Predstojeća predavanja ', code: '2'},
      {name: 'Prošla predavanja', code: '3'},
      {name: 'Prijava za predavanje', code: '1'},
    ];

  }

  ngOnInit(): void {
    this.id = this.route.snapshot.params['id']
    if(this.id==this.authService?.userValue?.id)
    {
      this.dalJeProfil=true
    }
    if(this.id==this.authService?.userValue?.id && this.authService?.userValue?.role=='Predavac' )
    {
      this.logovaniPredavac=true
    }
    if(this.authService?.userValue?.role==="Predavac")
    {
     this.getSveZahteveJednogPredavaca(this.authService?.userValue?.id,1);
    }
    this.getPredavanjaJednogPredavaca(1,1,3);
    this.getFeedbacksPredavaca();
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach(subscription => subscription.unsubscribe())
  }

  povecaj(event:any): void {
    if(this.selected==0)
    { 
      this.getPredavanjaJednogPredavaca(this.id,event.page+1,3);
    }
    if(this.selected==1)
    {
      this.getPredavanjaBezPredavacaPoOblasti(event.page+1,this.id)
    }
    if(this.selected==2)
    {
      this.getProslaPredstojecaPredavanja('Predstojecih',event.page+1);
    }
    if(this.selected==3)
    {
      this.getProslaPredstojecaPredavanja('Proslih',event.page+1);
    }

  }

  povecajZahtev(event:any): void {
    this.getSveZahteveJednogPredavaca(this.id,event.page+1);
    this.tenutnastranica=event.page+1
  }

  getPredavanjaJednogPredavaca(predavacId:number,pageid:number,elperpage:number):void { 
    this.predavanjeServe.getPredavanjaJednogPredavaca(this.id,pageid,3).subscribe((predavanja) => {
      this.predavanje = predavanja
    });
  }

  getSveZahteveJednogPredavaca(predavacID:number,pageId:number):void
  { 
    this.zahtevService.getSveZahteveJednogPredavaca(this.id,pageId).subscribe((zahtevpredavac) => {
    this.zahtevpredavac=zahtevpredavac
    });
  }
  getFeedbacksPredavaca(){
    this.feedbackService.getFeedbacksJednogPredavaca(this.id,this.strana).subscribe((feedback) => {
      console.log(this.feedback = feedback)
    });

  }
  loadMore(){
    this.strana++;
    this.feedbackService.getFeedbacksJednogPredavaca(this.id,this.strana).subscribe((feedback) => {
      this.feedback = feedback
    });

  }
  
  reportFeedback(feedbackid:number){
    this.ref = this.dialogService.open(ReportFormComponent, {
      data:{
      fedback: feedbackid,
      },
      header: 'Report feedback',
      width: '35%'
    });
    this.ref.onClose.subscribe(res=>
    {
      this.getFeedbacksPredavaca();
    })
  }
    
  promena(event:any):void
  {
  this.paginator.changePage(0);
  console.log(this.selected)
  if(this.selected==0)
  {
   
    this.getPredavanjaJednogPredavaca(this.id,1,3);
  }
  if(this.selected==1)
  {
    this.paginator.changePageToFirst;
    this.getPredavanjaBezPredavacaPoOblasti(1,this.id);
  }
  if(this.selected==2)
  {
    this.paginator.changePageToFirst;
    this.getProslaPredstojecaPredavanja('Predstojecih',1);
  }
  if(this.selected==3)
  {
    this.paginator.changePageToFirst;
    this.getProslaPredstojecaPredavanja('Proslih',1);
  }
  }
  
  deleteZahtev(zahtevPred:any)
  {
    this.confirmationService.confirm
    ({
      message: 'Da li ste sigurni da želite da izvršite ovu radnju?',
      accept: () => {
      console.log(zahtevPred.idzahtev);
      this.zahtevService.izbrisatiZahtev(zahtevPred.idzahtev).subscribe({
            next: res=>{
              this.getSveZahteveJednogPredavaca(this.id,this.tenutnastranica)
              this.messageService.add({key: 'br', severity:'success', summary: 'Uspešno', detail: 'Uspešno ste izbrisali zahtev!'});
            },
            error: err=>{
              this.messageService.add({key: 'br', severity:'error', summary: 'Neuspešno', detail: 'Pokušajte ponovo, došlo je do greške!'});
            }
      });
    }
  });
    
  }
  postNoviZahtev(predavanje:any,text:string):any
  {
    this.confirmationService.confirm
    ({
      message: 'Da li ste sigurni da želite da izvršite ovu radnju?',
      accept: () => 
      {
        this.zahtevService.postNoviZahtev(this.authService?.userValue?.id,predavanje.id,text).subscribe
        ({
        next: res=>
        {
          this.getSveZahteveJednogPredavaca(this.id,this.tenutnastranica)
          this.messageService.add({key: 'br', severity:'success', summary: 'Uspešno', detail: 'Uspešno ste poslali zahtev!'});
        },
        error: err=>
        {
            this.messageService.add({key: 'br', severity:'error', summary: 'Neuspešno', detail: 'Pokušajte ponovo, došlo je do greške!'});
        }
        });
      }
    });
  }
  
  getPredavanjaBezPredavacaPoOblasti(pageId:number,predavacId:number):void{ 
    this.subscriptions.push(
      this.predavanjeServe.getPredavanjaBezPredavacaPoOblasti(pageId,this.id).subscribe((predavanja) => {
        this.predavanje = predavanja
      })
    )
  }
  getProslaPredstojecaPredavanja(proslapredstojeca:string,pagenumber:number):void{
    this.subscriptions.push(
      this.predavanjeServe.PredavanjaPredavaca(proslapredstojeca,this.id,pagenumber,3).subscribe((predavanja) => {
        this.predavanje = predavanja;
      })
    )
  }
}

