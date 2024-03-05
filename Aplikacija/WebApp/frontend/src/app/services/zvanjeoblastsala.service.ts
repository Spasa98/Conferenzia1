import { Injectable } from '@angular/core';
import {HttpClient,HttpHeaders} from '@angular/common/http'
import {oblast} from '../oblast';
import {zvanje} from '../zvanje';
import {sala} from '../sala';
import { Observable } from 'rxjs';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
  }),
};

@Injectable({
  providedIn: 'root'
})
export class ZvanjeOvlastSalaService {
  // private apiUrlOblast = 'https://localhost:5001/Predavac/PrikaziOblasti';
  private apiUrlOblast = 'https://localhost:5001/Predavac/PrikaziOblasti';
  private apiUrlZvanje = 'https://localhost:5001/Predavac/PrikaziZvanja';
  private apiUrlSala = 'https://localhost:5001/Predavac/PrikaziSale';

  constructor(private http:HttpClient) { }

  getOblast(): Observable<oblast[]> {
    return this.http.get<oblast[]>('https://localhost:5001/Predavac/PrikaziOblasti');
  }
  getZvanje(): Observable<zvanje[]> {
    return this.http.get<zvanje[]>('https://localhost:5001/Predavac/PrikaziZvanja');
  }
  getSala(): Observable<sala[]> {
    return this.http.get<sala[]>('https://localhost:5001/Predavac/PrikaziSale');
  }

  dodajOblast(arg:string): Observable<oblast[]> {
    return this.http.post<oblast[]>(`https://localhost:5001/ZvanjeOblast/DodajOblast?name=${arg}`,{});
  }
  dodajZvanje(arg:string): Observable<zvanje[]> {
    return this.http.post<zvanje[]>(`https://localhost:5001/ZvanjeOblast/DodajZvanje?zvanje=${arg}`,{});
  }
  // getZvanje(): Observable<zvanje[]> {
  //   return this.http.get<zvanje[]>(this.apiUrlZvanje);
  // }

  // getSala(): Observable<sala[]> {
  //   return this.http.get<sala[]>(this.apiUrlSala);
  // }
}
