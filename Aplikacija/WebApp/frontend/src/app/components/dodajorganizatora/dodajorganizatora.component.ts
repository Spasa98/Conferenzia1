import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ConfirmationService, MessageService } from 'primeng/api';

@Component({
  selector: 'app-dodajorganizatora',
  templateUrl: './dodajorganizatora.component.html',
  styleUrls: ['./dodajorganizatora.component.scss']
})
export class DodajorganizatoraComponent implements OnInit {
  form!: FormGroup;

  constructor(private fb: FormBuilder,
    private httpClient: HttpClient,
    private messageService: MessageService,
    private confirmationService: ConfirmationService)
  { }

  ngOnInit(): void {
    this.form = this.fb.group({
      ime:'',
      prezime: '',
      email: ['', [Validators.required, Validators.email]],
      lozinka: ['', [Validators.required]],
    })
  }

  submit() {
    this.confirmationService.confirm({
      message: 'Da li ste sigurni da želite da izvršite ovu radnju?',
      accept: () => {
        this.httpClient.post("https://localhost:5001/ZvanjeOblast/DodajOrganizatora",this.form.getRawValue()).subscribe({
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
