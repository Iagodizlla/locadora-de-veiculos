import { inject } from '@angular/core';
import { ActivatedRouteSnapshot, Routes } from '@angular/router';
import { AluguelService } from './aluguel.service';
import { ListarAlugueis } from './listar/listar-alugueis';
import { ExcluirAluguel } from './excluir/excluir-aluguel';
import { ClienteService } from '../clientes/cliente.service';
import { CondutorService } from '../condutores/condutor.service';
import { TaxaService } from '../taxas/taxa.service';
import { AutomovelService } from '../automoveis/automovel.service';
import { CadastrarAluguel } from './cadastrar/cadastrar-aluguel';
import { PlanoService } from '../planos/plano.service';


export const listarAlugueisResolver = () => {
  return inject(AluguelService).selecionarTodos();
};

export const detalhesAluguelResolver = (route: ActivatedRouteSnapshot) => {
  const aluguelService = inject(AluguelService);

  if (!route.paramMap.get('id')) throw new Error('O parâmetro id não foi fornecido.');

  const aluguelId = route.paramMap.get('id')!;

  return aluguelService.selecionarPorId(aluguelId);
};

export const listarClientesResolver = () => {
  return inject(ClienteService).selecionarTodos();
};

export const listarPlanosResolver = () => {
  return inject(PlanoService).selecionarTodos();
}

export const listarCondutoresResolver = () => {
  return inject(CondutorService).selecionarTodos();
};

export const listarTaxasResolver = () => {
  return inject(TaxaService).selecionarTodos();
}

export const listarAutomoveisResolver = () => {
  return inject(AutomovelService).selecionarTodos();
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
      {
        path: 'cadastrar',
        component: CadastrarAluguel,
        resolve: {
             clientes: listarClientesResolver,
             condutores: listarCondutoresResolver,
             automoveis: listarAutomoveisResolver,
             taxas: listarTaxasResolver,
             planos: listarPlanosResolver,
        },
      },
      // {
      //   path: 'editar/:id',
      //   component: EditarAluguel,
      //   resolve: { alugueis: detalhesAluguelResolver },
      // },
      {
        path: 'excluir/:id',
        component: ExcluirAluguel,
        resolve: { aluguel: detalhesAluguelResolver },
      },
    ],
    providers: [AluguelService, AutomovelService, ClienteService, CondutorService, PlanoService, TaxaService],
  },
];
