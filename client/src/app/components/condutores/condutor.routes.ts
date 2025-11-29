import { inject } from '@angular/core';
import { ActivatedRouteSnapshot, Routes } from '@angular/router';

//import { CadastrarCondutor } from './cadastrar/cadastrar-condutor';
//import { EditarCondutor } from './editar/editar-condutor';
//import { ExcluirCondutor } from './excluir/excluir-consutor';
import { ListarCondutores } from './listar/listar-condutores';
import { CondutorService } from './condutor.service';

export const listarCondutoresResolver = () => {
  return inject(CondutorService).selecionarTodos();
};

export const detalhesCondutorResolver = (route: ActivatedRouteSnapshot) => {
  const condutorService = inject(CondutorService);

  if (!route.paramMap.get('id')) throw new Error('O parâmetro id não foi fornecido.');

  const condutorId = route.paramMap.get('id')!;

  return condutorService.selecionarPorId(condutorId);
};

export const condutorRoutes: Routes = [
  {
    path: '',
    children: [
      {
        path: '',
        component: ListarCondutores,
        resolve: { condutores: listarCondutoresResolver },
      },
      //{
      //  path: 'cadastrar',
      //  component: CadastrarCondutor,
      //},
      //{
      //  path: 'editar/:id',
      //  component: EditarCondutor,
      //  resolve: { condutor: detalhesCondutorResolver},
      //},
      //{
      //  path: 'excluir/:id',
      //  component: ExcluirCondutor,
      //  resolve: { condutor: detalhesCondutorResolver },
      //},
    ],
    providers: [CondutorService],
  },
];
