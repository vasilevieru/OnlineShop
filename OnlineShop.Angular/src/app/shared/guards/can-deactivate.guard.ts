import { Injectable, HostListener } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot, CanDeactivate } from '@angular/router';
import { Observable } from 'rxjs';

export abstract class ComponentCanDeactivate {

  abstract canDeactivate(): Observable<boolean> | Promise<boolean> | boolean;

  @HostListener('window:beforeunload', ['$event'])
  unloadNotification($event: any) {
    if (!this.canDeactivate()) {
      $event.returnValue = true;
    }
  }
}

@Injectable({
  providedIn: 'root'
})
export class CanDeactivateGuard implements CanDeactivate<ComponentCanDeactivate> {
  canDeactivate(component: ComponentCanDeactivate,
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot) {
    return component.canDeactivate ? component.canDeactivate() : true;
  }
}

