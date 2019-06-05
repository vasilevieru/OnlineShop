import { Product } from '@product';

export class FileDetails {
    id: number;
    name: string;
    path: string;
    mimeType: string;
    length: number;
    imageSource: string;
    userId: number;
    product: Product;
}
