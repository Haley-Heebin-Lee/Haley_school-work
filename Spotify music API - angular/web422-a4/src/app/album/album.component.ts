import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MusicDataService } from '../music-data.service';
import { ActivatedRoute, Params } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-album',
  templateUrl: './album.component.html',
  styleUrls: ['./album.component.css']
})
export class AlbumComponent implements OnInit {

  album:any;
  albumSub:Subscription;
 
  constructor(
    private matSnackBar: MatSnackBar,
    private route: ActivatedRoute,
    private musicData: MusicDataService) { }

  ngOnInit(): void {
    this.albumSub = this.route.params.subscribe((params: Params) => {
      this.musicData.getAlbumById(params.id)
        .subscribe((data:any) => (this.album = data));
    });
  }

  ngOnDestroy(): void {
    if (this.albumSub) {
      this.albumSub.unsubscribe();
    }
  }

  addToFavourites(trackID:string) :void{
    this.musicData.addToFavourites(trackID).subscribe((data:any)=>{
      this.matSnackBar.open("Adding to Favourites...", "Done", {duration: 1500})
    }, (err:any)=>{
      this.matSnackBar.open("Unable to add song...", "Done", {duration: 1500})
    })
  }
}
