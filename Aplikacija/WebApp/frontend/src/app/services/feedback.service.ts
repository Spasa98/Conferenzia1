import { Injectable } from '@angular/core';
import {HttpClient,HttpHeaders} from '@angular/common/http'
import { Observable } from 'rxjs';
import { feedback } from '../feedback';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
  }),
};

@Injectable({
  providedIn: 'root'
})
export class FeedbackService {

  constructor(private http:HttpClient) { }

  getFeedbacksJednogPredavaca(predavacid:number,pagenumber:number): Observable<feedback[]>
  {
    return this.http.get<feedback[]>(`https://localhost:5001/Feedback/VratiFeedbacksPredavaca/${predavacid}/${pagenumber}`);
  }
  odobri(rid:number,radnja:number): Observable<feedback[]>
  {
    return this.http.delete<feedback[]>(`https://localhost:5001/ReportFeedback/ObrisiFeedback/${rid}/${radnja}`);
  }
  ucitajReports(): Observable<any[]>
  {
    return this.http.get<any[]>(`https://localhost:5001/ReportFeedback/vratiReports`);
  }
}
