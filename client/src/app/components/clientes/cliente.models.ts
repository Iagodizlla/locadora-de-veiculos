export enum ClienteTipoEnum {
 PessoaFisica = 'PessoaFisica',
 PessoaJuridica = 'PessoaJuridica'
}
export interface ListarClientesApiResponseModel {
  quantidadeRegistros: number;
  registros: ListarClientesModel[];
}

export interface ListarClientesModel {
  id: string;
  nome: string;
  cnh: string;
  documento: string;
  telefone: string;
  tipoCliente: ClienteTipoEnum;
  endereco: EnderecoClienteModel;
}

export interface DetalhesClienteModel {
  id: string;
  nome: string;
  cnh: string;
  documento: string;
  telefone: string;
  tipoCliente: ClienteTipoEnum;
  endereco: EnderecoClienteModel;
}

export interface CadastrarClienteModel {
  nome: string;
  cnh: string;
  documento: string;
  telefone: string;
  tipoCliente: ClienteTipoEnum;
  endereco: EnderecoClienteModel;
}

export interface CadastrarClienteResponseModel {
  id: string;
}

export interface EditarClienteModel {
  nome: string;
  cnh: string;
  documento: string;
  telefone: string;
  tipoCliente: ClienteTipoEnum;
  endereco: EnderecoClienteModel;
}

export interface EditarClienteResponseModel {
  nome: string;
  cnh: string;
  documento: string;
  telefone: string;
  tipoCliente: ClienteTipoEnum;
  endereco: EnderecoClienteModel;
}
export interface EnderecoClienteModel{
  id?: string;
  logradouro: string;
  numero: number;
  bairro: string;
  cidade: string;
  estado: string;
}
