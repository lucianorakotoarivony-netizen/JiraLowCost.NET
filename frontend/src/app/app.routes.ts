import { Routes } from '@angular/router';
import { authGuard } from './guards/auth-guard';
import { TaskItemList } from './pages/task-item-list/task-item-list';

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
            loadComponent: () => import('./pages/task-item-list/task-item-list').then(m => m.TaskItemList)
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
