import { inject } from '@angular/core';
import { ActivatedRouteSnapshot, Routes } from '@angular/router';
import { TaxaService } from './taxa.service';


export const listarTaxasResolver = () => {
  return inject(TaxaService).selecionarTodos();
};

export const detalhesTaxaResolver = (route: ActivatedRouteSnapshot) => {
  const taxaService = inject(TaxaService);

  if (!route.paramMap.get('id')) throw new Error('O parâmetro id não foi fornecido.');

  const taxaId = route.paramMap.get('id')!;

  return taxaService.selecionarPorId(taxaId);
};

export const taxaRoutes: Routes = [
  {
    path: '',
    children: [
      //{
      //  path: '',
      //  component: ListarTaxas,
      //  resolve: { taxas: listarTaxasResolver },
      //},
      //{
      //  path: 'cadastrar',
      //  component: CadastrarTaxa,
      //},
      //{
      //  path: 'editar/:id',
      //  component: EditarTaxa,
      //  resolve: { taxa: detalhesTaxaResolver },
      //},
      //{
      //  path: 'excluir/:id',
      //  component: ExcluirTaxa,
      //  resolve: { taxa: detalhesTaxaResolver },
      //},
    ],
    providers: [TaxaService],
  },
];
