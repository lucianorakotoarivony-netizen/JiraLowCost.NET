import { CanActivateFn, Router } from '@angular/router';
import { Auth } from '../Services/auth';
import { inject } from '@angular/core';
import { RoleAdmin } from '../../Constants/user-role';

export const roleGuard: CanActivateFn = () => {
  const auth = inject(Auth);
  const router = inject(Router);
  const RoleUser = auth.currentUserRole();
  if (!RoleAdmin.includes(RoleUser!)){
    return router.parseUrl('selected-role/dashboard-dev/taskitem');
  }
  return true;
};
