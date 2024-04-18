export class ProductModel{
    ProductId!:number;
    ProductName!:string;
    Description!:string;
    Content!:string;
    Price!:number;
    PhotoReview!:string;
    IsChecked!:boolean;

    /**
     *
     */
    constructor(productId:number,productName:string,description:string,content:string,price:number,photoReview:string,isChecked:boolean) {
        this.ProductId=productId;
        this.ProductName=productName
        this.Description=description
        this.Content=content
        this.Price=price
        this.PhotoReview=photoReview
        this.IsChecked=isChecked
    }
}

export class AddProductRequest{
    ProductName!:string;
    Description!:string;
    Content!:string;
    Price!:number;
    PhotoReview!:File

    constructor( productName:string,
        description:string,
        content:string,
        price:number,
        photoReview:File) {
        this.ProductName=productName;
        this.Description=description;
        this.Content=content;
        this.Price=price;
        this.PhotoReview=photoReview;
    }
}