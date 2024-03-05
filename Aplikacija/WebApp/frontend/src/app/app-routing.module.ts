import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginPageComponent } from './components/login-page/login-page.component';
import { PredavacSelectComponent } from './components/predavac-select/predavac-select.component';
import { ProfilOrganizatorComponent } from './profil/profil-organizator/profil-organizator.component';
import { ProfilPredavacComponent } from './profil/profil-predavac/profil-predavac.component';
import { DodajFeedbackComponent } from './components/dodaj-feedback/dodaj-feedback.component';
import { ProfilSlusalacComponent } from './profil/profil-slusalac/profil-slusalac.component';
import { HomePageComponent } from './components/home-page/home-page.component';
import { PredavanjePredavacComponent } from './components/predavanja/predavanje-predavac/predavanje-predavac.component'
import { PredavanjaTabelaComponent } from './components/prikaz-tabela/predavanja-tabela/predavanja-tabela.component'
import { DodajpredavanjeComponent } from'./components/dodajpredavanje/dodajpredavanje.component';
import { ZahtevPredavacTabelaComponent } from './components/prikaz-tabela/zahtev-predavac-tabela/zahtev-predavac-tabela.component'
import { PredavanjaMogucaComponent } from './components/predavanja/predavanje-predavac/predavanja-moguca/predavanja-moguca.component'
import { AuthGuard } from './services/auth.guard';
import{PredavaciPocetnaComponent} from './components/predavaci-pocetna/predavaci-pocetna.component'
import { ZahtevSlusalacTabelaComponent } from './components/prikaz-tabela/zahtev-slusalac-tabela/zahtev-slusalac-tabela.component';
import { SlusalacSelectComponent } from './components/slusalac-select/slusalac-select.component';
import { IzgledProfilaPredavacComponent } from './profil/izgled-profila-predavac/izgled-profila-predavac.component';
import { IzgledProfilaSlusalacComponent } from './profil/izgled-profila-slusalac/izgled-profila-slusalac.component';
import { IzmeniPredavanjeComponent } from './components/izmeni-predavanje/izmeni-predavanje.component';
import { ReportFormComponent } from './profil/izgled-profila-predavac/report-form/report-form.component';
import { OrganizatorReportFeedbackComponent } from './components/prikaz-tabela/organizator-report-feedback/organizator-report-feedback.component';
import { JednoPredavanjeComponent } from './components/jedno-predavanje/jedno-predavanje.component';
import { DodajpredavacaComponent } from './components/dodajpredavaca/dodajpredavaca.component';
import { AddpredavaconpredavanjeComponent } from './components/addpredavaconpredavanje/addpredavaconpredavanje.component';
import { Error404Component } from './components/error404/error404.component';
import { PredavanjaSlusalacMogucaComponent } from './components/predavanja/predavanja-slusalac-moguca/predavanja-slusalac-moguca.component';



const routes: Routes = [
  {

    path: 'login',
    component: LoginPageComponent
  },
  {
    path: 'home',
    component: HomePageComponent,
    //canActivate: [AuthGuard],
  },
  {
    path:'predavanja',
    component:PredavanjePredavacComponent,
 
  },
  {
    path:'predavaci',
    component:PredavaciPocetnaComponent,

  },
  {
    path:'predavaci-divovi',
    component:PredavaciPocetnaComponent
  },
  {
    path:'predavanjaO',
    component:PredavanjaTabelaComponent,
    canActivate: [AuthGuard],
    data: {roles: ["Organizator"]}
  
  },
  {
    path:'predavaciO',
    component:PredavacSelectComponent,
    canActivate: [AuthGuard],
    data: {roles: ["Organizator"]}
  },
  {
    path:'slusaociO',
    component:SlusalacSelectComponent,
    canActivate: [AuthGuard],
    data: {roles: ["Organizator"]}
  },
  {
    path:'dodajPredavanje',
    component:DodajpredavanjeComponent,
    canActivate: [AuthGuard],
    data: {roles: ["Organizator"]}
  },
  {
    path:'zahteviPredavaca',
    component:ZahtevPredavacTabelaComponent,
    canActivate: [AuthGuard],
    data: {roles: ["Organizator"]}
  },
  {
    path:'zahteviSlusaoca',
    component:ZahtevSlusalacTabelaComponent,
    canActivate: [AuthGuard],
    data: {roles: ["Organizator"]}
  },
  {
    path: 'profil/:id',
    component: IzgledProfilaPredavacComponent
  },
  {
    path:'dodajpredavacanapredavanje',
    component:AddpredavaconpredavanjeComponent,
    canActivate: [AuthGuard],
    data: {roles: ["Organizator"]}
  },
  {
    path: 'profil/me/:id',
    component: IzgledProfilaPredavacComponent,
    data: {roles: ["Predavac"]}
  },
  {
    path: 'profil/slusalac/:id',
    component: IzgledProfilaSlusalacComponent
  },
  {
    path: 'profilpredavaca/:id',
    component: ProfilPredavacComponent
  },
  {
    path: 'mojaPredavanja/:id',
    component: DodajFeedbackComponent
  },
  {
    path: 'profilslusaoca/:id',
    component: IzgledProfilaSlusalacComponent
  },
  {
    path: 'profilorganizatora/:id',
    component: ProfilOrganizatorComponent,
    canActivate: [AuthGuard],
    data: {roles: ["Organizator"]}
  },
  {
    
    path:'reportfeedback',
    component:OrganizatorReportFeedbackComponent,
    canActivate: [AuthGuard],
    data: {roles: ["Organizator"]}

  },
  {
    path:'predvanjaZaKojaJeMogucaPrijava',
    component:PredavanjaMogucaComponent,
    canActivate: [AuthGuard],
    data: {roles: ["Predavac"]}
  },
  {
    path:'izmeniPredavanje',
    component:IzmeniPredavanjeComponent,
    canActivate: [AuthGuard],
    data: {roles: ["Organizator"]}

  },
  {
    path:'reportFeedback/:id',
    component:ReportFormComponent,
    canActivate: [AuthGuard],
    data: {roles: ["Predavac"]}
  },
  {
    path:'jednoPredavanje/:id',
    component:JednoPredavanjeComponent,
  },
  {
    path:'dodajPredavaca',
    component:DodajpredavacaComponent,
    canActivate: [AuthGuard],
    data: {roles: ["Organizator"]}
  },
  {
    path: 'prijavaZaPredavanja',
    component: PredavanjaSlusalacMogucaComponent
  },
  {
    path:'',redirectTo:'home',
    pathMatch:'full'
  },
  {
    path:'**',redirectTo:'error404'
  },
  {
    path:'error404',
    component:Error404Component,
  },
 
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
