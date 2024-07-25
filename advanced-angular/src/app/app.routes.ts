import { Routes } from '@angular/router';
import { RequireLoginGuard } from './guards/require-login.guard';

export const routes: Routes = [
    {
        path: '',
        loadComponent: () => import('./home/home.component').then(m => m.HomeComponent)
    },
    {
        path: 'unauthorized',
        loadComponent: () => import('./unauthorized/unauthorized.component').then(m => m.UnauthorizedComponent)
    },
    {
        path: 'protected',
        loadComponent: () => import('./protected/protected.component').then(m => m.ProtectedComponent),
        canActivate: [RequireLoginGuard]
    },
    {
        path: 'nested',
        loadComponent: () => import('./nested/nested.component').then(m => m.NestedComponent),
        children: [
            {
                path: '',
                pathMatch: 'full',
                redirectTo: 'page-1'
            },
            {
                path: 'page-1',
                loadComponent: () => import('./nested/page-one/page-one.component').then(m => m.PageOneComponent),
            },
            {
                path: 'page-2',
                loadComponent: () => import('./nested/page-two/page-two.component').then(m => m.PageTwoComponent),
            },
            {
                path: 'page-3',
                loadComponent: () => import('./nested/page-three/page-three.component').then(m => m.PageThreeComponent),
            }
        ]
    }
];
