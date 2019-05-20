import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-mat-loading',
  templateUrl: './mat-loading.component.html',
  styleUrls: ['./mat-loading.component.scss']
})
export class MatLoadingComponent implements OnInit {

  @Input() diameter = 50;
  @Input() strokeWidth = 5;
  @Input() borderRadius = '0px';
  @Input() shadow = true;

  constructor() { }

  ngOnInit() {
  }
}
