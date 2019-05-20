import { ViewChild, OnInit, AfterViewInit, ElementRef } from '@angular/core';
import { Observable, merge, ConnectableObservable, fromEvent, empty } from 'rxjs';
import { MatTableDataSource, MatPaginator, MatSort, MatDialog } from '@angular/material';
import { takeUntil, publish, debounceTime, distinctUntilChanged, tap } from 'rxjs/operators';
import { PageView } from '../models/page-view.model';
import { NgOnDestroy } from '../services/ng-on-destroy.service';
import { PagedResult } from '../models/page-result.model';
import { ConfirmationDialogComponent } from '../components';

export abstract class MaterialGridComponent implements OnInit, AfterViewInit {

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild('filterInput') filterInput: ElementRef;

  public dataSource = new MatTableDataSource();
  public resultsLength = 0;
  public isLoadingResults = true;

  public pageSize = 10;
  public pageSizeOptions = [5, 10, 20];

  protected PageView: PageView = new PageView();
  protected fetchDataEvent$: Observable<any> = null;

  protected sourceChanges: Observable<any>;

  public get hasNoResults() { return this.resultsLength === 0 && !this.isLoadingResults; }

  constructor(
    protected onDestroy$: NgOnDestroy,
    public dialog: MatDialog,
  ) {
  }

  abstract fetchData(vm: PageView): Observable<PagedResult<any>>;

  ngOnInit(): void {
    let filterEvent: Observable<any>;
    let sort: Observable<any>;
    if (this.sort) {
      this.sort.disableClear = true;
      // If the user changes the sort order, reset back to the first page.
      this.sort.sortChange.pipe(
        takeUntil(this.onDestroy$)
      ).subscribe(() => this.paginator.pageIndex = 0);
      sort = this.sort.sortChange;
    } else {
      sort = empty();
    }

    if (this.filterInput) {
      filterEvent = fromEvent(this.filterInput.nativeElement, 'keyup')
        .pipe(
          debounceTime(400),
          distinctUntilChanged(),
          tap(() => this.paginator.pageIndex = 0),
          takeUntil(this.onDestroy$)
        );
    } else {
      filterEvent = empty();
    }

    const start$ = new Observable(o => { o.next({}); });
    this.sourceChanges = merge(
      sort, this.paginator.page, filterEvent,
      this.fetchDataEvent$ || start$
    ).pipe(
      debounceTime(400),
      takeUntil(this.onDestroy$),
      publish()
    );

    this.sourceChanges.pipe(
      takeUntil(this.onDestroy$),
    ).subscribe(() => {
      this.loadData();
    });
  }

  ngAfterViewInit(): void {
    (<ConnectableObservable<any>>this.sourceChanges).connect();
  }

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
 
  fillPageData(): void {
    this.PageView.filter = this.filterInput ? this.filterInput.nativeElement.value : '';
    this.PageView.sort = null;
    // if (this.sort.direction === '' || this.sort.direction == null) {
    //   this.PageView.sort = null;
    // } else {
      this.PageView.ascending = true;
    // }
    this.PageView.pageSize = this.paginator.pageSize;
    this.PageView.page = this.paginator.pageIndex + 1;
  }

  loadData(): void {
    this.isLoadingResults = true;
    this.fillPageData();

    this.fetchData(this.PageView).subscribe(data => {
      this.isLoadingResults = false;
      this.resultsLength = data.totalCount;
      this.pageSize = data.pageSize;
      this.dataSource.data = data.result;
    });
  }

  appendItem(item: any): void {
    const pageSize = this.paginator.pageSize;
    const dataLength = this.dataSource.data.length;

    if (pageSize !== dataLength) {
      this.dataSource.data = [...this.dataSource.data, item];
    }

    // disable sorting after append
    this.sort.direction = null;
    this.sort.active = null;

    this.resultsLength += 1;
    this.paginator.length += 1;
    this.paginator.lastPage();
  }

  deleteItem(value: number, key: string): void {
    const index = this.dataSource.data.findIndex(item => item[key] === value);
    this.dataSource.data = [...this.dataSource.data.slice(0, index), ...this.dataSource.data.slice(index + 1)];

    this.resultsLength = this.resultsLength - 1;
    this.paginator.length -= 1;

    if (this.paginator.pageIndex === this.paginator.getNumberOfPages()) {
      this.paginator.previousPage();
    } else {
      this.loadData();
    }
  }

  confirmDeletion(message: string = 'Would you like to delete this item?', isWarning: boolean = false): Promise<any> {
    return new Promise((resolve) => {
      const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
        data: {
          messageText: message,
          isWarning: isWarning
        },
        disableClose: true
      });

      dialogRef.afterClosed().subscribe(resolve);
    });
  }
}
