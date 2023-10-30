import { Component } from '@angular/core';
import {NgForm} from "@angular/forms";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-connexion',
  templateUrl: './connexion.component.html',
  styleUrls: ['./connexion.component.css']
})
export class ConnexionComponent {

  constructor(public http: HttpClient) { }

  login(connexion: NgForm){
    let user = {
      userName: connexion.value.nom_user,
      password: connexion.value.mdp_user
    }
    this.http.post<any>('http://localhost:5042/api/Account/Login', user).subscribe(res => {
      console.log(res)
      localStorage.setItem('token', res.token);
    });
    connexion.resetForm();
  }
}
