import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { zahtevpredavac } from '../zahtevpredavac';
import { zahtevslusalac } from '../zahtevslusalac';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
  }),
};

@Injectable({
  providedIn: 'root'
})
export class ZahtevServiceService {

  constructor(private http:HttpClient) { }


  getBrojStrana(str:string): Observable<number> {
    return this.http.get<number>(`https://localhost:5001/ZahtevSlusalac/BrojStranica/${str}`);
  }
  getBrojStranaZ(str:string,id:number): Observable<number> {
    return this.http.get<number>(`https://localhost:5001/Slusalac/BrojStranica/${str}/${id}`);
  }
  getZahtevSlusalac(page:number,txt:string): Observable<zahtevslusalac[]> {
    return this.http.get<zahtevslusalac[]>(`https://localhost:5001/ZahtevSlusalac/Prikazi${txt}ZahteveSlusaoca/${page}`);
  }
  getZahtevTrazeni(slusalacid:number,status:string,str:number): Observable<any[]> {
    return this.http.get<any[]>(`https://localhost:5001/Slusalac/TrazeniZahtevi/${slusalacid}/${status}/${str}`);
  }
  obradaZahteva(txt:string,zahtevId:number) {
    return this.http.put(`https://localhost:5001/ZahtevSlusalac/${txt}Zahteva/${zahtevId}`,{});
  }
  izbrisiZahtevSlusaoca(arg:number){
    return this.http.delete(`https://localhost:5001/ZahtevSlusalac/ObrisiZahtev/${arg}`)

  }

//   getZahtevSlusalac(): Observable<zahtevslusalac[]> {
//     return this.http.get<zahtevslusalac[]>('https://localhost:5001/ZahtevSlusalac/PrikaziSveZahteveSlusaoca');
// >>>>>>> 8969479a70ecca94cfb2ca5cb69467b0b25b07d9
//   }
  getZahtevPredavac(): Observable<zahtevpredavac[]> {
    return this.http.get<zahtevpredavac[]>('https://localhost:5001/ZahtevPredavac/PrikaziSveZahtevePredavaca');
  }
  getSveZahteveSlusaocaPerPage(pageId:number): Observable<zahtevslusalac[]> {
    return this.http.get<zahtevslusalac[]>(`https://localhost:5001/ZahtevSlusalac/VratiSveZahteveSlusaocaPerPage?pagenumber=${pageId}`);
    // ?page=${pageId}
  }
  getSveZahtevePredavacaPerPage(pageId:number): Observable<zahtevpredavac[]> {
    return this.http.get<zahtevpredavac[]>(`https://localhost:5001/ZahtevPredavac/VratiSveZahtevePredavacaPerPage?pagenumber=${pageId}`);
    // ?page=${pageId}
  }

  postNoviZahtev(PredavacID:number,PredavanjeID:number,tipzahteva:string)
  { 
    return this.http.post(`https://localhost:5001/ZahtevPredavac/DodajZahtevPredavaca/`+`${PredavacID}`+`/${PredavanjeID}`+`/${tipzahteva}`,{responseType:'text'});
  }
  odobriZahtevPredavaca(ZahtevID:number)
  {
    return this.http.put(`https://localhost:5001/ZahtevPredavac/OdobriZahtevPredavaca?ZahtevID=${ZahtevID}`,{})
  }
  odbijZahtevPredavaca(ZahtevID:number)
  {
    return this.http.put(`https://localhost:5001/ZahtevPredavac/OdbijZahtevPredavaca?ZahtevID=${ZahtevID}`,{})
  }
  getSveZahteveJednogPredavaca(predavacid:number,pagenumber:number): Observable<zahtevpredavac[]>
  {
    return this.http.get<zahtevpredavac[]>(`https://localhost:5001/ZahtevPredavac/PrikaziZahtevePredavaca/`+`${predavacid}`+`/${pagenumber}`);
  }
  postNoviZahtevSlusalac(predavanjeId:number,slusalacId:number)
  { 
    return this.http.post(`https://localhost:5001/ZahtevSlusalac/NovZahtev/${slusalacId}/${predavanjeId}`,{});
  }
  izbrisatiZahtev(arg:number)
  {
    return this.http.delete('https://localhost:5001/ZahtevPredavac/IzbrisatiZahtev/'+`${arg}`);
  }
}

