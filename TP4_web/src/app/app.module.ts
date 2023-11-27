import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import {HTTP_INTERCEPTORS, HttpClientModule} from "@angular/common/http";
import {FormsModule} from "@angular/forms";
import {RouterModule} from "@angular/router";
import { ConnexionComponent } from './connexion/connexion.component';
import { InscriptionComponent } from './inscription/inscription.component';
import { VoyagesComponent } from './voyages/voyages.component';
import {NgOptimizedImage} from "@angular/common";
import {AuthInterceptor} from "./auth.interceptor";

@NgModule({
  declarations: [
    AppComponent,
    ConnexionComponent,
    InscriptionComponent,
    VoyagesComponent
  ],
    imports: [
        BrowserModule,
        HttpClientModule,
        FormsModule,
        RouterModule.forRoot([
            {path: '', redirectTo: '/voyages', pathMatch: 'full'},
            {path: 'connexion', component: ConnexionComponent},
            {path: 'inscription', component: InscriptionComponent},
            {path: 'voyages', component: VoyagesComponent}
        ]),
        NgOptimizedImage
    ],
  providers: [
      {provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi:true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
