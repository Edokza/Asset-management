import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  private apiUrl = 'https://localhost:7048/api/Categories';

  constructor(private http: HttpClient) { }

  getCategories() {
    return this.http.get(this.apiUrl);
  }
}
