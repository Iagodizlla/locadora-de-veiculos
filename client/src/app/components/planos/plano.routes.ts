import { inject } from '@angular/core';
import { ActivatedRouteSnapshot, Routes } from '@angular/router';

import { GrupoAutomovelService } from '../grupo-automoveis/grupo-automovel.service';

import { CadastrarPlano } from './cadastrar/cadastrar-plano';
import { EditarPlano } from './editar/editar-plano';
import { ExcluirPlano } from './excluir/excluir-plano';
import { ListarPlanos } from './listar/listar-planos';
import { PlanoService } from './plano.service';

export const listarPlanosResolver = () => {
  return inject(PlanoService).selecionarTodos();
};

export const detalhesPlanoResolver = (route: ActivatedRouteSnapshot) => {
  const planoService = inject(PlanoService);

  if (!route.paramMap.get('id')) throw new Error('O parâmetro id não foi fornecido.');

  const planoId = route.paramMap.get('id')!;

  return planoService.selecionarPorId(planoId);
};

export const listarGruposAutomoveisResolver = () => {
  return inject(GrupoAutomovelService).selecionarTodos();
};

export const planoRoutes: Routes = [
  {
    path: '',
    children: [
      {
        path: '',
        component: ListarPlanos,
        resolve: { planos: listarPlanosResolver },
      },
      {
        path: 'cadastrar',
        component: CadastrarPlano,
        resolve: { grupoAutomoveis: listarGruposAutomoveisResolver},
      },
      {
        path: 'editar/:id',
        component: EditarPlano,
        resolve: { plano: detalhesPlanoResolver, grupoAutomoveis: listarGruposAutomoveisResolver },
      },
      {
        path: 'excluir/:id',
        component: ExcluirPlano,
        resolve: { plano: detalhesPlanoResolver },
      },
    ],
    providers: [PlanoService, GrupoAutomovelService],
  },
];
