import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { MessageService } from 'primeng/api';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { AuthService } from 'src/app/services/auth.service';
import { SlusalacService } from 'src/app/services/slusalac.service';
import { IzmeniProfilSlusalacComponent } from '../izmeni-profil/izmeni-profil-slusalac/izmeni-profil-slusalac.component';


@Component({
  selector: 'app-profil-slusalac',
  templateUrl: './profil-slusalac.component.html',
  styleUrls: ['./profil-slusalac.component.scss']
})
export class ProfilSlusalacComponent implements OnInit {
  form!: FormGroup;
  slusalac: any;
  id!: number;
  ref!: DynamicDialogRef;
  pomocna:number=1;
  
  constructor(private fb: FormBuilder,
    private route: ActivatedRoute,
    public authService: AuthService,
    private httpClient: HttpClient,
    private slusalacService: SlusalacService,
    public dialogService: DialogService,
    public messageService: MessageService
    ) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.params['id']
    console.log('ID KORINISKA',this.id)

    this.form = this.fb.group({
      ime: '',
      prezime: '',
      email: '',
      lozinka: '',
      telefon: ''
    })

    this.getSlusalac(this.id);

  }

  getSlusalac(slusalacId:number):void
  {
    this.slusalacService.getSlusalac(slusalacId).subscribe((slusaoci) => {
      this.slusalac = slusaoci[0]
      console.log(this.slusalac)
    });
  }

  show() {
    this.ref = this.dialogService.open(IzmeniProfilSlusalacComponent, {
        header: 'Izmeni profil',
        width: '40%'
    });
    this.ref.onClose.subscribe(res=>
      {
        this.getSlusalac(this.slusalac.id);
      })
  }
}
