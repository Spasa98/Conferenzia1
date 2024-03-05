import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { organizator } from '../organizator';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
  }),
};

@Injectable({
  providedIn: 'root'
})
export class OrganizatorService {

  constructor(private http:HttpClient) { }

  getOrganizatora(organizatorId:number): Observable<organizator[]> {
    return this.http.get<organizator[]>(`https://localhost:5001/ZvanjeOblast/VratiJednogOrganizatora?idnumber=${organizatorId}`);
  }
}
