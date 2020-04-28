import {Component, Inject} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import {FormControl} from '@angular/forms';

@Component({
  selector: 'app-packages-component',
  templateUrl: './package-list.component.html',
  styleUrls: ['./package-list.component.css']
})
export class PackageListComponent {
  private packages: IPackage[] = [];
  public filteredPackages: IPackage[] = [];
  public filter: FormControl = new FormControl('');
  public error: string;

  public constructor(private _httpClient: HttpClient, @Inject('BASE_URL') private _baseUrl: string) {
    this._httpClient.get<IPackage[]>(this._baseUrl + 'packages/getAll').subscribe(result => {
      this.packages = result;
      this.filteredPackages = result;
    }, error => this.error = error.error);
    this.filter.valueChanges.subscribe(value => {
      this.filteredPackages = this.filterPackages(value);
    });
  }

  public generatePackage() {
    this._httpClient.get<IPackage>(this._baseUrl + 'packages/generate').subscribe(result => {
      this.packages.push(result);
      this.filter.setValue('');
    }, error => this.error = error.error);
  }

  private filterPackages(text: string): IPackage[] {
    return this.packages.filter(pkg => {
      const term = text.toLowerCase();
      return pkg.code.toLowerCase().includes(term);
    });
  }
}

interface IPackage {
  id: string;
  code: string;
  name: string;
  description: string;
  state: string;
}
