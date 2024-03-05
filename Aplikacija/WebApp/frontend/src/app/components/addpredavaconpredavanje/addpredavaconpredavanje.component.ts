import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ConfirmationService, MessageService,} from 'primeng/api';
import { DialogService, DynamicDialogConfig } from 'primeng/dynamicdialog';
import { predavaci } from 'src/app/predavaci';
import { PredavacSelectOServiceService } from 'src/app/services/predavac-select-o-service.service';
import { PredavanjaServiceService } from 'src/app/services/predavanja-service.service';

@Component({
  selector: 'app-addpredavaconpredavanje',
  templateUrl: './addpredavaconpredavanje.component.html',
  styleUrls: ['./addpredavaconpredavanje.component.scss']
})
export class AddpredavaconpredavanjeComponent implements OnInit {
  predavaci: predavaci[]=[];
  form!: FormGroup;
  constructor(
  private predavanjeService:PredavanjaServiceService,
  private fb: FormBuilder,
  private httpClient:HttpClient,
  private predavacService:PredavacSelectOServiceService,
  public config: DynamicDialogConfig,
  public dialogService: DialogService,
  public messageService: MessageService,
  private confirmationService: ConfirmationService) 
  {
    this.predavaci = [];
  }

  ngOnInit(): void {
    this.form = this.fb.group({
      predavacID: '',
    })
    this.predavacService.vratiSvePredavace().subscribe((predavac) => (this.predavaci = predavac));
  }
  DodajPredavaca()
  {
    this.confirmationService.confirm({
      message: 'Da li ste sigurni da želite da izvršite ovu radnju?',
      accept: () => {
        this.predavanjeService.DodajPredavacaNaPredavanje(this.config?.data?.idpomocni,this.form.value.predavacID).subscribe({
              next: res=>{
                this.messageService.add({key: 'br', severity:'success', summary: 'Uspešno', detail: 'Dodali ste predavaca na predavanje!'});
              },
              error: err=>{
                this.messageService.add({key: 'br', severity:'error', summary: 'Neuspešno', detail: 'Pokušajte ponovo, došlo je do greške!'});
              }
        });
      }
    });
  }
}
