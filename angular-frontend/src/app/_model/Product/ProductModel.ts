export class ProductModel{
    ProductId!:number;
    ProductName!:string;
    Description!:string;
    Content!:string;
    Price!:number;
    PhotoReview!:string;

    /**
     *
     */
    constructor(productId:number,productName:string,description:string,content:string,price:number,photoReview:string) {
        this.ProductId=productId;
        this.ProductName=productName
        this.Description=description
        this.Content=content
        this.Price=price
        this.PhotoReview=photoReview
    }
}