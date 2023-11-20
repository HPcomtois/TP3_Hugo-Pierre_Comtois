import {Component, OnInit} from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {NgForm} from "@angular/forms";
import {AppComponent} from "../app.component";
import {lastValueFrom} from "rxjs";

class Voyage {
  constructor(
    public id: number,
    public name: string,
    public img: string = "",
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

  constructor(public http: HttpClient, public app: AppComponent){ }
  addvoyage(addVoyage: NgForm){

    let voyage: Voyage = {
      id: 0,
      name: addVoyage.value.nom_voyage,
      img: "https://www.routesdumonde.com/wp-content/uploads/2019/06/Thumbnail-Japon.jpg",
    }

    this.http.post<Voyage>('http://localhost:5042/api/Voyages', voyage)
        .subscribe(res =>
      console.log(res)
    );
    this.voyages.push(voyage);
    addVoyage.resetForm();
  }

  async getVoyages(): Promise<void> {
      let res = await  lastValueFrom(this.http.get<Voyage[]>(
        'http://localhost:5042/api/Voyages'));
      console.log(res);
      this.voyages = res;
  }

  

  ngOnInit(): void {
      this.getVoyages();
  }
}
