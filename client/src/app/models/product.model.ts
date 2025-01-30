export class Product {
    productId!: number;
    addedTimestamp!: string;
    inStock!: boolean;
    stockArrivalDate!: string | null;  // Keep it as string or use Date if conversion is needed
    productName!: string;
  }
  