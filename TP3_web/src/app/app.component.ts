import {Component, OnInit} from '@angular/core';
import {BehaviorSubject} from "rxjs";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'TP3_Voyages';
  connection:Boolean = false;
  StatusConnection :BehaviorSubject<Boolean> = new BehaviorSubject<Boolean>(false);
  constructor() { }

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
