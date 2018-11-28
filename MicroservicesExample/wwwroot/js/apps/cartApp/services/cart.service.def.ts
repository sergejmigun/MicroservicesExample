namespace Cart {
    export interface ICartService {
        getCart(): Promise<any>;
        removeCartItem(productId: number): Promise<any>;
        updateCartItem(productId: number, quantity: number): Promise<any>;
        processOrder(submitModel: object): Promise<any>;
        addToCart(productId: number, quantity?: number): Promise<any>;
    }
}