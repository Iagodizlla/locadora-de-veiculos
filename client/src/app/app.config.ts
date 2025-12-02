import { map, take } from 'rxjs';

import { ApplicationConfig,
  inject,
  provideBrowserGlobalErrorListeners,
  provideZonelessChangeDetection,
} from '@angular/core';
import { CanActivateFn, provideRouter, Router, Routes } from '@angular/router';

import { provideAuth } from './components/auth/auth.provider';
import { AuthService } from './components/auth/auth.service';
import { provideNotifications } from './components/shared/notificacao/notificacao.provider';

const usuarioDesconhecidoGuard: CanActivateFn = () => {
  const authService = inject(AuthService);
  const router = inject(Router);

  return authService.accessToken$.pipe(
    take(1),
    map((token) => (!token ? true : router.createUrlTree(['/inicio'])))
  );
};

const usuarioAutenticadoGuard: CanActivateFn = () => {
  const authService = inject(AuthService);
  const router = inject(Router);

  return authService.accessToken$.pipe(
    take(1),
    map((token) => (token ? true : router.createUrlTree(['/auth/login'])))
  );
};

const routes: Routes = [
  { path: '', redirectTo: 'auth/login', pathMatch: 'full' },
  {
    path: 'auth',
    loadChildren: () => import('./components/auth/auth.routes').then((r) => r.authRoutes),
    canMatch: [usuarioDesconhecidoGuard],
  },
  {
    path: 'inicio',
    loadComponent: () => import('./components/inicio/inicio').then((c) => c.Inicio),
    canMatch: [usuarioAutenticadoGuard],
  },
  {
    path: 'grupo-automoveis',
    loadChildren: () => import('./components/grupo-automoveis/grupo-automovel.routes').then((c) => c.grupoAutomovelRoutes),
    canMatch: [usuarioAutenticadoGuard],
  },
  {
    path: 'automoveis',
    loadChildren: () => import('./components/automoveis/automovel.routes').then((c) => c.automovelRoutes),
    canMatch: [usuarioAutenticadoGuard],
  },
  {
    path: 'condutores',
    loadChildren: () => import('./components/condutores/condutor.routes').then((c) => c.condutorRoutes),
    canMatch: [usuarioAutenticadoGuard],
  },
  {
    path: 'clientes',
    loadChildren: () => import('./components/clientes/cliente.routes').then((c) => c.clienteRoutes),
    canMatch: [usuarioAutenticadoGuard],
  },
  {
    path: 'planos',
    loadChildren: () => import('./components/planos/plano.routes').then((c) => c.planoRoutes),
    canMatch: [usuarioAutenticadoGuard],
  },
  {
  path: 'configuracoes',
  loadComponent: () => import('./components/configuracoes/editar/editar-config').then(c => c.EditarConfig),
  canMatch: [usuarioAutenticadoGuard],
}

];

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideZonelessChangeDetection(),
    provideRouter(routes),

    provideNotifications(),
    provideAuth(),
  ],
};
