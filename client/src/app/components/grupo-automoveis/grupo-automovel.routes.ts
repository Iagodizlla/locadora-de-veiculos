import { inject } from '@angular/core';
import { ActivatedRouteSnapshot, Routes } from '@angular/router';

import { CadastrarGrupoAutomovel } from './cadastrar/cadastrar-grupo-automovel';
//import { EditarGrupoAutomovel } from './editar/editar-grupo-automovel';
import { ExcluirGrupoAutomovel } from './excluir/excluir-grupo-automovel';
import { ListarGruposAutomoveis } from './listar/listar-grupos-automoveis';
import { GrupoAutomovelService } from './grupo-automovel.service';

export const listarGruposAutomoveisResolver = () => {
  return inject(GrupoAutomovelService).selecionarTodos();
};

export const detalhesGrupoAutomovelResolver = (route: ActivatedRouteSnapshot) => {
  const grupoAutomovelService = inject(GrupoAutomovelService);

  if (!route.paramMap.get('id')) throw new Error('O parâmetro id não foi fornecido.');

  const grupoAutomovelId = route.paramMap.get('id')!;

  return grupoAutomovelService.selecionarPorId(grupoAutomovelId);
};

export const grupoAutomovelRoutes: Routes = [
  {
    path: '',
    children: [
      {
        path: '',
        component: ListarGruposAutomoveis,
        resolve: { gruposAutomoveis: listarGruposAutomoveisResolver },
      },
      {
        path: 'cadastrar',
        component: CadastrarGrupoAutomovel,
      },
      //{
      //  path: 'editar/:id',
      //  component: EditarGrupoAutomovel,
      //  resolve: { grupoAutomovel: detalhesGrupoAutomovelResolver },
      //},
      {
        path: 'excluir/:id',
        component: ExcluirGrupoAutomovel,
        resolve: { grupoAutomovel: detalhesGrupoAutomovelResolver },
      },
    ],
    providers: [GrupoAutomovelService],
  },
];
