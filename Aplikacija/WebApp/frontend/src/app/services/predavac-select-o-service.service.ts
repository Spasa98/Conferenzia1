import { Injectable } from '@angular/core';
import {HttpClient,HttpHeaders} from '@angular/common/http'
import {predavaci} from '../predavaci';
import { Observable } from 'rxjs';
import { predavanja } from '../predavanja';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
  }),
};

@Injectable({
  providedIn: 'root'
})
export class PredavacSelectOServiceService {


  // private apiUrl = 'https://localhost:5001/Predavac/VratiSvePredavace?{pagenumber}';

  constructor(private http:HttpClient) { }

  getPredavace(pageId:number): Observable<predavaci[]> {
    return this.http.get<predavaci[]>(`https://localhost:5001/Predavac/VratiSvePredavacePerPage?pagenumber=${pageId}`);
    // ?page=${pageId}
  }
  getPredavaca(predavacId:number): Observable<predavaci[]> {
    return this.http.get<predavaci[]>(`https://localhost:5001/Predavac/VratiJegdnogPredavaca?idnumber=${predavacId}`);
    // ?page=${pageId}
  }
  getPredavacaPredavanja(predavanjeId:number): Observable<predavaci[]> {
    return this.http.get<predavaci[]>(`https://localhost:5001/Predavanje/PredavacPredavanja/`+`${predavanjeId}`);
    // ?page=${pageId}
  }
  vratiSvePredavace(): Observable<predavaci[]> {
    return this.http.get<predavaci[]>("https://localhost:5001/Predavac/VratiSvePredavace");
  }
  searchPretragaPredavaca(text:string):Observable<predavaci[]>
  {
    return this.http.get<predavaci[]>(`https://localhost:5001/Predavac/SearchPretragaPredavaca?searchString=${text}`);
  }
  obrisiPredavaca(id:number):Observable<predavaci[]>
  {
    return this.http.delete<predavaci[]>(`https://localhost:5001/Predavac/IzbrisatiPredavaca/`+`${id}`);
  }
  
}