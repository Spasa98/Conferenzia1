import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
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

export class PredavanjaServiceService {
  
  private sort=0;
  constructor(private http:HttpClient) { }

  getPredavanja(pageId:number,sortiraj:number): Observable<predavanja[]> {
    return this.http.get<predavanja[]>(`https://localhost:5001/Predavanje/VratiSvaPredavanja?pagenumber=${pageId}&pom=${sortiraj}`);
    // ?page=${pageId}
  }
  getPastFutur(pageId:number,pomocna:string): Observable<predavanja[]> {
    return this.http.get<predavanja[]>(`https://localhost:5001/Predavanje/${pomocna}?pagenumber=${pageId}`);
    // ?page=${pageId}
  }
  PredavanjaPredavaca(arg:string,id:number,pagenumber:number,elPerPage:number): Observable<predavanja[]> {
    return this.http.get<predavanja[]>('https://localhost:5001/Predavanje/Prikazi'+`${arg}`+'PredavanjaPoPredavacu/'+`${id}`+`?pagenumber=${pagenumber}&elPerPage=${elPerPage}`);
  }
  getPredavanjaBezPredavacaPoOblasti(pageId:number,predavacId:number): Observable<predavanja[]> {
    return this.http.get<predavanja[]>(`https://localhost:5001/Predavanje/PredavanjaZaKojaJeMogucZahtev?pagenumber=${pageId}&pid=${predavacId}`);
  }
  PredavanjaSlusaoca(arg:string,id:number): Observable<predavanja[]> {
    return this.http.get<predavanja[]>('https://localhost:5001/Slusalac/'+`${arg}`+'Predavanja/'+`${id}`);
  }
  PredavanjaBezZahteva(page:number,id:number): Observable<predavanja[]> {
    return this.http.get<predavanja[]>(`https://localhost:5001/Slusalac/NeprijavljenaPredavanja/${id}/${page}`);
  }
  getPredavanjaJednogPredavaca(predavacID:number,pagenumber:number,elperpage:number): Observable<predavanja[]> {
    return this.http.get<predavanja[]>(`https://localhost:5001/Predavanje/PrikaziPredavanjaPoPredavacu/`+`${predavacID}`+`?pagenumber=${pagenumber}&elPerPage=${elperpage}`);
  }
  IzbrisatiPredavanje(arg:number)
  {
    return this.http.delete('https://localhost:5001/Predavanje/IzbrisatiPredavanje/'+`${arg}`);
  }
  getJednoPredavanje(predavanjeID:number):Observable<predavanja> 
  {
    return this.http.get<predavanja>(`https://localhost:5001/Predavanje/JednoPredavanje/`+`${predavanjeID}`);
  }
  getJednoPredavanjeBezPredavaca(predavanjeID:number):Observable<predavanja> 
  {
    return this.http.get<predavanja>(`https://localhost:5001/Predavanje/JednoPredavanjeBezPredavaca/`+`${predavanjeID}`);
  }
  searchPretragaPredavanja(text:string):Observable<predavanja>
  {
    return this.http.get<predavanja>(`https://localhost:5001/Predavanje/SearchPretragaPredavanja?searchString=${text}`);
  }
  ukloniPredavaca(id:number):Observable<predavanja>
  {
    return this.http.put<predavanja>(`https://localhost:5001/Predavac/UkloniPredavacaSaPredavanja?pid=${id}`,{});
  }
  DodajPredavacaNaPredavanje(id:number,predavacID:number):Observable<predavanja>
  {
    return this.http.put<predavanja>(`https://localhost:5001/Predavac/DodajPredavacaNaPredavanje?pid=${id}&predavacid=${predavacID}`,{});
  }
}
// IzbrisatiPredavanje/{id}
// JednoPredavanjeBezPredavaca