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
  voyagesPubliques: Voyage[] = [];
  voyage: any = {id:0 , name: "", img: "", visible: false};
  constructor(public http: HttpClient, public app: AppComponent){ }
  async getVoyages(): Promise<void> {
      let res = await  lastValueFrom(this.http.get<Voyage[]>(
        'http://localhost:5042/api/Voyages'));
      console.log(res);
      this.voyages = res;
  }

  async delete(inputId : number): Promise<void> {
    let res = await lastValueFrom(this.http.delete<Voyage>('http://localhost:5042/api/Voyages/' + inputId))
    console.log(res)
    this.getVoyages()
  }

  async deletePublic(item_id : number){
    this.voyagesPubliques = JSON.parse(localStorage.getItem('voyagesPublic')!);
    this.voyagesPubliques = this.voyagesPubliques.splice(item_id, 1);
    localStorage.setItem('voyagesPublic', JSON.stringify(this.voyagesPubliques));
  }

  async addvoyage(addVoyage: NgForm): Promise<void> {

    let voyage: Voyage = {
      id: 0,
      name: addVoyage.value.nom_voyage,
      img: "https://www.routesdumonde.com/wp-content/uploads/2019/06/Thumbnail-Japon.jpg",
      visible: false
    }
    let res = await lastValueFrom(this.http.post<Voyage>('http://localhost:5042/api/Voyages', voyage))
    console.log(res);
    addVoyage.resetForm();
    this.getVoyages()
  }

  async rendrePublic(inputId : number): Promise<void> {
    let voyage = await lastValueFrom(this.http.get<Voyage>('http://localhost:5042/api/Voyages/' + inputId))
    this.voyage = voyage;
    this.voyage.visible = !this.voyage.visible;
    if(localStorage.getItem('voyagesPublic') == null){
      localStorage.setItem('voyagesPublic', JSON.stringify(this.voyagesPubliques!));
    }
    else if (this.voyage.visible){
      this.voyagesPubliques.push(this.voyage);
      localStorage.setItem('voyagesPublic', JSON.stringify(this.voyagesPubliques));
    }
    else{
      this.voyages.push(voyage);
    }
  }

  ngOnInit(): void {
      this.getVoyages();
  }

  protected readonly localStorage = localStorage;
  protected readonly JSON = JSON;
}
