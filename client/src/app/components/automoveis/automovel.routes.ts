import { inject } from '@angular/core';
import { ActivatedRouteSnapshot, Routes } from '@angular/router';

import { GrupoAutomovelService } from '../grupo-automoveis/grupo-automovel.service';

import { CadastrarAutomovel } from './cadastrar/cadastrar-automovel';
import { EditarAutomovel } from './editar/editar-automovel';
//import { ExcluirAutomovel } from './excluir/excluir-automovel';
import { ListarAutomoveis } from './listar/listar-automoveis';
import { AutomovelService } from './automovel.service';

export const listarAutomoveisResolver = () => {
  return inject(AutomovelService).selecionarTodos();
};

export const detalhesAutomovelResolver = (route: ActivatedRouteSnapshot) => {
  const automovelService = inject(AutomovelService);

  if (!route.paramMap.get('id')) throw new Error('O parâmetro id não foi fornecido.');

  const automovelId = route.paramMap.get('id')!;

  return automovelService.selecionarPorId(automovelId);
};

export const listarGruposAutomoveisResolver = () => {
  return inject(GrupoAutomovelService).selecionarTodos();
};

export const automovelRoutes: Routes = [
  {
    path: '',
    children: [
      {
        path: '',
        component: ListarAutomoveis,
        resolve: { automoveis: listarAutomoveisResolver },
      },
      {
        path: 'cadastrar',
        component: CadastrarAutomovel,
        resolve: { grupoAutomoveis: listarGruposAutomoveisResolver},
      },
      {
        path: 'editar/:id',
        component: EditarAutomovel,
        resolve: { automovel: detalhesAutomovelResolver, grupoAutomoveis: listarGruposAutomoveisResolver },
      },
      //{
      //  path: 'excluir/:id',
      //  component: ExcluirAutomovel,
      //  resolve: { automovel: detalhesAutomovelResolver },
      //},
    ],
    providers: [AutomovelService, GrupoAutomovelService],
  },
];
