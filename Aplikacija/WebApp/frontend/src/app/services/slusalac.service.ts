import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { slusalac } from '../slusalac';
import { predavanja } from '../predavanja';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
  }),
};

@Injectable({
  providedIn: 'root'
})
export class SlusalacService {

  constructor(private http:HttpClient) { }

  getSlusalac(slusalacId:number): Observable<slusalac[]> {
    return this.http.get<slusalac[]>(`https://localhost:5001/Slusalac/VratiJednogSlusaoca?idnumber=${slusalacId}`);
  }
  getSlusalac2(slusalacId:number): any {
    return this.http.get<any>(`https://localhost:5001/Slusalac/VratiJednogSlusaoca?idnumber=${slusalacId}`);
  }
  PredavanjaSlusaoca(id:number,strana:number): Observable<predavanja[]> {
    return this.http.get<predavanja[]>(`https://localhost:5001/Slusalac/PredavanjaProfilSlusaoca/${id}/${strana}`);
  }

  getSlusaoce(pageId:number): Observable<slusalac[]> {
    return this.http.get<slusalac[]>(`https://localhost:5001/Slusalac/VratiSveSlusaocePerPage?pagenumber=${pageId}`);
    // ?page=${pageId}
  }
  getBrojStrana(): Observable<number> {
    return this.http.get<number>(`https://localhost:5001/Slusalac/BrStranaSlusaoca`);
  }
  obrisiSlusaoca(arg:number){
    return this.http.delete(`https://localhost:5001/Slusalac/IzbrisiSlusaoca/${arg}`)}
}
