import {Component, Inject} from '@angular/core';
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-items-component',
  templateUrl: './items.component.html'
})
export class ItemsComponent {

  public message: string;

  constructor(private _httpClient: HttpClient, @Inject('BASE_URL') private _baseUrl: string) {}

  public feedItems() {
    this._httpClient.get<IItemsFeedResponse>(this._baseUrl + 'items/feed').subscribe(result => {
      this.message = result.message;
    }, error => this.message = error.error);
  }
}

interface IItemsFeedResponse {
  message: string;
}
