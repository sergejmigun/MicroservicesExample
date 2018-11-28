namespace Common {
    export interface IUtilsService {
        toCamelCase(obj: object): object;
        toUpperCamelCase(obj: object): object;
    }
}