import { Routes } from '@angular/router';
import { authGuard } from './guards/auth-guard';

export const routes: Routes = [
    {
        path: 'login',
        loadComponent: () => import('./pages/login/login').then(m => m.Login)
    },
    {
        path: 'dashboard',
        loadComponent: () => import('./pages/dashboard/dashboard').then(m => m.Dashboard),
            canActivate:[authGuard],
            children: [
      {
        path: 'taskitem',
        children: [
          {
            path: '',
            loadComponent: () => import('./pages/dashboard/tasks/task-item-list/task-item-list').then(m => m.TaskItemList)
          },
          {
            path: 'create',
            loadComponent: () => import('./pages/dashboard/tasks/task-create/task-create').then(m => m.TaskCreate)
          },
          {
            path:':id',
            loadComponent:() => import('./pages/dashboard/tasks/task-item-detail/task-item-detail').then(m => m.TaskItemDetail)
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
    {
        path: '',
        redirectTo: '/dashboard',
        pathMatch: 'full'
    },
    {
        path: '**',
        redirectTo: '/dashboard'
    }
];
