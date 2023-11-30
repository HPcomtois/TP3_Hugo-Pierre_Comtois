import {Component, OnInit} from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {NgForm} from "@angular/forms";
import {AppComponent} from "../app.component";
import {lastValueFrom} from "rxjs";

 export class Voyage {
  constructor(
    public id: number,
    public name: string,
    public img: string = "",
    public visible: boolean
  ) { }
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

  token: string | null = sessionStorage.getItem('token');
  voyages: Voyage[] = [];
  voyagesPublic: Voyage[] = [];
  public: boolean = true;
  email_value: string = "";
  constructor(public http: HttpClient, public app: AppComponent){ }
  async getVoyages(): Promise<void> {
      let res = await  lastValueFrom(this.http.get<Voyage[]>(
        'http://localhost:5042/api/Voyages'));
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
      img: "https://www.routesdumonde.com/wp-content/uploads/2019/06/Thumbnail-Japon.jpg",
      visible: this.public
    }
    let res = await lastValueFrom(this.http.post<Voyage>('http://localhost:5042/api/Voyages', voyage))
    console.log(res);
    addVoyage.resetForm();
    this.getVoyages();
    this.getVoyagesPubliques();
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
