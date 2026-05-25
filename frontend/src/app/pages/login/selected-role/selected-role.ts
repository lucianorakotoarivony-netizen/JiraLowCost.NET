import { Component, inject, signal} from '@angular/core';
import { NavigationEnd, Router, RouterOutlet } from '@angular/router';
import { Auth } from '../../../Services/auth';
import { filter } from 'rxjs';
import { USER_ROLE } from '../../../../Constants/user-role';


@Component({
  selector: 'app-selected-role',
  imports: [RouterOutlet],
  templateUrl: './selected-role.html',
  styleUrl: './selected-role.scss',
})
export class SelectedRole {
  router = inject(Router);
  auth = inject(Auth);
  currentUser = this.auth.currentUsername;
  currentUserRole = this.auth.currentUserRole;
  isChildRouteActive = signal(false);
  selectedRole(page: 'admin' | 'dev'): void{
    if (page === 'admin'){
      this.router.navigate(["selected-role/dashboard-admin"]);
    } else{
      this.router.navigate(["selected-role/dashboard-dev"]);
    }
  }
}
