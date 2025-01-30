import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service'; // Your AuthService

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private authService: AuthService, private router: Router) { }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {

    const userRole = this.authService.getUserRole(); // Get the user role

    // Check if the user is authenticated
    if (this.authService.isAuthenticated()) {
      // Allow Admin users to access all routes, including adding and editing products
      if (userRole === 'Admin') {
        return true;
      }

      // Allow Viewer users to access only the '/products' route
      if (userRole === 'Viewer' && next.url[0].path === 'products') {
        return true;
      }

      // Redirect to the products list if Viewer tries to access the add-product route
      if (userRole === 'Viewer' && next.url[0].path === 'add-product') {
        this.router.navigate(['/products']);
        return false;
      }

      // Redirect to the products list if Viewer tries to access the edit-product route
      if (userRole === 'Viewer' && next.url[0].path === 'edit-product') {
        this.router.navigate(['/products']);
        return false;
      }

      // Redirect to login if no valid role
      this.router.navigate(['/login']);
      return false;
    }

    // Redirect to login page if not authenticated
    this.router.navigate(['/login']);
    return false; // Prevent access
  }
}
