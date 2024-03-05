import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ConfirmationService, MessageService } from 'primeng/api';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { DodajorganizatoraComponent } from 'src/app/components/dodajorganizatora/dodajorganizatora.component';
import { AuthService } from 'src/app/services/auth.service';
import { OrganizatorService } from 'src/app/services/organizator.service';
import { ZvanjeOvlastSalaService } from 'src/app/services/zvanjeoblastsala.service';
import { IzmeniProfilOrganizatorComponent } from '../izmeni-profil/izmeni-profil-organizator/izmeni-profil-organizator.component';

@Component({
  selector: 'app-profil-organizator',
  templateUrl: './profil-organizator.component.html',
  styleUrls: ['./profil-organizator.component.scss']
})
export class ProfilOrganizatorComponent implements OnInit {
  formzvanje!: FormGroup;
  formoblast!: FormGroup;
  organizator!: any;
  id!: number;
  ref!: DynamicDialogRef;

  constructor(private fb: FormBuilder,
    private route: ActivatedRoute,
    private httpClient: HttpClient,
    public authService: AuthService,
    private orgService: OrganizatorService,
    public dialogService: DialogService,
    public messageService: MessageService,
    private confirmationService: ConfirmationService,
    private zvanjeoblast:ZvanjeOvlastSalaService)
  { }

  ngOnInit(): void {
    this.id = this.route.snapshot.params['id']
    console.log('ID KORINISKA',this.id)

    this.formoblast = this.fb.group({
      oblast: '',
    })
    this.formzvanje = this.fb.group({
      zvanje: '',
    })

    this.getOrganizator(this.id);
  }

  getOrganizator(organizatorId:number):void
  {
    this.orgService.getOrganizatora(organizatorId).subscribe((organizatori) => {
      this.organizator = organizatori[0]
      console.log(this.organizator)
    });
  }

  show() {
    this.ref = this.dialogService.open(IzmeniProfilOrganizatorComponent, {
      header: 'Izmeni profil',
      width: '45%'
    });
    this.ref.onClose.subscribe(res=>
    {
      this.getOrganizator(this.organizator.id);
    })
  }

  dodajOblast() {
    this.confirmationService.confirm({
      message: 'Da li ste sigurni da želite da izvršite ovu radnju?',
      accept: () => {
        this.zvanjeoblast.dodajOblast(this.formoblast.value.oblast).subscribe({
          next: res=>{
            this.messageService.add({key: 'br', severity:'success', summary: 'Uspešno', detail: 'Dodali ste oblast!'});
          },
          error: err=>{
            this.messageService.add({key: 'br', severity:'error', summary: 'Neuspešno', detail: 'Pokušajte ponovo, došlo je do greške!'});
        }
      });
    }
    });
  }

  dodajZvanje() {
    this.confirmationService.confirm({
      message: 'Da li ste sigurni da želite da izvršite ovu radnju?',
      accept: () => {
        this.zvanjeoblast.dodajZvanje(this.formzvanje.value.zvanje).subscribe({
          next: res=>{
            this.messageService.add({key: 'br', severity:'success', summary: 'Uspešno', detail: 'Dodali ste zvanje!'});
          },
          error: err=>{
            this.messageService.add({key: 'br', severity:'error', summary: 'Neuspešno', detail: 'Pokušajte ponovo, došlo je do greške!'});
        }
      });
    }
    });
  }

  dodajOrganizatora() {
    this.ref = this.dialogService.open(DodajorganizatoraComponent, {
      header: 'Dodaj novog organizatora',
      width: '35%'
    });
    // this.ref.onClose.subscribe(res=>
    // {
    //   this.getPredavac(this.predavac.id);
    // })
  }

}
