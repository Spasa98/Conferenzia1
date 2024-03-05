import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './components/header/header.component';
import { MegaMenuModule } from 'primeng/megamenu';
import { MenubarModule } from 'primeng/menubar';
import { InputTextModule } from 'primeng/inputtext';
import { TabViewModule } from 'primeng/tabview';
import { ButtonModule } from 'primeng/button';
import { PredavacSelectComponent } from './components/predavac-select/predavac-select.component';
import { TableModule } from 'primeng/table';
import { ScrollingModule } from '@angular/cdk/scrolling';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { DodajpredavacaComponent } from './components/dodajpredavaca/dodajpredavaca.component';
import { DropdownModule} from 'primeng/dropdown';
import { DialogService, DynamicDialogModule } from 'primeng/dynamicdialog';
import { DialogModule } from 'primeng/dialog';

import { LoginPageComponent } from './components/login-page/login-page.component';
import { PasswordModule } from 'primeng/password';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { MessagesModule } from 'primeng/messages';
import { ProfilOrganizatorComponent } from './profil/profil-organizator/profil-organizator.component';
import { ProfilPredavacComponent } from './profil/profil-predavac/profil-predavac.component';
import { ProfilSlusalacComponent } from './profil/profil-slusalac/profil-slusalac.component';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { PredavanjaTabelaComponent } from './components/prikaz-tabela/predavanja-tabela/predavanja-tabela.component';
import { ZahtevSlusalacTabelaComponent } from './components/prikaz-tabela/zahtev-slusalac-tabela/zahtev-slusalac-tabela.component';
import { DodajpredavanjeComponent } from './components/dodajpredavanje/dodajpredavanje.component';
import { CalendarModule } from 'primeng/calendar';
import { ZahtevPredavacTabelaComponent } from './components/prikaz-tabela/zahtev-predavac-tabela/zahtev-predavac-tabela.component';
import { PredavacPredavanjaComponent } from './components/prikaz-tabela/predavac-predavanja/predavac-predavanja.component';
import { SlusalacPredavanjaComponent } from './components/prikaz-tabela/slusalac-predavanja/slusalac-predavanja.component';
import { PredavanjePredavacComponent } from './components/predavanja/predavanje-predavac/predavanje-predavac.component';
import {PaginatorModule} from 'primeng/paginator';
import { RegistracijaSlusaocaComponent } from './components/registracija-slusaoca/registracija-slusaoca.component';
import { DodajFeedbackComponent } from './components/dodaj-feedback/dodaj-feedback.component';
import { RadioButtonModule } from 'primeng/radiobutton';
import { RatingModule } from 'primeng/rating';
import { IzmeniProfilPredavacComponent } from './profil/izmeni-profil/izmeni-profil-predavac/izmeni-profil-predavac.component';
import { IzmeniProfilSlusalacComponent } from './profil/izmeni-profil/izmeni-profil-slusalac/izmeni-profil-slusalac.component';
import { IzmeniProfilOrganizatorComponent } from './profil/izmeni-profil/izmeni-profil-organizator/izmeni-profil-organizator.component';
import { PredavaciPocetnaComponent } from './components/predavaci-pocetna/predavaci-pocetna.component';
import { CheckboxModule } from 'primeng/checkbox';
import { MessageModule } from 'primeng/message';
import { RippleModule } from 'primeng/ripple';
import { HomePageComponent } from './components/home-page/home-page.component';
import { PredavanjaMogucaComponent } from './components/predavanja/predavanje-predavac/predavanja-moguca/predavanja-moguca.component';
import { JwtInterceptor } from './intercepors/jwt.interceptor';
import { PredavanjaSlusalacMogucaComponent } from './components/predavanja/predavanja-slusalac-moguca/predavanja-slusalac-moguca.component';
import { CommonModule } from '@angular/common';
import { ConfirmationService, MessageService } from 'primeng/api';
import { SplitButtonModule } from 'primeng/splitbutton';
import { SlusalacSelectComponent } from './components/slusalac-select/slusalac-select.component';
import { IzgledProfilaPredavacComponent } from './profil/izgled-profila-predavac/izgled-profila-predavac.component';
import { IzgledProfilaSlusalacComponent } from './profil/izgled-profila-slusalac/izgled-profila-slusalac.component';
import {AvatarModule} from 'primeng/avatar';
import {AvatarGroupModule} from 'primeng/avatargroup';
import { IzmeniPredavanjeComponent } from './components/izmeni-predavanje/izmeni-predavanje.component';
import { ReportFormComponent } from './profil/izgled-profila-predavac/report-form/report-form.component';
import { OrganizatorReportFeedbackComponent } from './components/prikaz-tabela/organizator-report-feedback/organizator-report-feedback.component';


import {ToastModule} from 'primeng/toast';
import { FooterComponent } from './components/footer/footer.component';
import {ConfirmDialogModule} from 'primeng/confirmdialog';
import { JednoPredavanjeComponent } from './components/jedno-predavanje/jedno-predavanje.component';
import { AddpredavaconpredavanjeComponent } from './components/addpredavaconpredavanje/addpredavaconpredavanje.component';
import { Error404Component } from './components/error404/error404.component';
import { DodajorganizatoraComponent } from './components/dodajorganizatora/dodajorganizatora.component';

@NgModule({
  declarations: [
    ReportFormComponent,
    AppComponent,
    IzgledProfilaSlusalacComponent,
    IzgledProfilaPredavacComponent,
    SlusalacSelectComponent,
    IzmeniPredavanjeComponent,
    HeaderComponent,
    PredavacSelectComponent,
    DodajpredavacaComponent,
    LoginPageComponent,
    ProfilOrganizatorComponent,
    ProfilPredavacComponent,
    ProfilSlusalacComponent,
    PredavanjaTabelaComponent,
    ZahtevSlusalacTabelaComponent,
    DodajpredavanjeComponent,
    ZahtevPredavacTabelaComponent,
    PredavacPredavanjaComponent,
    SlusalacPredavanjaComponent,
    PredavanjePredavacComponent,
    RegistracijaSlusaocaComponent,
    DodajFeedbackComponent,
    IzmeniProfilPredavacComponent,
    IzmeniProfilSlusalacComponent,
    IzmeniProfilOrganizatorComponent,
    PredavaciPocetnaComponent,
    HomePageComponent,
    PredavanjaMogucaComponent,
    SlusalacSelectComponent,
    IzgledProfilaPredavacComponent,
    IzgledProfilaSlusalacComponent,
    IzmeniPredavanjeComponent,
    FooterComponent,
    PredavanjaSlusalacMogucaComponent,
    OrganizatorReportFeedbackComponent,
    JednoPredavanjeComponent,
    AddpredavaconpredavanjeComponent,
    Error404Component,
    DodajorganizatoraComponent,
  ],

  imports: [
    BrowserModule,
    AppRoutingModule,
    MegaMenuModule,
    MenubarModule,
    InputTextModule,
    TabViewModule,
    ButtonModule,
    CommonModule,
    TableModule,
    ScrollingModule,
    HttpClientModule,
    ConfirmDialogModule,
    FormsModule,
    DynamicDialogModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    DropdownModule,
    PaginatorModule,
    PasswordModule,
    ProgressSpinnerModule,
    MessagesModule,
    InputTextareaModule,
    DropdownModule,
    CalendarModule,
    RadioButtonModule,
    RatingModule,
    CheckboxModule,
    MessageModule,
    RippleModule,
		ToastModule,
    SplitButtonModule,
    AvatarModule,
    AvatarGroupModule,
    CommonModule,
    ConfirmDialogModule
],
  providers: [AppComponent,ConfirmationService,DialogService,MessageService,{ provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },],
  bootstrap: [AppComponent],
  schemas: [ CUSTOM_ELEMENTS_SCHEMA ]

})
export class AppModule { }
