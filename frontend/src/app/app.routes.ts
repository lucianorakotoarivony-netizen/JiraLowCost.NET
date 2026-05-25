import { Routes } from '@angular/router';
import { authGuard } from './guards/auth-guard';
import { roleGuard } from './guards/role-guard';
import { inject } from '@angular/core';
import { Auth } from './Services/auth';
import { RoleAdmin } from '../Constants/user-role';

export const routes: Routes = [
  {
    path: 'login',
    loadComponent: () => import('./pages/login/login').then(m => m.Login)
  },
  {
    path: 'selected-role',
    loadComponent: () => import('./pages/login/selected-role/selected-role').then(m => m.SelectedRole),
    canActivate: [authGuard],
    children: [
      // Dashboard Admin (PO)
      {
        path: 'dashboard-admin',
        loadComponent: () => import('./pages/login/selected-role/dashboard-admin/dashboard').then(m => m.Dashboard),
        canActivate: [roleGuard],
        children: [
          {
            path: 'register',
            loadComponent: () => import('./pages/login/selected-role/dashboard-admin/register/register').then(m => m.Register)
          },
          {
            path: 'taskitem',
            children: [
              {
                path: '',
                loadComponent: () => import('./pages/login/selected-role/dashboard-admin/tasks/task-item-list/task-item-list').then(m => m.TaskItemList)
              },
              {
                path: 'create',
                loadComponent: () => import('./pages/login/selected-role/dashboard-admin/tasks/task-create/task-create').then(m => m.TaskCreate)
              },
              {
                path: ':id',
                loadComponent: () => import('./pages/login/selected-role/dashboard-admin/tasks/task-item-detail/task-item-detail').then(m => m.TaskItemDetail)
              }
            ]
          },
          {
            path: '',
            redirectTo: 'taskitem',
            pathMatch: 'full'
          }
        ]
      },
      // Dashboard Dev (SENIOR, JUNIOR)
      {
        path: 'dashboard-dev',
        loadComponent: () => import('./pages/login/selected-role/dashboard-dev/dashboard').then(m => m.Dashboard),
        children: [
          {
            path: 'taskitem',
            children: [
              {
                path: '',
                loadComponent: () => import('./pages/login/selected-role/dashboard-dev/tasks/task-item-list/task-item-list').then(m => m.TaskItemList)
              },
              {
                path: ':id',
                loadComponent: () => import('./pages/login/selected-role/dashboard-dev/tasks/task-item-detail/task-item-detail').then(m => m.TaskItemDetail)
              }
            ]
          },
          {
            path: '',
            redirectTo: 'taskitem',
            pathMatch: 'full'
          }
        ]
      },
    ]
  },
  {
    path: '',
    redirectTo: () => {
      const auth = inject(Auth);
      const userRole = auth.currentUserRole();
      const isAdmin = RoleAdmin.includes(userRole!);
      if (isAdmin){
        return '/selected-role/dahsboard-admin'
      } else{
        return '/selected-role/dashboard-dev'
      }
    },
    pathMatch: 'full'
  },
  {
    path: '**',
    redirectTo: '/selected-role'
  }
];