import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FeedbackService } from 'src/app/services/feedback.service';

@Component({
  selector: 'app-organizator-report-feedback',
  templateUrl: './organizator-report-feedback.component.html',
  styleUrls: ['./organizator-report-feedback.component.scss']
})
export class OrganizatorReportFeedbackComponent implements OnInit {
strana:number=3;
  constructor(private fService:FeedbackService,private httpClient:HttpClient) { }
reportF:any=[];
  ngOnInit(): void {
    this.ucitaj();
  }
  delay(ms: number) {
    return new Promise( resolve => setTimeout(resolve, ms) );
}
  ucitaj(){
    this.fService.ucitajReports().subscribe((rep)=>console.log(this.reportF=rep));

  }
  
  obradi(rid:number,br:number){
    this.fService.odobri(rid,br).subscribe(q=>{this.ucitaj()}); 
    // this.delay(1000);   
    // this.ucitaj();
  }
  loadMore(){
    this.strana+=3;
    this.httpClient.get(`https://localhost:5001/ReportFeedback/vratiReports/${this.strana}`).subscribe((rep)=>{this.reportF=rep});
  }
}
