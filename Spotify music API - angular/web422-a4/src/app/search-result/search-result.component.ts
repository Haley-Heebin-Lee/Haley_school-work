import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { MusicDataService } from './../music-data.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-search-result',
  templateUrl: './search-result.component.html',
  styleUrls: ['./search-result.component.css']
})
export class SearchResultComponent implements OnInit {

  results:any;
  searchQuery:any;
  searchSub: Subscription;

  constructor(private route: ActivatedRoute,
    private musicData: MusicDataService) { }

  ngOnInit(): void {
    this.searchSub = this.route.queryParams.subscribe((params) => {
      this.searchQuery = params['q'];
      this.musicData.searchArtists(this.searchQuery).subscribe((data) => {
          this.results = data.artists.items.filter(
            (item:any) => item.images.length > 0
          );
        });
    });
  }
  ngOnDestroy(): void {
    this.searchSub.unsubscribe();
  }
}
