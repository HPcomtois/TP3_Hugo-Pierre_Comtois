import {Component, OnInit} from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {NgForm} from "@angular/forms";
import {ActivatedRoute} from "@angular/router";
import {BehaviorSubject} from "rxjs";

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
  connection:Boolean = false;
  StatusConnection :BehaviorSubject<Boolean> = new BehaviorSubject<Boolean>(false);

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
    if(localStorage.getItem('token')){
      this.StatusConnection.next(false);
      localStorage.removeItem('token');
    }
    else{
      this.StatusConnection.next(true);
    }
  }

  ifConnected(){
    if(localStorage.getItem('token')){
      this.StatusConnection.next(true);
    }
  }

  ngOnInit(): void {
      this.StatusConnection.subscribe(value => {
        this.connection = value;
      })
    this.ifConnected();
  }
}
