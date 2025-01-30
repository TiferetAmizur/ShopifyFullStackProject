import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../products/product.service';
import { Product } from '../../models/product.model';
import { FormsModule } from '@angular/forms'; // Import FormsModule
import { CommonModule } from '@angular/common'; // Import CommonModule for *ngIf and other common directives
import { Router } from '@angular/router';
import { AuthService } from '../../auth/auth.service';

@Component({
  selector: 'app-product-list',
  standalone: true, // This makes the component standalone
  imports: [FormsModule, CommonModule],
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {
  products: Product[] = [];
  isAdmin: boolean = false;  // Flag to track if the user is an admin

  constructor(private authService: AuthService, private productService: ProductService, private router: Router) { }

  ngOnInit(): void {
    this.fetchProducts();
    this.isAdmin = this.authService.isAdmin;
  }

  checkIfAdmin(): void {
    const role = this.authService.getUserRole();  // Get user role from the token
    this.isAdmin = role === 'Admin';  // Set the isAdmin flag to true if role is 'admin'
  }

  fetchProducts(): void {
    this.productService.getAllProducts().subscribe({
      next: (products) => {
        this.products = products; // products is now directly the Product[] array
      },
      error: (err) => {
        console.error('Error loading products:', err);
      }
    });
  }

  onAddProduct(): void {
    // Redirect to the add product page or show a form (you can modify as per your requirements)
    if (this.isAdmin) {
      this.router.navigate(['/add-product']);  // Make sure to create the AddProduct component and route
    } else {
      alert('Only admin can add products');
    }
  }
  
  // Method to handle editing a product
  onEditProduct(id: number): void {
    if (this.isAdmin) {
      this.router.navigate(['/edit-product', id]);
    } else {
      alert('Only admin can edit products');
    }
  }

  // Method to handle deleting a product
  onDeleteProduct(id: number): void {
    if (confirm('Are you sure you want to delete this product?')) {
      this.productService.deleteProduct(id).subscribe(
        () => {
          console.log('Product deleted');
          this.fetchProducts(); // Refresh the list of products
        },
        (error) => {
          console.error('Error deleting product:', error);
        }
      );
    }
  }
}
