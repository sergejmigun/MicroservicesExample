namespace Orders {
    export interface IOrdersService {
        getOrders(): Promise<any>;
        getOrder(orderId: number): Promise<any>;
        deleteOrder(orderId: number): Promise<any>;
        completeOrder(orderId: number): Promise<any>;
        addProduct(orderId: number, productId: number, quantity: number): Promise<any>;
        updateProduct(orderId: number, productId: number, quantity: number): Promise<any>;
        deleteProduct(orderId: number,productId: number): Promise<any>;
    }
}