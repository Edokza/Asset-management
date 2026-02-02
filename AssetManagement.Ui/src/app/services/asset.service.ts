import { Injectable } from '@angular/core';
import { HttpClient,HttpParams  } from '@angular/common/http';

import { Asset } from '../models/asset.model';
import { AssetQuery } from '../models/asset-query.model';
import { PagedResult } from '../models/api-response.model';

@Injectable({
  providedIn: 'root',
})
export class AssetService {

  private apiUrl = 'https://localhost:7048/api/Assets';

  constructor(private http: HttpClient) { }

  getAssets(query: AssetQuery) {

  const params = new HttpParams({
    fromObject: {
    page: query.page,
    pageSize: query.pageSize,
    search: query.search ?? '',
    categoryId: query.categoryId?.toString() ?? ''
  }});

  return this.http.get<PagedResult<Asset>>(this.apiUrl, { params });
}

  deleteAsset(id: number) {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
