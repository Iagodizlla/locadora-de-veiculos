export enum CategoriaCnhEnum {
  A = 'A',
  B = 'B',
  C = 'C',
  D = 'D',
  E = 'E',
  AB = 'AB',
  AC = 'AC',
  AD = 'AD',
  AE = 'AE'
}
export interface ListarCondutoresApiResponseModel {
  quantidadeRegistros: number;
  registros: ListarCondutoresModel[];
}

export interface ListarCondutoresModel {
  id: string;
  nome: string;
  cnh: string;
  cpf: string;
  telefone: string;
  categoria: CategoriaCnhEnum;
  validadeCnh: string;
  eCliente: boolean;
}

export interface DetalhesCondutorModel {
  id: string;
  nome: string;
  cnh: string;
  cpf: string;
  telefone: string;
  categoria: CategoriaCnhEnum;
  validadeCnh: string;
  eCliente: boolean;
}

export interface CadastrarCondutorModel {
  nome: string;
  cnh: string;
  cpf: string;
  telefone: string;
  categoria: CategoriaCnhEnum;
  validadeCnh: string;
  eCliente: boolean;
}

export interface CadastrarCondutorResponseModel {
  id: string;
}

export interface EditarCondutorModel {
  nome: string;
  cnh: string;
  cpf: string;
  telefone: string;
  categoria: CategoriaCnhEnum;
  validadeCnh: string; 
  eCliente: boolean;
}

export interface EditarCondutorResponseModel {
  nome: string;
  cnh: string;
  cpf: string;
  telefone: string;
  categoria: CategoriaCnhEnum;
  validadeCnh: string;
  eCliente: boolean;
}
