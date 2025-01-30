import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Product } from '../models/product.model';
import { map } from 'rxjs/operators'; // Import map operator
import { AuthService } from '../auth/auth.service';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private apiUrl = `${environment.apiBaseUrl}${environment.apiProductsEndPoint}`; 

  constructor(private http: HttpClient,private authService: AuthService) {}

  getAllProducts(): Observable<Product[]> {
    const token = this.authService.getTokenFromCookie(); // Use AuthService

    // Add the token to the Authorization header if it exists
    let headers = new HttpHeaders();
    if (token) {
      headers = headers.set('Authorization', `Bearer ${token}`);
    }

    // Send the request with the Authorization header
    return this.http.get<ApiResponse>(this.apiUrl, {headers, withCredentials: true })
    .pipe(
      map((response) => response.$values) // Extract the $values array from ApiResponse
    );
  }
  
   // Get a single product by ID
   getProduct(id: number): Observable<Product> {
    const token = this.authService.getTokenFromCookie(); // Use AuthService

    let headers = new HttpHeaders();
    if (token) {
      headers = headers.set('Authorization', `Bearer ${token}`);
    }

    return this.http.get<Product>(`${this.apiUrl}/${id}`, { headers, withCredentials: true });
  }

  // Add a new product
  addProduct(product: Product): Observable<Product> {
    const token = this.authService.getTokenFromCookie(); // Use AuthService

    let headers = new HttpHeaders();
    if (token) {
      headers = headers.set('Authorization', `Bearer ${token}`);
    }

    return this.http.post<Product>(this.apiUrl, product, { headers, withCredentials: true });
  }

  // Update an existing product
  updateProduct(id: number, updatedProduct: Product): Observable<Product> {
    const token = this.authService.getTokenFromCookie(); // Use AuthService

    let headers = new HttpHeaders();
    if (token) {
      headers = headers.set('Authorization', `Bearer ${token}`);
    }

    return this.http.put<Product>(`${this.apiUrl}/${id}`, updatedProduct, { headers, withCredentials: true });
  }

  // Delete a product
  deleteProduct(id: number): Observable<void> {
    const token = this.authService.getTokenFromCookie(); // Use AuthService

    let headers = new HttpHeaders();
    if (token) {
      headers = headers.set('Authorization', `Bearer ${token}`);
    }

    return this.http.delete<void>(`${this.apiUrl}/${id}`, { headers, withCredentials: true });
  }
}

interface ApiResponse {
  $values: Product[];
}
