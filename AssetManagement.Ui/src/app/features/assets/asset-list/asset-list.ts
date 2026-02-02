import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';

import { AssetService } from '../../../services/asset.service';

@Component({
  selector: 'app-asset-list',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    TableModule,
    ButtonModule,
    InputTextModule
  ],
  templateUrl: './asset-list.html'
})
export class AssetListComponent implements OnInit {

  assets: any[] = [];

  totalRecords = 0;
  loading = false;

  page = 1;
  pageSize = 10;
  search = '';

  constructor(private assetService: AssetService) { }

  ngOnInit(): void {
    this.loadAssets();
  }

  loadAssets() {

    const params = {
      page: this.page,
      pageSize: this.pageSize,
      search: this.search
    };

    this.assetService.getAssets(params)
      .subscribe((res: any) => {

        this.assets = res.data;
        this.totalRecords = res.totalCount;

      });
  }

  onSearch() {
    this.page = 1;
    this.loadAssets();
  }

  onPage(event: any) {
    this.page = event.first / event.rows + 1;
    this.pageSize = event.rows;

    this.loadAssets();
  }

  delete(id: number) {
    if (!confirm('Delete this asset?')) return;

    this.assetService.deleteAsset(id)
      .subscribe(() => this.loadAssets());
  }
}
