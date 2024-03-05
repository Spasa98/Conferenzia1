import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {zvanje} from '../../zvanje';
import { oblast } from '../../oblast';
import {ZvanjeOvlastSalaService} from '../../services/zvanjeoblastsala.service';
import { ConfirmationService, MessageService } from 'primeng/api';


@Component({
  selector: 'app-dodajpredavaca',
  templateUrl: './dodajpredavaca.component.html',
  styleUrls: ['./dodajpredavaca.component.scss']
})
export class DodajpredavacaComponent implements OnInit {
  form!: FormGroup
  
  oblasti: oblast[]=[];
  selectedOblastId!: number;
  zvanja: zvanje[]=[];
  selectedZvanjeId!: number;


  constructor(private fb: FormBuilder,
    private httpClient:HttpClient,
    private messageService: MessageService,
    private oblastService:ZvanjeOvlastSalaService,
    private zvanjeService:ZvanjeOvlastSalaService,
    private confirmationService: ConfirmationService) 
    {
      this.oblasti = [];
      this.zvanja = [];
    }

  ngOnInit(): void {
    this.form = this.fb.group({
      ime:'',
      prezime: '',
      opis: '',
      email: ['', [Validators.required, Validators.email]],
      lozinka: ['', [Validators.required]],
      telefon: '',
      grad: '',
      oblastId: '',
      zvanjeId: ''
    }
    )
    this.oblastService.getOblast().subscribe((oblast) => (this.oblasti = oblast));
    console.log(this.oblasti);
    this.zvanjeService.getZvanje().subscribe((zvanje) => (this.zvanja = zvanje));
    console.log(this.zvanja);
  }

  submit(){
    this.confirmationService.confirm({
      message: 'Da li ste sigurni da želite da izvršite ovu radnju?',
      accept: () => {
        this.httpClient.post("https://localhost:5001/Predavac/DodajPredavaca",this.form.getRawValue()).subscribe({
          next: res=>{
          this.messageService.add({key: 'br', severity:'success', summary: 'Uspešno', detail: 'Uspešno ste dodali predavača!'});
          },
          error: err=>{
          this.messageService.add({key: 'br', severity:'error', summary: 'Neuspešno', detail: 'Pokušajte ponovo, došlo je do greške!'});
          }
        });
      }
    });
  }


}
