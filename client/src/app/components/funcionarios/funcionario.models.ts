export interface ListarFuncionariosApiResponseModel {
  quantidadeRegistros: number;
  registros: ListarFuncionariosModel[];
}

export interface ListarFuncionariosModel {
  id: string;
  nome: string;
  salario: number;
  admissao: string;
}

export interface DetalhesFuncionarioModel {
  id: string;
  nome: string;
  salario: number;
  admissao: string;
}

export interface CadastrarFuncionarioModel {
  nome: string;
  salario: number;
  admissao: string;
  userName: string;
  email: string;
  password: string;
}

export interface CadastrarFuncionarioResponseModel {
  id: string;
}

export interface EditarFuncionarioModel {
  nome: string;
  salario: number;
  admissao: string;
}

export interface EditarFuncionarioResponseModel {
  nome: string;
  salario: number;
  admissao: string;
}

export interface AutoEditarFuncionarioModel {
  nome: string;
}

export interface AutoEditarFuncionarioResponseModel {
  nome: string;
}
