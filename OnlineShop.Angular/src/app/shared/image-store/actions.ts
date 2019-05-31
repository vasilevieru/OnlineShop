import { Image } from './image';

export interface Action {
    type: string;
    payload?: any;
}

export interface ImageAction extends Action {
    type: string;
    payload: Image;
}
