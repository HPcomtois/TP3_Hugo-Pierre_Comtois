import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {HttpClient, HttpEventType, HttpHeaders} from "@angular/common/http";
import {NgForm} from "@angular/forms";
import {AppComponent} from "../app.component";
import {lastValueFrom} from "rxjs";

 export class Voyage {
  constructor(
    public id: number,
    public name: string,
    public img: string|null,
    public visible: boolean
  ) { }
}

export class Photo{
   constructor(
     public id: number,
     public nomDuFicher: string,
     public mimeType: string
   ) {}
}

export class User{
  constructor(
    public userName: string,
    public password: string,
    public listVoyages?: Voyage[]
  ){}
}
@Component({
  selector: 'app-voyages',
  templateUrl: './voyages.component.html',
  styleUrls: ['./voyages.component.css']
})

export class VoyagesComponent implements OnInit{
  voyages: Voyage[] = [];
  voyagesPublic: Voyage[] = [];
  public: boolean = true;
  constructor(public http: HttpClient, public app: AppComponent){ }

  async getVoyages(): Promise<void>{
    let res = await lastValueFrom(this.http.get<Voyage[]>(
      "http://localhost:5042/api/Voyages"));
    console.log(res);
    this.voyages = res;
  }

  async getVoyagesPubliques(): Promise<void>{
    let res = await lastValueFrom(this.http.get<Voyage[]>(
      "http://localhost:5042/api/Voyages/GetVoyagePublique"));
    console.log(res);
    this.voyagesPublic = res;
  }

  async delete(inputId : number): Promise<void> {
    let res = await lastValueFrom(this.http.delete<Voyage>(
        'http://localhost:5042/api/Voyages/' + inputId));
    console.log(res);
    this.getVoyages();
    this.getVoyagesPubliques();
  }

  async addvoyage(addVoyage: NgForm): Promise<void> {

    let voyage: Voyage = {
      id: 0,
      name: addVoyage.value.nom_voyage,
      img: null,
      visible: this.public
    }
    let res = await lastValueFrom(this.http.post<Voyage>('http://localhost:5042/api/Voyages', voyage))
    console.log(res);
    addVoyage.resetForm();
    await this.getVoyages();
    await this.getVoyagesPubliques();
  }

  async partager(InputId: number, email: NgForm){
    let res = await lastValueFrom(this.http.put<Voyage[]>(
      'http://localhost:5042/api/Voyages/' + InputId, email.value.value_email));
    console.log(res);
    email.resetForm();
    this.getVoyages();
    this.getVoyagesPubliques();
  }

  ngOnInit(): void {
      this.getVoyages();
      this.getVoyagesPubliques();
  }
}
