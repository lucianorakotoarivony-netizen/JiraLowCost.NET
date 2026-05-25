import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { Router } from '@angular/router';
import { AuthResponse, RegisterDto } from '../models/site.models';
import { RoleAdmin, RoleDev } from '../../Constants/user-role';

@Injectable({
  providedIn: 'root',
})
export class Auth {
  private http = inject(HttpClient);
  private readonly apiUrl = environment.apiUrl;
  private router = inject(Router);
  isAuthenticated = signal<boolean>(!!localStorage.getItem("jwt_token"));
  currentUsername = signal<string | null>(this.getUsername());
  currentUserRole = signal<string | null>(this.getRole());
  currentUserId = signal<string | null>(this.getId());

  CheckRole(userRole: string): string | null{
    if (RoleAdmin.includes(userRole) && !RoleDev.includes(userRole)){
      return "/dashboard-admin";
    } else if (!RoleAdmin.includes(userRole)){
      return "/dashboard-dev";
    }else{
      return null;
    }
  }
  
  login(username: string, password: string){
    return this.http.post<AuthResponse>(`${this.apiUrl}/auth/login/`, {username, password});
  }

  register(dto: RegisterDto){
    return this.http.post<AuthResponse>(`${this.apiUrl}/auth/register/`, dto);
  }

  saveSession(response: AuthResponse): void{
    localStorage.setItem("jwt_token", response.token);
    this.isAuthenticated.set(true);
    this.currentUsername.set(response.username);
    this.currentUserRole.set(response.role);
    const path = this.CheckRole(this.currentUserRole()!) // Arriver ici on est certains que le guard principal à donner son accord donc le rôle ne sera jamais null
    console.log(path);
    if (path !== null){
      this.router.navigate([`/selected-role/${path}/taskitem`]);  
    } else{
      this.router.navigate(["/selected-role/taskitem"]);
    }
    
  }

  logout(){
    localStorage.removeItem('jwt_token');
    this.isAuthenticated.set(false);
    this.router.navigate(['/login']);
  }

  getToken(): string | null{
    return localStorage.getItem("jwt_token");
  }

  private getUsername(): string | null{
    const token = this.getToken();
    if (!token) return null;
    try{
      const payload = JSON.parse(atob(token.split('.')[1]));
      return payload.username;
    } catch(err){
      return null;
    }
  }
  private getRole(): string | null{
    const token = this.getToken();
    if (!token) return null;
    try{
      const payload = JSON.parse(atob(token.split('.')[1]));
      return payload.role;
    } catch(err){
      return null;
    }
  }
  private getId(): string | null{
    const token = this.getToken();
    if (!token) return null;
    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      return payload.id
      
    }catch(err){
      return null;
    }
  }
}
