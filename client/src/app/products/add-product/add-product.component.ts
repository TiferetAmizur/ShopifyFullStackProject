import { Component } from '@angular/core';
import { ProductService } from '../../products/product.service';
import { Router } from '@angular/router';
import { Product } from '../../models/product.model';
import { FormsModule } from '@angular/forms'; 
import { CommonModule } from '@angular/common'; 

@Component({
  selector: 'app-add-product',
  imports: [FormsModule, CommonModule],
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.css']
})
export class AddProductComponent {
  newProduct: Product = {
    productId: 0,
    productName: '',
    addedTimestamp: new Date().toISOString(),
    inStock: false,
    stockArrivalDate: '',
  };

  constructor(private productService: ProductService, private router: Router) {}

  onSubmit(): void {
    this.productService.addProduct(this.newProduct).subscribe({
      next: (product) => {
        alert('Product add successfully!');
        this.router.navigate(['/products']);  // Redirect to product list after adding
      },
      error: (error) => {
        console.error('Error adding product:', error);
      }
    });
  }
}
