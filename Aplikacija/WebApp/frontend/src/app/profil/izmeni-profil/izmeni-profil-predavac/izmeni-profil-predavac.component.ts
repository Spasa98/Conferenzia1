import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ConfirmationService, MessageService } from 'primeng/api';
import { AuthService } from 'src/app/services/auth.service';
import { PredavacSelectOServiceService } from 'src/app/services/predavac-select-o-service.service';

@Component({
  selector: 'app-izmeni-profil-predavac',
  templateUrl: './izmeni-profil-predavac.component.html',
  styleUrls: ['./izmeni-profil-predavac.component.scss']
})

export class IzmeniProfilPredavacComponent implements OnInit {
  form!: FormGroup;
  predavac:any | undefined;
  predavacID:number=this.authService.userValue.id;

  constructor(private httpClient: HttpClient,
    public predavacService: PredavacSelectOServiceService,
    private fb: FormBuilder,
    public authService: AuthService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService)
  { }

  ngOnInit(): void {
    this.predavacService.getPredavaca(this.predavacID).subscribe((predavac) => console.log(this.predavac = predavac));
    this.form = this.fb.group({
      ime:'',
      prezime: '',
      opis: '',
      email: ["",Validators.email],
      lozinka: '',
      telefon: '',
      grad: '',
    })

    console.log(this.form.get('email'));
  }

  submit() {
    if(this.form.getRawValue().ime==""){
      this.form.patchValue({ime: this.predavac[0].ime});
    }
    if(this.form.getRawValue().prezime==""){
      this.form.patchValue({prezime: this.predavac[0].prezime});
    }
    if(this.form.getRawValue().email==""){
      this.form.patchValue({email: this.predavac[0].email});
    }
    if(this.form.getRawValue().telefon==""){
      this.form.patchValue({telefon: this.predavac[0].telefon});
    }
    if(this.form.getRawValue().lozinka==""){
      this.form.patchValue({lozinka: this.predavac[0].lozinka});
    }
    if(this.form.getRawValue().opis==""){
      this.form.patchValue({opis: this.predavac[0].opis});
    }
    if(this.form.getRawValue().grad==""){
      this.form.patchValue({grad: this.predavac[0].grad});
    }
    
    this.confirmationService.confirm({
      message: 'Da li ste sigurni da želite da izvršite ovu radnju?',
      accept: () => {
        this.httpClient.put(`https://localhost:5001/Predavac/IzmenitiPredavacaNova/${this.predavacID}`,this.form.getRawValue()).subscribe({
          next: res=>{
            this.messageService.add({key: 'br', severity:'success', summary: 'Uspešno', detail: 'Izmenili ste profil!'});
          },
          error: err=>{
            this.messageService.add({key: 'br', severity:'error', summary: 'Neuspešno', detail: 'Pokušajte ponovo, došlo je do greške!'});
          }
        });
      }
    });
  }
}
