import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthService } from 'src/app/services/auth.service';
import { FormGroup,FormBuilder } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { DynamicDialogConfig } from 'primeng/dynamicdialog';

@Component({
  selector: 'app-report-form',
  templateUrl: './report-form.component.html',
  styleUrls: ['./report-form.component.scss']
})
export class ReportFormComponent implements OnInit {
text2:string="rrrrrr";
predavacId:number=this.authService.userValue.id;
form!: FormGroup;
id!: number;


  constructor(private route: ActivatedRoute,private fb: FormBuilder,private httpClient:HttpClient,public authService: AuthService, public config: DynamicDialogConfig,) { 
    
  }

  ngOnInit(): void {
    this.form = this.fb.group({
      text:'',

    })
    console.log(this.config?.data)
  }
  report(){
      console.log(this.form.value.text)
      this.httpClient.post(`https://localhost:5001/ReportFeedback/ReportFeedback/${this.config?.data?.fedback}/${this.form.value.text}`,{}).subscribe((success)=>{
      },(error)=>{
        console.log(error);
      });
  }

}
