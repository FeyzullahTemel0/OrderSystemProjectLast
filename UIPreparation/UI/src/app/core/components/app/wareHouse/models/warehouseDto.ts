export class WareHouseDto {
    id?: number;
    createdUserId?: number;
    createdDate?: (Date | any);
    lastUpdatedUserId?: number;
    lastUpdatedDate?: (Date | any);
    status: boolean;
    isDeleted: boolean;
    productId?: number;
    productName?: string;
    size?: number;
    amount?: number;
    isReady: boolean;
}