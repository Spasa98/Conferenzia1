import { Component, OnInit } from '@angular/core';
import { FormControl,FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SlusalacService } from 'src/app/services/slusalac.service';
import { slusalac } from 'src/app/slusalac';
import { HttpClient } from '@angular/common/http';
import { AuthService } from 'src/app/services/auth.service';
import { CommonModule, DatePipe } from '@angular/common';  
import { BrowserModule } from '@angular/platform-browser';
import { ConfirmationService, MessageService } from 'primeng/api';

@Component({
  selector: 'app-izmeni-profil-slusalac',
  templateUrl: './izmeni-profil-slusalac.component.html',
  styleUrls: ['./izmeni-profil-slusalac.component.scss'],
  providers: [DatePipe]
})
export class IzmeniProfilSlusalacComponent implements OnInit {
  form!: FormGroup;
  slusalac:any | undefined;
  slusalacID:number=this.authService.userValue.id;

  constructor(private httpClient: HttpClient,
    public slusalacService: SlusalacService,
    private fb: FormBuilder,
    public authService: AuthService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService,
    private datePipe:DatePipe)
  { }

  ngOnInit(): void {
    this.slusalacService.getSlusalac(this.slusalacID).subscribe((slusalac) => console.log(this.slusalac = slusalac));
    this.form = this.fb.group({
      ime: "",
      prezime: "",
      email: ["",Validators.email],
      telefon: "",
      lozinka: ''
      
    })

    console.log(this.form.get('email'));
  }

  submit() {
    
    if(this.form.getRawValue().ime==""){
      this.form.patchValue({ime: this.slusalac[0].ime});
    }
    if(this.form.getRawValue().prezime==""){
      this.form.patchValue({prezime: this.slusalac[0].prezime});
    }
    if(this.form.getRawValue().email==""){
      this.form.patchValue({email: this.slusalac[0].email});
    }

    if(this.form.getRawValue().telefon==""){
      this.form.patchValue({telefon: this.slusalac[0].telefon});
    }
    if(this.form.getRawValue().lozinka==""){
      this.form.patchValue({lozinka: this.slusalac[0].lozinka});
    }
    console.log("this.form.value.telefon.substring(4)",this.form.value.telefon.substring(4))
    if(this.form.value.telefon !== ''){
      // this.form.value.telefon  = this.form.value.telefon.substring(4)
      // parseInt(this.form.value.telefon)
    }

    this.confirmationService.confirm({
      message: 'Da li ste sigurni da želite da izvršite ovu radnju?',
      accept: () => {
        this.httpClient.put(`https://localhost:5001/Slusalac/IzmeniSlusaoca/${this.slusalacID}`,this.form.getRawValue()).subscribe({
          next: res=>{
            console.log("ovdeeeeee")
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
