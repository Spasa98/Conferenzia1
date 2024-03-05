import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { predavaci } from 'src/app/predavaci';
import { PredavacSelectOServiceService } from 'src/app/services/predavac-select-o-service.service';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import {Location} from '@angular/common';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { ConfirmationService, MessageService } from 'primeng/api';

@Component({
  selector: 'app-dodaj-feedback',
  templateUrl: './dodaj-feedback.component.html',
  styleUrls: ['./dodaj-feedback.component.scss']
})

export class DodajFeedbackComponent implements OnInit {
  id!: number;
  predavac!:any;
  slusalacID:number=this.authService.userValue.id;
  form!: FormGroup
  tip!: string;
  selektovano: any = null;
  kategorija: any[] = [{name: 'Pozitivan', key: 'P'}, {name: 'Negativan', key: 'N'}];
  val1: number = 3;
  predavaci: predavaci[]=[];
  selectedPredavacId!: number;

  constructor(private _location: Location,private fb: FormBuilder,
    private httpClient:HttpClient,
    private route: ActivatedRoute,
    private predavacService:PredavacSelectOServiceService,public authService: AuthService,
    public ref: DynamicDialogRef,
    private messageService: MessageService,
    private confirmationService: ConfirmationService, 
    public config: DynamicDialogConfig,
    ) {
      this.predavaci = [];
    }

  ngOnInit(): void {

    this.selektovano = this.kategorija[1];
    this.form = this.fb.group({
      opis: '',
      ocena: '',
     
    })
    console.log("proveraaaaa",this.config?.data?.pradid)
  }

  submit() {
      this.confirmationService.confirm({
      message: 'Da li ste sigurni da želite da izvršite ovu radnju?',
      accept: () => {
      this.httpClient.post(`https://localhost:5001/Feedback/NovFeedback/${this.slusalacID}/${this.config?.data?.pradid}`,this.form.getRawValue()).subscribe({
        next: res=>{
          this.messageService.add({key: 'br', severity:'success', summary: 'Uspešno', detail: 'Dodali ste organizatora!'});
        },
          error: err=>{
          this.messageService.add({key: 'br', severity:'error', summary: 'Neuspešno', detail: 'Pokušajte ponovo, došlo je do greške!'});
          }
        });
      }
    });
  }
}
