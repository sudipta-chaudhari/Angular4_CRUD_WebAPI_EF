import { IProduct } from '../interface/iProduct';

export class Product implements IProduct {
    Id: number;
    Name: string;
    Category: string;
    Price: number;
}