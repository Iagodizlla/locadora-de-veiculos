export interface ListarAutomoveisApiResponseModel {
  quantidadeRegistros: number;
  registros: ListarAutomoveisModel[];
}

export interface ListarAutomoveisModel {
  id: string;
  placa: string;
  marca: string;
  modelo: string;
  cor: string;
  ano: number;
  capacidadeTanque: number;
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

export interface DetalhesAutomovelModel {
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

export interface CadastrarAutomovelModel {
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

export interface CadastrarAutomovelResponseModel {
  id: string;
}

export interface EditarAutomovelModel {
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

export interface EditarAutomovelResponseModel {
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
