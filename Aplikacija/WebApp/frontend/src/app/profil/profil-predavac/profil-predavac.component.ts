import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs'
import {predavaci} from '../../predavaci';
import {PredavacSelectOServiceService} from '../../services/predavac-select-o-service.service';
import {AvatarModule} from 'primeng/avatar';
import {AvatarGroupModule} from 'primeng/avatargroup';
import { AuthService } from 'src/app/services/auth.service';
import { IzmeniProfilPredavacComponent } from '../izmeni-profil/izmeni-profil-predavac/izmeni-profil-predavac.component';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { MessageService } from 'primeng/api';
import { DodajFeedbackComponent } from 'src/app/components/dodaj-feedback/dodaj-feedback.component';

@Component({
  selector: 'app-profil-predavac',
  templateUrl: './profil-predavac.component.html',
  styleUrls: ['./profil-predavac.component.scss']
})
export class ProfilPredavacComponent implements OnInit {
  form!: FormGroup; 
  //zvanja!: Zvanje[];
  //oblasti!: Oblast[];
  //selektovanoZvanje: Zvanje;
  //selektovanaOblast: Oblast;
  predavac:any;
  id!: number;
  slovo!:number;
  ref!: DynamicDialogRef;
  pomocna:number=1;
  ocena!: number;


  constructor(private fb: FormBuilder,
    public authService:AuthService,
    private route: ActivatedRoute,
    private httpClient: HttpClient,
    private predavacService:PredavacSelectOServiceService,
    public dialogService: DialogService,
    public messageService: MessageService) { }

    odslusan:Boolean=false;
  ngOnInit(): void {
    this.id = this.route.snapshot.params['id']
    console.log('ID KORINISKA',this.id)
    this.form = this.fb.group({
      ime: '',
      prezime: '',
      email: '',
      telefon: '',
      grad: '',
      opis: '',

      zvanjeID: '',
      oblastID: ''
    }
    
  )
  
  this.getPredavac(this.id);

  }

  getPredavac(predavacId:number):void
  {
    this.predavacService.getPredavaca(predavacId).subscribe((predavaci) => {
      this.predavac = predavaci[0];
      this.odslusanP();
      console.log(this.predavac)
    });
  
  }
  odslusanP(){
    if(this.authService?.userValue?.role==="Slusalac"){
      this.httpClient.get<boolean>(`https://localhost:5001/Predavac/daLiJeOdslusan/${this.id}/${this.authService.userValue.id}`).subscribe((el)=>(this.odslusan=el))  
    }
  
  }

  show() {
    this.ref = this.dialogService.open(IzmeniProfilPredavacComponent, {
      header: 'Izmeni profil',
      width: '45%'
    });
    this.ref.onClose.subscribe(res=>
    {
      this.getPredavac(this.predavac.id);
    })
  }
  OstaviFeedback(predavacid:number) {
    this.ref = this.dialogService.open(DodajFeedbackComponent, {
      data:
      {
        pradid:predavacid,
      },
      header: 'Dodaj feedback',
      width: '40%'
    });
    this.ref.onClose.subscribe(res=>
    {
      this.getPredavac(this.predavac.id);
    })
  }

}
