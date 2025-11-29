import { inject } from '@angular/core';
import { ActivatedRouteSnapshot, Routes } from '@angular/router';

//import { CadastrarCliente } from './cadastrar/cadastrar-cliente';
//import { EditarCliente } from './editar/editar-cliente';
//import { ExcluirCliente } from './excluir/excluir-cliente';
//import { ListarClientes } from './listar/listar-clientes';
import { ClienteService } from './cliente.service';

// Resolver para listar todos
export const listarClientesResolver = () => {
  return inject(ClienteService).selecionarTodos();
};

// Resolver para detalhes por ID
export const detalhesClienteResolver = (route: ActivatedRouteSnapshot) => {
  const clienteService = inject(ClienteService);

  const id = route.paramMap.get('id');
  if (!id) throw new Error('O parâmetro id não foi fornecido.');

  return clienteService.selecionarPorId(id);
};

// Rotas do módulo Cliente
export const clienteRoutes: Routes = [
  {
    path: '',
    children: [
      //{
      //  path: '',
      //  component: ListarClientes,
      //  resolve: { clientes: listarClientesResolver }
      //},
      //{
      //  path: 'cadastrar',
      //  component: CadastrarCliente
      //},
      //{
      //  path: 'editar/:id',
      //  component: EditarCliente,
      //  resolve: { cliente: detalhesClienteResolver }
      //},
      //{
      //  path: 'excluir/:id',
      //  component: ExcluirCliente,
      //  resolve: { cliente: detalhesClienteResolver }
      //}
    ],
    providers: [ClienteService]
  }
];
