import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Message, MessageService, PrimeNGConfig } from 'primeng/api';
import { oblast } from 'src/app/oblast';
import { predavaci } from 'src/app/predavaci';
import { sala } from 'src/app/sala';
import { PredavacSelectOServiceService } from 'src/app/services/predavac-select-o-service.service';
import { ZvanjeOvlastSalaService } from 'src/app/services/zvanjeoblastsala.service';
import {DatePipe} from '@angular/common';
import { AuthService } from 'src/app/services/auth.service';
import { PredavanjaServiceService } from 'src/app/services/predavanja-service.service';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';


@Component({
  selector: 'app-izmeni-predavanje',
  templateUrl: './izmeni-predavanje.component.html',
  styleUrls: ['./izmeni-predavanje.component.scss'],
  providers: [MessageService,DatePipe]
})
export class IzmeniPredavanjeComponent implements OnInit {
  form!: FormGroup;
  msgs1!: Message[];
  msgs2!: Message[];

  oblasti: oblast[]=[];
  sale: sala[]=[];
  predavaci: predavaci[]=[];
  predavac:any | undefined;
  predavanje:any | undefined;
  predavacID:number=this.authService.userValue.id;


  constructor(private fb: FormBuilder,
    private httpClient:HttpClient,
    private zosService:ZvanjeOvlastSalaService,
    private predavacService:PredavacSelectOServiceService,
    private predavanjeService:PredavanjaServiceService,
    private messageService: MessageService,
    private primengConfig: PrimeNGConfig,
    public authService: AuthService,
    public ref: DynamicDialogRef, 
    public config: DynamicDialogConfig,
    private datePipe:DatePipe)
    {
      this.oblasti = [];
      this.sale = [];
      this.predavaci=[];
    }

    ngOnInit(): void {
      this.form = this.fb.group({
        naziv:'',
        opis: '',
        datum: ' ',
        salaID: '',
        oblastID: '',
        predavacID: '',
        pom:false
      }
       ) 
     
      this.zosService.getOblast().subscribe((oblast) => (this.oblasti = oblast));
      this.zosService.getSala().subscribe((sala) => (this.sale = sala));
      this.predavacService.vratiSvePredavace().subscribe((predavac) => (this.predavaci = predavac));
      if(this.config?.data?.predavacIme!==' ')
      {
        this.getPredavanje();
      }
      else
        this.getPredavanjeBezPredavaca();
      console.log(this.config?.data)
  
  
    }
    submit() {
        this.form.value.datum=this.datePipe.transform(this.form.value.datum,'yyyy-MM-ddTHH:mm:ss')
        console.log(this.form.value.datum);
        this.httpClient.put(`https://localhost:5001/Predavanje/IzmeniPredavanje?predavanjeid=${this.predavanje.id}&predavacid=${this.form.value.predavacID}&naziv=${this.form.value.naziv}&datum=${this.form.value.datum}&Opis=${this.form.value.opis}&OblastID=${this.form.value.oblastID}&SalaID=${this.form.value.salaID}`,this.form.getRawValue()).subscribe((success)=>{
    
        },(error)=>{
          console.log(error);
        });
    }
    showViaService() {
      this.messageService.add({severity:'succes', summary:'Service Message', detail:'Via MessageService'});
    }
    getPredavanje()
    {
      this.predavanjeService.getJednoPredavanje(this.config?.data?.idpomocni).subscribe((predavanja) => {
        this.predavanje = predavanja
        console.log("predavanje",this.predavanje)
        this.form.patchValue({
          datum:new Date(this.predavanje?.ceoDatum ),
          opis:this.predavanje?.opis,
          naziv:this.predavanje?.naziv,
          oblastID:this.predavanje?.oblastID,
          salaID:this.predavanje.salaID
        })
        if(this.form.value.predavac!=="")
        { 
          this.form.patchValue({
            predavacID:this.predavanje?.predavacID,
          })
        }
      });
    }
    getPredavanjeBezPredavaca()
    {
      this.predavanjeService.getJednoPredavanjeBezPredavaca(this.config?.data?.idpomocni).subscribe((predavanja) => {
        this.predavanje = predavanja
        console.log("predavanje",this.predavanje)
        this.form.patchValue({
          datum:new Date(this.predavanje?.ceoDatum ),
          opis:this.predavanje?.opis,
          naziv:this.predavanje?.naziv,
          oblastID:this.predavanje?.oblastID,
          salaID:this.predavanje.salaID
        })
      });
    }
  }
  
