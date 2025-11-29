export enum TipoClienteEnum {
  PessoaFisica = 'Pessoa Fisica',
  PessoaJuridica = 'Pessoa Juridica'
}

export interface EnderecoModel {
  id?: string;
  logradouro: string;
  numero: number;
  bairro: string;
  cidade: string;
  estado: string;
}

export interface ClienteModel {
  id?: string;
  nome: string;
  telefone: string;
  clienteTipo: TipoClienteEnum;
  documento: string;       // CPF ou CNPJ
  cnh?: string | null;     // Apenas PF
  endereco: EnderecoModel; // Objeto completo
}

export interface CadastrarClienteModel {
  nome: string;
  telefone: string;
  clienteTipo: TipoClienteEnum;
  documento: string;
  cnh?: string | null;
  endereco: EnderecoModel;
}

export interface CadastrarClienteResponseModel {
  id: string;
  nome: string;
}

export interface EditarClienteModel {
  nome: string;
  telefone: string;
  clienteTipo: TipoClienteEnum;
  documento: string;
  cnh?: string | null;
  endereco: EnderecoModel;
}

export interface EditarClienteResponseModel {
  id: string;
  nome: string;
}

export interface DetalhesClienteModel {
  id: string;
  nome: string;
  telefone: string;
  clienteTipo: TipoClienteEnum;
  documento: string;
  cnh?: string | null;
  endereco: EnderecoModel;
}

export interface ListarClientesApiResponseModel {
  registros: ListarClientesModel[];
}

export interface ListarClientesModel {
  id: string;
  nome: string;
  telefone: string;
  clienteTipo: TipoClienteEnum;
  documento: string;
}
