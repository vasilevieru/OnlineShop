import { Injectable } from '@angular/core';
import { FileService } from '../services';
import { Observable, MonoTypeOperatorFunction, Subject, timer, concat, merge, EMPTY, of } from 'rxjs';
import { take, switchMap, map, filter, startWith, scan, shareReplay, mergeMap, mergeMapTo, catchError } from 'rxjs/operators';
import { ImageState } from './image-state';
import { ImageAction, Action } from './actions';
import { Image } from './image';

const concurrent = 7;
const delay = 2000;

class ImageStoreInternal {
    constructor(
        private getImageBlob: (id: number) => Observable<Blob>
    ) { }

    private actions$: Subject<Action> = new Subject<Action>();

    private addImage$: Observable<ImageAction> = this.actions$.pipe(
        ofType('Add image'),
        mergeMap((action) => {
            const timeout = timer(delay).pipe(mergeMapTo(EMPTY));
            return concat(
                this.getImageBlob(action.payload).pipe(
                    map((payload) => ({
                        type: 'Add image success',
                        payload: this.createLoadedImage(action.payload, payload)
                    })),
                    catchError((error) => of({
                        type: 'Add image failed',
                        payload: this.createImageWithError(action.payload, error)
                    }))
                ),
                timeout
            );
        }, concurrent)
    );

    private dispatcher$: Observable<Action> =
        merge(
            this.actions$,
            this.addImage$
        );

    private state$: Observable<ImageState> =
        this.dispatcher$.pipe(
            startWith({ images: [] }),
            scan((state: ImageState, action: Action): ImageState => {
                switch (action.type) {
                    case 'Delete image':
                        return { images: this.findAndDeleteImage(state.images, action.payload) };
                    case 'Add image':
                        return { images: this.createOrUpdateNotLoadedImage(state.images, action.payload) };
                    case 'Add image success':
                    case 'Add image failed':
                        return { images: this.findAndFillImage(state.images, (<ImageAction>action).payload) };
                    default:
                        return state;
                }
            }),
            shareReplay(1)
        );


    getImageUrlById(id: number): Observable<Blob> {
        return this.state$.pipe(
            take(1),
            switchMap((state) => {
                const image = state.images.find(i => i.id === id);
                if (image == null || image.error != null) {
                    this.dispatch({ type: 'Add image', payload: id });
                }
                return this.state$;
            }),
            map(state => state.images.find(i => i.id === id)),
            filter(image => image != null && !image.loading && !image.error),
            map(image => image.blob)
        );
    }

    eleteImageFromStore(id: number): void {
        this.dispatch({ type: 'Delete image', payload: id });
    }

    public dispatch(action: Action): void {
        this.actions$.next(action);
    }

    private createOrUpdateNotLoadedImage(images: Image[], id: number): Image[] {
        const foundImageIndex = images.findIndex(i => i.id === id);
        if (foundImageIndex !== -1) {
            images[foundImageIndex].loading = true;
            images[foundImageIndex].blob = null;
            images[foundImageIndex].error = null;
        } else {
            images.push({ id, loading: true, blob: null });
        }
        return images;
    }

    private createImageWithError(id: number, error: Error): Image {
        return { id, loading: false, blob: null, error };
    }

    private createLoadedImage(id: number, image: Blob): Image {
        return { id, loading: false, blob: image };
    }

    private findAndFillImage(images: Image[], image: Image): Image[] {
        const foundImageIndex = images.findIndex(i => i.id === image.id);
        if (foundImageIndex !== -1) {
            images[foundImageIndex].loading = false;
            images[foundImageIndex].blob = image.blob;
            images[foundImageIndex].error = image.error;
        }
        return images;
    }

    private findAndDeleteImage(images: Image[], id: number): Image[] {
        const foundImageIndex = images.findIndex(i => i.id === id);
        if (foundImageIndex >= 0) {
            images.splice(foundImageIndex, 1);
        }
        return images;
    }

}

function ofType<T extends Action>(type: string): MonoTypeOperatorFunction<T> {
    return filter((_) => type === _.type);
}


@Injectable({
    providedIn: 'root'
})
export class ImageStore {
    private images: ImageStoreInternal;
    private thumbnails: ImageStoreInternal;

    constructor(private fileUploadService: FileService) {
        this.images = new ImageStoreInternal(fileUploadService.getImageBlob.bind(fileUploadService));
    }

    getImageUrlById(id: number): Observable<Blob> {
        return this.images.getImageUrlById(id);
    }

    getImageThumbnailUrlById(id: number): Observable<Blob> {
        return this.thumbnails.getImageUrlById(id);
    }

    deleteImageFromStore(id: number): void {
        this.deleteImage(this.images, id);
        this.deleteImage(this.thumbnails, id);
    }

    private deleteImage(images: ImageStoreInternal, id: number): void {
        images.dispatch({ type: 'Delete image', payload: id });
    }
}

