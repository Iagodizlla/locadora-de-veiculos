import { inject } from '@angular/core';
import { ActivatedRouteSnapshot, Routes } from '@angular/router';
import { AluguelService } from './aluguel.service';
import { ListarAlugueis } from './listar/listar-alugueis';


export const listarAlugueisResolver = () => {
  return inject(AluguelService).selecionarTodos();
};

export const detalhesAluguelResolver = (route: ActivatedRouteSnapshot) => {
  const aluguelService = inject(AluguelService);

  if (!route.paramMap.get('id')) throw new Error('O parâmetro id não foi fornecido.');

  const aluguelId = route.paramMap.get('id')!;

  return aluguelService.selecionarPorId(aluguelId);
};

export const aluguelRoutes: Routes = [
  {
    path: '',
    children: [
      {
        path: '',
        component: ListarAlugueis,
        resolve: { alugueis: listarAlugueisResolver },
      },
      // {
      //   path: 'cadastrar',
      //   component: CadastrarAluguel,
      // },
      // {
      //   path: 'editar/:id',
      //   component: EditarAluguel,
      //   resolve: { alugueis: detalhesAluguelResolver },
      // },
      // {
      //   path: 'excluir/:id',
      //   component: ExcluirAluguel,
      //   resolve: { alugueis: detalhesAluguelResolver },
      // },
    ],
    providers: [AluguelService],
  },
];
