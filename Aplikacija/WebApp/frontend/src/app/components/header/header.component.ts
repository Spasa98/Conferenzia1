import { Component, OnInit } from '@angular/core';
import {MegaMenuItem,MenuItem} from 'primeng/api';
import { Injectable } from '@angular/core';
import {MenubarModule} from 'primeng/menubar';
import { AuthService } from 'src/app/services/auth.service';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { NumberValueAccessor } from '@angular/forms';
import { PredavacSelectOServiceService } from 'src/app/services/predavac-select-o-service.service';
import { SlusalacService } from 'src/app/services/slusalac.service';
import { OrganizatorService } from 'src/app/services/organizator.service';
import { ConfirmationService } from 'primeng/api';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
   subscriptions: Subscription[] = [];
   items!: MenuItem[];
   pom:number=1;
   organizator:any=" ";
   predavac:any=" ";
   slusalac:any=" ";
   id!: number;
  
   constructor(public authService: AuthService, private router: Router,
    private predavacService:PredavacSelectOServiceService,
    private slusalacService:SlusalacService,
    private organizatorService:OrganizatorService,
    private confirmationService: ConfirmationService)
    {}

   ngOnInit() {
    
    console.log("ovdee:",this.authService.userValue)

    this.authService.loginStatusChange().subscribe(userSubject=>{

    this.items = [
      {
          label: 'O nama', routerLink:['/home']
      },
      {
          label: 'Predavanja', routerLink:['/predavanja']
      },
      {
          label: 'Predavaci', routerLink:['/predavaci']
      },
      
    ];

  if(this.authService?.userValue?.role==="Organizator"){
        this.items = [
            {
                label: 'O nama', routerLink:['/home']
            },
            {
                label: 'Predavanja', routerLink: ['/predavanjaO']
            },
            {
                label: 'Korisnici',
                items: [
                  {label: 'Predavaci', routerLink: ['/predavaciO']},
                  {label: 'Slusaoci', routerLink: ['/slusaociO']},
              ]
            },
            {
              label: 'Zahtevi',
              items: [
                {label: 'Zahtevi predavaca', routerLink:['zahteviPredavaca']},
                {label: 'Zahtevi slusaoca', routerLink:['zahteviSlusaoca']}
              ]
            },
            {
              label: 'Feedback-s',routerLink:['reportfeedback'],
            }
          ];
  } 
  else if(this.authService?.userValue?.role==="Predavac"){
    // console.log("ovdeeeeeee",this.authService.userValue.role);
     
    this.items = [
        {
            label: 'O nama', routerLink:['/home']
        },
        {
            label: 'Predavanja ',routerLink:['/predavanja']

        },
        {
            label: 'Predavaci', routerLink:['/predavaci']
        },
      ];
  }
  else if(this.authService?.userValue?.role==="Slusalac"){
    // console.log("ovdeeeeeee",this.authService.userValue.role);
     
    this.items = [
        
        {
            label: 'O nama', routerLink:['/home']
        },
        {
            label: 'Predavanja ', routerLink:['/predavanja']
        },
        {
            label: 'Predavaci', routerLink:['/predavaci']
        },
        {
          label: 'Predavanja prijava', routerLink:['/prijavaZaPredavanja']
        },
        // {
        //   label: 'Predavanja prijava', routerLink:['/mojaPredavanja/:id']
        // },

      ];
  }
})
}
  onClick()
  {
    this.confirmationService.confirm({
      message: 'Da li ste sigurni da želite da se odjavite?',
      accept: () => {
        this.authService.logout();
        console.log('OVO JE IZ HOME',this.authService.userValue)
        this.router.navigate(['/home']);
      }
    });

    // this.authService.logout();
    // console.log('OVO JE IZ HOME',this.authService.userValue)
    // this.router.navigate(['/home']);
  }

  onLogin()
  {
    
  }

  // onProfileOrganizator():void
  // {
  //   this.organizatorService.getOrganizatora(this.authService?.userValue?.id).subscribe((organizatori) => {
  //     this.organizator = organizatori[0]
  //     console.log(this.organizator)
  //   });
  // }

  // onProfilePredavac():void
  // {
  //   this.predavacService.getPredavaca(this.authService?.userValue?.id).subscribe((predavaci) => {
  //     this.predavac = predavaci[0]
  //     console.log(this.predavac)
  //   });
  // }

  // onProfileSlusalac():void
  // {
  //   this.slusalacService.getSlusalac(this.authService?.userValue?.id).subscribe((slusaoci) => {
  //     this.slusalac = slusaoci[0]
  //     console.log(this.slusalac)
  //   });
  // }
}
