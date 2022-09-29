﻿
export class OrderDetailsDto { //***
    id?: number;
    createdUserId?: number;
    createdDate?: (Date | any);
    lastUpdatedUserId?: number;
    lastUpdatedDate?: (Date | any);
    status: boolean;
    isDeleted: boolean;
    customerId?: number;
    customerName?: string;
    productId?: number;
    productName?: string;
    amount?: number;
    size?: number;
}