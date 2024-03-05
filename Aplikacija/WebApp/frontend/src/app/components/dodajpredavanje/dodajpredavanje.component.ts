import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Message, MessageService, PrimeNGConfig } from 'primeng/api';
import { oblast } from 'src/app/oblast';
import { predavaci } from 'src/app/predavaci';
import { sala } from 'src/app/sala';
import { PredavacSelectOServiceService } from 'src/app/services/predavac-select-o-service.service';
import { ZvanjeOvlastSalaService } from 'src/app/services/zvanjeoblastsala.service';
import {DatePipe} from '@angular/common';
import {ConfirmationService} from 'primeng/api';


@Component({
  selector: 'app-dodajpredavanje',
  templateUrl: './dodajpredavanje.component.html',
  styleUrls: ['./dodajpredavanje.component.scss'],
  providers: [DatePipe]
})
export class DodajpredavanjeComponent implements OnInit {
  form!: FormGroup;
  msgs1!: Message[];
  msgs2!: Message[];

  oblasti: oblast[]=[];
  sale: sala[]=[];
  predavaci: predavaci[]=[];

  constructor(private fb: FormBuilder,
    private httpClient:HttpClient,
    private zosService:ZvanjeOvlastSalaService,
    private predavacService:PredavacSelectOServiceService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService,
    private primengConfig: PrimeNGConfig,
    private datePipe:DatePipe)
    {
      this.oblasti = [];
      this.sale = [];
      this.predavaci = [];
   
    }

  ngOnInit(): void {
    this.form = this.fb.group({
      naziv:'',
      opis: '',
      datum: '',
      salaID: '',
      oblastID: '',
      predavacID: '',
      pom:false
    }
    
    ) 
   
    this.zosService.getOblast().subscribe((oblast) => (this.oblasti = oblast));
    this.zosService.getSala().subscribe((sala) => (this.sale = sala));
    this.predavacService.getPredavace(1).subscribe((predavac) => (this.predavaci = predavac));

  }
  submit() {
    if(this.form?.value?.datum !== ''){
      this.form.value.datum = this.datePipe.transform(this.form.value.datum,'yyyy-MM-ddTHH:mm:ss')
    }
    this.confirmationService.confirm({
      message: 'Da li ste sigurni da želite da izvršite ovu radnju?',
      accept: () => {
        this.httpClient.post(`https://localhost:5001/Predavanje/DodajPredavanje/`+`${this.form.value['naziv']}`+`/${this.form.value['opis']}`+`/${this.form.value['salaID']}`+`/${this.form.value['oblastID']}`+`/${this.form.value['pom']}`+`/${this.form.value['predavacID']}`+`/${this.form.value.datum}`,this.form.getRawValue()).subscribe({
          next: res=>{
            this.messageService.add({key: 'br', severity:'success', summary: 'Uspešno', detail: 'Dodali ste predavanje!'});
          },
          error: err=>{
            this.messageService.add({key: 'br', severity:'error', summary: 'Neuspešno', detail: 'Pokušajte ponovo, došlo je do greške!'});
        }
      });
    }
    });
  }
}
