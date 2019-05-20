import { Directive, OnInit, OnDestroy, Input, TemplateRef, ViewContainerRef } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { takeWhile, filter, map } from 'rxjs/operators';
import { AuthService } from '@shared';

@Directive({
  selector: '[appAuthorize]'
})
export class AuthorizeDirective implements OnInit, OnDestroy {

  private _hasView = false;

  private _functionality = new BehaviorSubject<string>(null);
  private _functionality$: Observable<string>;

  private isAlive = true;

  constructor(
    private templateRef: TemplateRef<any>,
    private viewContainer: ViewContainerRef,
    private authService: AuthService
  ) {
    this._functionality$ = this._functionality.asObservable();
  }

  @Input() set appAuthorize(functionality: string) {
    this._functionality.next(functionality);
  }

  ngOnInit(): void {
    this._functionality$
      .pipe(
        takeWhile(() => this.isAlive),
        filter((functionality) => functionality != null),
        map((functionality) => functionality)
      )
      .subscribe((functiuonality) => {
        this._toggleEmbeddedView(this._checkPermissions(functiuonality));
      });
  }

  ngOnDestroy(): void {
    this.isAlive = false;
  }

  private _checkPermissions(functionality: string): boolean {
    return this.authService.userHasPermission(functionality);
  }

  private _createEmbeddedView() {
    this.viewContainer.createEmbeddedView(this.templateRef);
    this._hasView = true;
  }

  private _clearEmbeddedView() {
    this.viewContainer.clear();
    this._hasView = false;
  }

  private _toggleEmbeddedView(condition: boolean) {
    if (condition && !this._hasView) {
      this._createEmbeddedView();
    } else if (!condition && this._hasView) {
      this._clearEmbeddedView();
    }
  }
}
