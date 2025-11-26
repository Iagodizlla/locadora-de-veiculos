export interface ListarGruposAutomoveisApiResponseModel {
  quantidadeRegistros: number;
  registros: ListarGruposAutomoveisModel[];
}

export interface ListarGruposAutomoveisModel {
  id: string;
  nome: string;
}

export interface DetalhesGrupoAutomovelModel {
  id: string;
  nome: string;
}

export interface CadastrarGrupoAutomovelModel {
  nome: string;
}

export interface CadastrarGrupoAutomovelResponseModel {
  id: string;
}

export interface EditarGrupoAutomovelModel {
  nome: string;
}

export interface EditarGrupoAutomovelResponseModel {
  nome: string;
}
