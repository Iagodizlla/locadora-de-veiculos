export interface ListarPlanosApiResponseModel {
  quantidadeRegistros: number;
  registros: ListarPlanosModel[];
}

export interface ListarPlanosModel {
  id: string;
  grupoAutomovel: GrupoAutomovelPlanoModel;
  precoDiario: number;
  precoDiarioControlado: number;
  precoPorKm: number;
  kmLivres: number;
  precoPorKmExplorado: number;
  precoLivre: number;
}

export interface GrupoAutomovelPlanoModel {
  id: string;
  nome: string;
}

export interface DetalhesPlanoModel {
  id: string;
  grupoAutomovel: GrupoAutomovelPlanoModel;
  precoDiario: number;
  precoDiarioControlado: number;
  precoPorKm: number;
  kmLivres: number;
  precoPorKmExplorado: number;
  precoLivre: number;
}

export interface CadastrarPlanoModel {
  grupoAutomovel: GrupoAutomovelPlanoModel;
  precoDiario: number;
  precoDiarioControlado: number;
  precoPorKm: number;
  kmLivres: number;
  precoPorKmExplorado: number;
  precoLivre: number;
}

export interface CadastrarPlanoResponseModel {
  id: string;
}

export interface EditarPlanoModel {
  grupoAutomovel: GrupoAutomovelPlanoModel;
  precoDiario: number;
  precoDiarioControlado: number;
  precoPorKm: number;
  kmLivres: number;
  precoPorKmExplorado: number;
  precoLivre: number;
}

export interface EditarPlanoResponseModel {
  id: string;
  grupoAutomovel: GrupoAutomovelPlanoModel;
  precoDiario: number;
  precoDiarioControlado: number;
  precoPorKm: number;
  kmLivres: number;
  precoPorKmExplorado: number;
  precoLivre: number;
}
