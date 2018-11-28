namespace Products {
    export interface IProductsService {
        getProducts(): Promise<any>;
        getProduct(productId: number): Promise<any>;
        editProduct(product: object): Promise<any>;
        createProduct(product: object): Promise<any>;
        deleteProduct(productId: number): Promise<any>;
    }
}