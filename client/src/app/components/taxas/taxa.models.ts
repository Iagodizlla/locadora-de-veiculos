export enum ServicoEnum {
  PrecoFixo = 'PrecoFixo',
  CobrancaDiaria = 'CobrancaDiaria'
}

export interface ListarTaxasApiResponseModel {
  quantidadeRegistros: number;
  registros: ListarTaxasModel[];
}

export interface ListarTaxasModel {
  id: string;
  nome: string;
  preco: number;
  servico: ServicoEnum;
}

export interface DetalhesTaxaModel {
  id: string;
  nome: string;
  preco: number;
  servico: ServicoEnum;
}

export interface CadastrarTaxaModel {
  nome: string;
  preco: number;
  servico: ServicoEnum;
}

export interface CadastrarTaxaResponseModel {
  id: string;
}

export interface EditarTaxaModel {
  nome: string;
  preco: number;
  servico: ServicoEnum;
}

export interface EditarTaxaResponseModel {
  nome: string;
  preco: number;
  servico: ServicoEnum;
}
