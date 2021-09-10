import { Component, OnInit, OnDestroy } from '@angular/core';
import { MusicDataService } from './../music-data.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-new-releases',
  templateUrl: './new-releases.component.html',
  styleUrls: ['./new-releases.component.css']
})
export class NewReleasesComponent implements OnInit {

  releases:Array<any>;
  newReleaseSub: Subscription; 

  constructor(private musicData: MusicDataService) { }

  ngOnInit(): void {
    this.newReleaseSub = this.musicData.getNewReleases().subscribe(data=>{
      this.releases = data.albums.items;
    })
  }
  ngOnDestroy(): void {
    this.newReleaseSub.unsubscribe();
  }
}
