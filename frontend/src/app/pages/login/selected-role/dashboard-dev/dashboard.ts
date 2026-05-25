import { Component, inject } from '@angular/core';
import { Auth } from '../../../../Services/auth';
import { RouterLink, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  imports: [RouterLink, RouterOutlet],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss',
})
export class Dashboard {
  auth = inject(Auth);
  currentUser = this.auth.currentUsername;
  currentUserRole = this.auth.currentUserRole;
  logout(): void{
    this.auth.logout();
}
}
