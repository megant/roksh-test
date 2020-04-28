import {Component, Inject} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-items-component',
  templateUrl: './package-items.component.html'
})
export class PackageItemsComponent {

  public message: string;
  public packageCode: string;
  public packageItems: IPackageItem[] = [];

  constructor(
    private _httpClient: HttpClient,
    @Inject('BASE_URL') private _baseUrl: string,
    private route: ActivatedRoute) {
    this.packageCode = this.route.snapshot.paramMap.get('packageCode');
    this._httpClient.get<IPackageItem[]>(`${this._baseUrl}packageItems/getItems/${this.packageCode}`).subscribe(result => {
      this.packageItems = result;
    }, error => this.message = error.error);
  }
}

interface IPackageItem {
  code: string;
  imageUrl: string;
  name: string;
  description: string;
}
