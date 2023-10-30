import {Component, OnInit} from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {NgForm} from "@angular/forms";
import {ActivatedRoute} from "@angular/router";

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
export class AppComponent implements OnInit{
  title = 'TP3_Voyages';

  constructor(public http: HttpClient, private route: ActivatedRoute) { }

  // callapi(){
  //   let token = localStorage.getItem('token');
  //
  //   let httpOptions = {
  //     headers: new HttpHeaders( {
  //       'Content-Type': 'application/json',
  //       Authorization: 'Bearer ' + token
  //     })
  //   };
  //
  //   this.http.get<any>('http://localhost:5042/api/Voyages', httpOptions).subscribe(res =>
  //     console.log(res)
  //   );
  // }

  // addcat(){
  //   let token = localStorage.getItem('token');
  //
  //   let httpOptions = {
  //     headers: new HttpHeaders( {
  //       'Content-Type': 'application/json',
  //       Authorization: 'Bearer ' + token
  //     })
  //   };
  //
  //   this.http.post<any>('http://localhost:5042/api/Voyages', httpOptions).subscribe(res =>
  //     console.log(res)
  //   );
  // }

  logout(){
    localStorage.removeItem('token');
  }

  ngOnInit(): void {

  }
}
