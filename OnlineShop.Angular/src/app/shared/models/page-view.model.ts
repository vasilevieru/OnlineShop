import { HttpParams } from '@angular/common/http';

export class PageView {
  page: number;
  pageSize: number;
  sort: string;
  filter: string;
  filterFields: string[];
  ascending: boolean;

  constructor(page: number = 1, pageSize: number = 10, ascending: boolean = true) {
    this.page = page;
    this.pageSize = pageSize;
    this.ascending = ascending;
  }

  public toSearchParams(): HttpParams {
    let parameters = new HttpParams();
    for (const key in this) {
      if (this[key] && key !== 'toSearchParams') {
        parameters = parameters.append(`${key}`, `${this[key]}`);
      }
    }
    return parameters;
  }
}
