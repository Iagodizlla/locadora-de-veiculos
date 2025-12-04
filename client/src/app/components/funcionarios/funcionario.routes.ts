import { inject } from '@angular/core';
import { ActivatedRouteSnapshot, Routes } from '@angular/router';
import { FuncionarioService } from './funcionario.service';

// import { CadastrarFuncionario } from './cadastrar/cadastrar-funcionario';
// import { EditarFuncionario } from './editar/editar-funcionario';
// import { AutoEditarFuncionario } from './auto-editar/auto-editar-funcionario';
// import { ExcluirFuncionario } from './excluir/excluir-funcionario';
import { ListarFuncionarios } from './listar/listar-funcionarios';

export const listarFuncionariosResolver = () => {
  return inject(FuncionarioService).selecionarTodos();
};

export const detalhesFuncionarioResolver = (route: ActivatedRouteSnapshot) => {
  const funcionarioService = inject(FuncionarioService);

  if (!route.paramMap.get('id')) throw new Error('O parâmetro id não foi fornecido.');

  const funcionarioId = route.paramMap.get('id')!;

  return funcionarioService.selecionarPorId(funcionarioId);
};

export const funcionarioRoutes: Routes = [
  {
    path: '',
    children: [
      {
       path: '',
       component: ListarFuncionarios,
       resolve: { fucionarios: listarFuncionariosResolver },
      },
      // {
      //  path: 'cadastrar',
      //  component: CadastrarFuncionario,
      // },
      // {
      //  path: 'editar/:id',
      //  component: EditarFuncionario,
      //  resolve: { fucionario: detalhesFuncionarioResolver },
      // },
      // {
      //  path: 'excluir/:id',
      //  component: ExcluirFuncionario,
      //  resolve: { fucionario: detalhesFuncionarioResolver },
      // },
      // {
      //   path: 'auto-editar',
      //   component: AutoEditarFuncionario,
      //   //resolve: { funcionario: autoEditarFuncionarioResolver },
      // },
    ],
    providers: [FuncionarioService],
  },
];
