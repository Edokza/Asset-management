import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class Asset {
  
  private apiUrl = 'https://localhost:7048/api/Assets';

  constructor(private http: HttpClient) { }

  getAsset(params: any) {
    return this.http.get(this.apiUrl, { params });
  }

  createAsset(data: any) {
    return this.http.post(this.apiUrl, data);
  }

  updateAsset(id: string, data: any) {
    return this.http.put(`${this.apiUrl}/${id}`, data); 
  }

  deleteAsset(id: string) {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }

}
