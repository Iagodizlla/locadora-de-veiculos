export interface ListarAlugueisApiResponseModel {
  quantidadeRegistros: number;
  registros: ListarAlugueisModel[];
}

export interface ListarAlugueisModel {
  id: string;
  status: boolean;
  valorTotal: number;
  quilometragemInicial: number;
  quilometragemFinal: number | null;
  nivelCombustivelNaSaida: number;
  nivelCombustivelNaDevolucao: number | null;
  seguroCliente: boolean;
  seguroTerceiro: boolean;
  valorSeguroPorDia: number | null;
  dataSaida: Date;
  dataRetornoPrevista: Date;
  dataDevolucao: Date | null;
  automovel: AutomovelAluguelModel;
  condutor: CondutorAluguelModel;
  cliente: ClienteAluguelModel;
  plano: PlanoAluguelModel;
  taxas: TaxaAluguelModel[];
}

export interface DetalhesAluguelModel {
  id: string;
  status: boolean;
  valorTotal: number;
  quilometragemInicial: number;
  quilometragemFinal: number | null;
  nivelCombustivelNaSaida: number;
  nivelCombustivelNaDevolucao: number | null;
  seguroCliente: boolean;
  seguroTerceiro: boolean;
  valorSeguroPorDia: number | null;
  dataSaida: Date;
  dataRetornoPrevista: Date;
  dataDevolucao: Date | null;
  automovel: AutomovelAluguelModel;
  condutor: CondutorAluguelModel;
  cliente: ClienteAluguelModel;
  plano: PlanoAluguelModel;
  taxas: TaxaAluguelModel[];
}

export interface CadastrarAluguelModel {
  quilometragemInicial: number;
  nivelCombustivelNaSaida: number;
  seguroCliente: boolean;
  seguroTerceiro: boolean;
  valorSeguroPorDia: number | null;
  dataSaida: Date;
  dataRetornoPrevista: Date;
  automovelId: AutomovelAluguelModel;
  condutorId: CondutorAluguelModel;
  clienteId: ClienteAluguelModel;
  planoId: PlanoAluguelModel;
  taxas: TaxaAluguelModel[];
}

export interface CadastrarAluguelResponseModel {
  id: string;
}

export interface EditarAluguelModel {
  status: boolean;
  valorTotal: number;
  quilometragemInicial: number;
  quilometragemFinal: number | null;
  nivelCombustivelNaSaida: number;
  nivelCombustivelNaDevolucao: number | null;
  seguroCliente: boolean;
  seguroTerceiro: boolean;
  valorSeguroPorDia: number | null;
  dataSaida: Date;
  dataRetornoPrevista: Date;
  dataDevolucao: Date | null;
  automovel: AutomovelAluguelModel;
  condutor: CondutorAluguelModel;
  cliente: ClienteAluguelModel;
  plano: PlanoAluguelModel;
  taxas: TaxaAluguelModel[];
}

export interface EditarAluguelResponseModel {
  status: boolean;
  valorTotal: number;
  quilometragemInicial: number;
  quilometragemFinal: number | null;
  nivelCombustivelNaSaida: number;
  nivelCombustivelNaDevolucao: number | null;
  seguroCliente: boolean;
  seguroTerceiro: boolean;
  valorSeguroPorDia: number | null;
  dataSaida: Date;
  dataRetornoPrevista: Date;
  dataDevolucao: Date | null;
  automovel: AutomovelAluguelModel;
  condutor: CondutorAluguelModel;
  cliente: ClienteAluguelModel;
  plano: PlanoAluguelModel;
  taxas: TaxaAluguelModel[];
}

export interface FinalizarAluguelModel {
  id: string;
  dataDevolucao: Date;
  quilometragemFinal: number;
  nivelCombustivelNaDevolucao: number;
}

export interface FinalizarAluguelResponseModel {
  id: string;
  status: boolean;
  valorTotal: number;
}

export interface AutomovelAluguelModel {
  id: string;
  placa: string;
  marca: string;
  modelo: string;
  cor: string;
  ano: number;
  capacidadeTanque: number;
  grupoAutomovel: GrupoAutomovelAutomovelModel;
  foto: string;
  combustivel: CombustivelEnum;
}

export enum CombustivelEnum {
  Gasolina = 'Gasolina',
  Gas = 'Gas',
  Diesel = 'Diesel',
  Alcool = 'Alcool'
}

export interface GrupoAutomovelAutomovelModel {
  id: string;
  nome: string;
}

export interface CondutorAluguelModel {
  id: string;
  nome: string;
  cnh: string;
  cpf: string;
  telefone: string;
  categoria: CategoriaCnhEnum;
  validadeCnh: Date;
  eCliente: boolean;
}

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

export interface ClienteAluguelModel {
  id: string;
  nome: string;
  cnh: string;
  documento: string;
  telefone: string;
  tipoCliente: ClienteTipoEnum;
  endereco: EnderecoClienteModel;
}

export enum ClienteTipoEnum {
 PessoaFisica = 'PessoaFisica',
 PessoaJuridica = 'PessoaJuridica'
}

export interface EnderecoClienteModel{
  id?: string;
  logradouro: string;
  numero: number;
  bairro: string;
  cidade: string;
  estado: string;
}

export interface TaxaAluguelModel {
  id: string;
  nome: string;
  preco: number;
  servico: ServicoEnum;
}

export enum ServicoEnum {
  PrecoFixo = 'PrecoFixo',
  CobrancaDiaria = 'CobrancaDiaria'
}

export interface PlanoAluguelModel {
  id: string;
  grupoAutomovel: GrupoAutomovelAutomovelModel;
  precoDiario: number;
  precoDiarioControlado: number;
  precoPorKm: number;
  kmLivres: number;
  precoPorKmExplorado: number;
  precoLivre: number;
}
