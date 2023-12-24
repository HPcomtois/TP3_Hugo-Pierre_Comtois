import {AfterViewInit, Component, ElementRef, OnInit, QueryList, ViewChild, ViewChildren} from '@angular/core';
import {lastValueFrom} from "rxjs";
import {Photo, Voyage} from "../voyages/voyages.component";
import {HttpClient} from "@angular/common/http";
import {ActivatedRoute, Router} from "@angular/router";


declare var Masonry : any;
declare var imagesLoaded : any;

@Component({
  selector: 'app-voyage-photo',
  templateUrl: './voyage-photo.component.html',
  styleUrls: ['./voyage-photo.component.css']
})
export class VoyagePhotoComponent implements OnInit, AfterViewInit{

  constructor(public http: HttpClient, public router: Router, public route: ActivatedRoute){ }

  @ViewChild("_fileUploadViewChild", {static:false}) fileUpload ?: ElementRef;
  @ViewChild('masongrid') masongrid?: ElementRef;
  @ViewChildren('masongriditems') masongriditems?: QueryList<any>;

  voyage: Voyage = new Voyage(0, "", "", false);
  photos: Photo[] = [];
  bigPhoto: Photo = new Photo(0, "", "");

  async getVoyagePhotos(inputId : number): Promise<void> {
    if(inputId == 0){
      console.log("id est null")
      this.router.navigate(['voyages/']);
    }

    let res = await  lastValueFrom(this.http.get<Voyage>(
      'http://localhost:5042/api/Voyages/' + inputId));
    let photosRes = await lastValueFrom(this.http.get<Photo[]>(
      'http://localhost:5042/api/Photos/' + inputId));

    console.log(res);
    console.log(photosRes);

    this.voyage = res;
    this.photos = photosRes;
  }

  async uploadPhoto(id:number){
    if(this.fileUpload == undefined){
      console.log("Input non chargé.");
      return;
    }
    let file = this.fileUpload.nativeElement.files[0];
    if(file == null){
      console.log("Input ne contient pas d'image.");
      return;
    }

    let formData= new FormData();
    formData.append('image', file, file.name);

    let res = await lastValueFrom(this.http.post<any>('http://localhost:5042/api/Photos/Upload/'+ id,formData));
    console.log(res);
    location.reload();
  }

  async deletePhoto(id: number): Promise<void>{
    if(id == 0){
      console.log("id est null")
      return;
    }
    let res = await lastValueFrom(this.http.delete<any>('http://localhost:5042/api/Photos/' + id))
    console.log(res);
    this.getVoyagePhotos(this.voyage.id);
  }

  async photoBig(id: number): Promise<void>{
    if(id == 0){
      console.log("id est null")
      return;
    }
    let res = await lastValueFrom(this.http.get<Photo>('http://localhost:5042/api/Photos/GetPhotoVoyage/' + id));
    console.log(res);
    if(localStorage.getItem("bigPic") == null){
      localStorage.setItem("bigPic", res.id.toString());
    }
    else{
      localStorage.removeItem("bigPic");
      localStorage.setItem("bigPic", res.id.toString());
    }
  }

  ngOnInit(): void {
    this.voyage.id = parseInt(<string>this.route.snapshot.paramMap.get('id'));
    this.getVoyagePhotos(this.voyage.id);
    localStorage.removeItem('bigPic');
  }

  ngAfterViewInit() {
    this.masongriditems?.changes.subscribe(e => {
      this.initMasonry();
    });

    if(this.masongriditems!.length > 0) {
      this.initMasonry();
    }
  }

  initMasonry() {
    var grid = this.masongrid?.nativeElement;
    var msnry = new Masonry( grid, {
      itemSelector: '.grid-item',
      columnWidth:320, // À modifier si le résultat est moche
      gutter:3
    });

    imagesLoaded( grid ).on( 'progress', function() {
      msnry.layout();
    });
  }

  enlever(){
    localStorage.removeItem('bigPic');
  }

  protected readonly JSON = JSON;
  protected readonly localStorage = localStorage;
}
