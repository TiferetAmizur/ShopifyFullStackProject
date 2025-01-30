import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductService } from '../../products/product.service';
import { Product } from '../../models/product.model';
import { FormsModule } from '@angular/forms'; 
import { CommonModule } from '@angular/common'; 

@Component({
  selector: 'app-product-edit',
  imports: [FormsModule, CommonModule],
  templateUrl: './product-edit.component.html',
  styleUrls: ['./product-edit.component.css']
})
export class ProductEditComponent implements OnInit {
  product: Product = new Product();

  constructor(
    private route: ActivatedRoute,
    private productService: ProductService,
    private router: Router
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.params['id'];
    this.productService.getProduct(id).subscribe((product) => {
      this.product = product || new Product();
    })
  }

  saveChanges(): void {
    this.productService.updateProduct(this.product.productId, this.product).subscribe(() => {
      alert('Product updated successfully!');
      this.router.navigate(['/products']);
    });
  }
}
