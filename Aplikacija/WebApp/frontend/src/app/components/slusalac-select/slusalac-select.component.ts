import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { SlusalacService } from 'src/app/services/slusalac.service';
import { slusalac } from 'src/app/slusalac';

@Component({
  selector: 'app-slusalac-select',
  templateUrl: './slusalac-select.component.html',
  styleUrls: ['./slusalac-select.component.scss']
})
export class SlusalacSelectComponent implements OnInit {

  constructor(private slusalacServise:SlusalacService, public authService: AuthService) { }
  brstr:number=1
  index=["ID","Ime","Email","Telefon"]
  slusalac:slusalac[]=[];
  strana:number=1;
  ngOnInit(): void {
    this.getSlusaoce(1);
  }

  povecaj(event:any): void {
     this.getSlusaoce(event.page+1);
     console.log(event);
     this.strana=event.page+1
  }
  getSlusaoce(pageId:number):void {
    this.getBrStr();
     this.slusalacServise.getSlusaoce(pageId).subscribe((slusalac) => {
      this.slusalac = slusalac
      console.log('GET TASKS',slusalac)
    });
  }
  getBrStr():void {

    this.slusalacServise.getBrojStrana().subscribe((brstr) => {
     this.brstr = brstr
     console.log('GET TASKS',this.brstr)
   });
 }
 obrisi(id:number){
  this.slusalacServise.obrisiSlusaoca(id).subscribe((arg) => {this.getSlusaoce(this.strana)
    console.log('GET TASKS',this.brstr)
  });
 }
}
