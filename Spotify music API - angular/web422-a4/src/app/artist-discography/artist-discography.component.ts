import { Component, OnInit, OnDestroy } from '@angular/core';
import { MusicDataService } from './../music-data.service';
import { Subscription } from 'rxjs';
import { ActivatedRoute, Params } from '@angular/router';

@Component({
  selector: 'app-artist-discography',
  templateUrl: './artist-discography.component.html',
  styleUrls: ['./artist-discography.component.css']
})
export class ArtistDiscographyComponent implements OnInit {

  albums:Array<any>;
  artists:any;
  discographySub: Subscription;
  
  
  constructor(private musicData: MusicDataService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.discographySub = this.route.params.subscribe((params:Params)=>{
      this.musicData.getArtistById(params.id).subscribe((data)=>{
        this.artists = data;
      });
      this.musicData.getAlbumsByArtistId(params.id).subscribe((data)=>{
    
        this.albums = (data.items).filter(function(elem:any, index:number, arr:Array<any>){
          let name = elem.name;
          return index === arr.findIndex(x=>x.name===name)
        });
      });
    });
  }
  ngOnDestroy(): void {
    this.discographySub.unsubscribe();
  }
}
