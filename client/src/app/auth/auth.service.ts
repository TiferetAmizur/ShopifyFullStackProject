import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthResponse } from '../models/auth-response.model';
import { jwtDecode } from 'jwt-decode';
import { environment } from '../../environments/environment';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = `${environment.apiBaseUrl}${environment.apiLoginEndPoint}`;

  constructor(private http: HttpClient) { }

  login(username: string, password: string): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(this.apiUrl, { username, password }, {
      withCredentials: true  // This ensures cookies and authorization headers are sent with the request
    });
  }

  // Extract the token from cookies
  getTokenFromCookie(): string | null {
    const matches = document.cookie.match(/(^| )token=([^;]+)/);
    return matches ? matches[2] : null;
  }

  // Method to get the role from the token
  getUserRole(): string | null {
    const token = this.getTokenFromCookie();
    if (token) {
      const decoded: any = jwtDecode(token);  // Decode the JWT token
      return decoded.role || null;  // Extract the role
    }
    return null;
  }

  // Check if the user is authenticated
  isAuthenticated(): boolean {
    return !!this.getTokenFromCookie(); // Returns true if token exists
  }

  // Getter to check if the user is an admin
  get isAdmin(): boolean {
    return this.getUserRole() === 'Admin';  // Check if the user's role is 'Admin'
  }
}
