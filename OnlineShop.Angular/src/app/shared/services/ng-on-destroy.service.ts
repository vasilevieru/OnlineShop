import { Injectable, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NgOnDestroy extends Subject<null> implements OnDestroy {
  ngOnDestroy() {
    this.next(null);
    this.complete();
  }
}
