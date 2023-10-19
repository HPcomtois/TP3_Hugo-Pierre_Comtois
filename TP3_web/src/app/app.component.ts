import { Component } from '@angular/core';
import {HttpClient} from "@angular/common/http";

class Voyage {
  constructor(
    public id: number,
    public name: string,
    public img: string = ""
  ) { }
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'TP3_Voyages';
  voyages: Voyage[] = [];
  newVoyage = new Voyage(0, "Colo", "");

  constructor(public http: HttpClient) { }

  getVoyages(){
    this.http.get<Voyage[]>('http://localhost:5042/api/Voyages/GetVoyage').subscribe(res => {
      console.log(res)
      this.voyages = res;
    });
  }

  sendVoyage() {
    this.http.post<Voyage[]>('http://localhost:5042/api/voyages/PostVoyage', this.newVoyage).subscribe(res => {
      console.log(res);
    })
  }
}
