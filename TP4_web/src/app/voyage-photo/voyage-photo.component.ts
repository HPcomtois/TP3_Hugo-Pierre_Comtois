import {Component, OnInit} from '@angular/core';
import {lastValueFrom} from "rxjs";
import {Voyage} from "../voyages/voyages.component";
import {HttpClient} from "@angular/common/http";
import {ActivatedRoute, Route, Router} from "@angular/router";
import {Parser} from "@angular/compiler";

@Component({
  selector: 'app-voyage-photo',
  templateUrl: './voyage-photo.component.html',
  styleUrls: ['./voyage-photo.component.css']
})
export class VoyagePhotoComponent implements OnInit{

  constructor(public http: HttpClient, public router: Router, public route: ActivatedRoute){ }

  voyage: Voyage = new Voyage(0, "", "", false);
  async getVoyage(inputId : number): Promise<void> {
    if(inputId == 0){
      console.log("id est null")
      this.router.navigate(['voyages/']);
    }
    let res = await  lastValueFrom(this.http.get<Voyage>(
      'http://localhost:5042/api/Voyages/' + inputId));
    console.log(res);
    this.voyage = res;
  }

  ngOnInit(): void {
    this.voyage.id = parseInt(<string>this.route.snapshot.paramMap.get('id'));
    this.getVoyage(this.voyage.id);
  }
}
