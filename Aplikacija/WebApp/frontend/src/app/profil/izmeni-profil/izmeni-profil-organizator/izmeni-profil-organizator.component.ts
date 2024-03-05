import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ConfirmationService, MessageService } from 'primeng/api';
import { AuthService } from 'src/app/services/auth.service';
import { OrganizatorService } from 'src/app/services/organizator.service';

@Component({
  selector: 'app-izmeni-profil-organizator',
  templateUrl: './izmeni-profil-organizator.component.html',
  styleUrls: ['./izmeni-profil-organizator.component.scss']
})

export class IzmeniProfilOrganizatorComponent implements OnInit {
  form!: FormGroup;
  organizator:any | undefined;
  organizatorID:number=this.authService.userValue.id;

  constructor(private httpClient: HttpClient,
    private fb: FormBuilder,
    public authService: AuthService,
    public organizatorService: OrganizatorService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService)
  { }

  ngOnInit(): void {
    this.organizatorService.getOrganizatora(this.organizatorID).subscribe((organizator) => console.log(this.organizator = organizator));
    this.form = this.fb.group({
      ime: '',
      prezime: '',
      email: ["",Validators.email],
      lozinka:'',
    })

    console.log(this.form.get('email'));
  }

  submit() {
    if(this.form.getRawValue().ime==""){
      this.form.patchValue({ime: this.organizator[0].ime});
    }
    if(this.form.getRawValue().prezime==""){
      this.form.patchValue({prezime: this.organizator[0].prezime});
    }
    if(this.form.getRawValue().email==""){
      this.form.patchValue({email: this.organizator[0].email});
    }
    if(this.form.getRawValue().lozinka==""){
      this.form.patchValue({lozinka: this.organizator[0].lozinka});
    }
    
    this.confirmationService.confirm({
      message: 'Da li ste sigurni da želite da izvršite ovu radnju?',
      accept: () => {
        this.httpClient.put(`https://localhost:5001/ZvanjeOblast/IzmenitiOrganizatora/${this.organizatorID}`,this.form.getRawValue()).subscribe({
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
