import { Component, OnInit, Input, OnChanges, SimpleChanges, SimpleChange, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { take, tap } from 'rxjs/operators';
import { ImageStore } from 'app/shared/image-store/image-store';

@Component({
  selector: 'app-image',
  templateUrl: './image.component.html',
  styleUrls: ['./image.component.scss']
})
export class ImageComponent implements OnChanges, OnInit, OnDestroy {
  @Input() imageId: number;
  @Input() isThumbnail = true;
  @Input() styleClasses: string;
  @Input() loaderDiameter = 50;
  @Input() loaderThickness = 5;
  @Input() borderRadius = '0px';
  @Input() loadingShadow = false;

  imageSource: string;
  subscription: Subscription;
  isLoading = false;

  constructor(
    private imageStore: ImageStore
  ) { }

  ngOnInit(): void {
  }

  ngOnDestroy(): void {
    this.revokeImageUrl();
  }

  ngOnChanges(changes: SimpleChanges): void {
    const imageId: SimpleChange = changes['imageId'];
    this.revokeImageUrl();
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
    if (imageId && imageId.currentValue != null && imageId.currentValue > 0) {
      this.loadImage();
    }
  }

  get formatedUrlOfImage(): string {
    return `url(${this.imageSource})`;
  }

  private loadImage(): void {
    this.isLoading = true;

    const image$ = this.isThumbnail
      ? this.imageStore.getImageThumbnailUrlById(this.imageId)
      : this.imageStore.getImageUrlById(this.imageId);

    this.subscription = image$
      .pipe(
        tap(() => this.subscription = null),
        take(1)
      )
      .subscribe(blob => {
          this.imageSource = URL.createObjectURL(blob);
          this.isLoading = false;
        }, () => {
          this.revokeImageUrl();
          this.isLoading = false;
        }
      );
  }

  private revokeImageUrl() {
    if (this.imageSource) {
      URL.revokeObjectURL(this.imageSource);
      this.imageSource = null;
    }
  }
}
