import { Category } from './category.model';

export interface Asset {
  id: number;
  name: string;
  serialNumber?: string;
  categoryId: number;
  category?: Category;

}
