import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-registracija-slusaoca',
  templateUrl: './registracija-slusaoca.component.html',
  styleUrls: ['./registracija-slusaoca.component.scss'],
})
export class RegistracijaSlusaocaComponent implements OnInit {
  value1!: string;
  form!: FormGroup;

  constructor(private fb: FormBuilder,
    private httpClient: HttpClient,
    private messageService: MessageService) 
    { }

  ngOnInit(): void {
    this.form = this.fb.group({
      ime:'',
      prezime: '',
      telefon: '',
      email: ['', [Validators.required, Validators.email]],
      lozinka: ['', [Validators.required]],
    });
  }

  reg() {
    this.httpClient.post("https://localhost:5001/Slusalac/DodajSlusaoca",this.form.getRawValue()).subscribe({
      next: res=>{
        this.messageService.add({key: 'br', severity:'success', summary: 'Uspešno', detail: 'Registracija je uspela!'});
      },
      error: err=>{
        this.messageService.add({key: 'br', severity:'error', summary: 'Neuspešno', detail: 'Pokušajte ponovo, došlo je do greške!'});
      }
    });
  }
}
