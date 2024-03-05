import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MessageService } from 'primeng/api';
import { DynamicDialogConfig } from 'primeng/dynamicdialog';
import { predavanja } from 'src/app/predavanja';
import { PredavanjaServiceService } from 'src/app/services/predavanja-service.service';
import { DynamicDialogRef } from 'primeng/dynamicdialog';

@Component({
  selector: 'app-jedno-predavanje',
  templateUrl: './jedno-predavanje.component.html',
  styleUrls: ['./jedno-predavanje.component.scss'],
  providers: [MessageService,DatePipe]
})
export class JednoPredavanjeComponent implements OnInit {

  predavanje: any;
  idPredavanja!: number;

  constructor(
    public predavanjeService: PredavanjaServiceService,
    private route: ActivatedRoute, 
    private messageService: MessageService,
    public config: DynamicDialogConfig) { }

  ngOnInit(): void {  
    if(this.config?.data?.predavacime!==' ')
      {
        this.getJednoPredavanje(this.config?.data.idpomocni);
      }
      else
        this.getPredavanjeBezPredavaca(this.config?.data.idpomocni);
      console.log(this.config?.data)
      // this.getPredavanjeBezPredavaca(this.config?.data.idpomocni);
    // this.id
    console.log('ID PREDAVANJA',this.idPredavanja);
    console.log("ovdeeee",this.config?.data.idpomocni)
  }
  
  getPredavanjeBezPredavaca(predavanje:number) {
    this.predavanjeService.getJednoPredavanjeBezPredavaca(predavanje).subscribe((predavanja) => {
      this.predavanje = predavanja
      console.log("predavanje",this.predavanje)
    });
  }

  getJednoPredavanje(predavanje:number)
  {
    this.predavanjeService.getJednoPredavanje(predavanje).subscribe((predavanja) => {
      this.predavanje = predavanja
      console.log("predavanje",this.predavanje)
    });
  }

}
