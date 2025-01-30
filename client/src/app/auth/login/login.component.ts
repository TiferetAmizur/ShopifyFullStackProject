import { Component } from '@angular/core';
import { AuthService } from '../../auth/auth.service';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms'; // Import FormsModule
import { CommonModule } from '@angular/common'; // Import CommonModule for *ngIf and other common directives
import { AuthResponse } from '../../models/auth-response.model';

@Component({
  selector: 'app-login',
  standalone: true, // This makes the component standalone
  imports: [FormsModule, CommonModule], // Include FormsModule here
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  username = '';
  password = '';
  errorMessage = '';

  constructor(private authService: AuthService, private router: Router) { }

  login() {
    this.authService.login(this.username, this.password).subscribe({
      next: (response: AuthResponse) => {
        // localStorage.setItem('token', response.token); // store token
        // Save token in a cookie (expires in 1 hour, use secure and httpOnly for production)
        document.cookie = `token=${response.accessToken};path=/;max-age=${60 * 60};secure;SameSite=Strict`;

        this.router.navigate(['/products']); // navigate to the products page
      },
      error: (err) => {
        this.errorMessage = 'Invalid username or password';
      }
    });
  }
}
