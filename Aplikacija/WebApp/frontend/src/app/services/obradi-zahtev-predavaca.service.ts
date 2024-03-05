import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { organizator } from '../organizator';

@Injectable({
  providedIn: 'root'
})
export class ObradiZahtevPredavacaService {

  constructor(private http:HttpClient) { }
  vratiPredavacee() {
    // return this.http.post(`https://localhost:5001/Predavanje/DodajPredavanjeBezPredavaca/`,);
  }
}
