import { map, Observable } from 'rxjs';

import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';

import { environment } from '../../../environments/environment';
import { mapearRespostaApi, RespostaApiModel } from '../../util/mapear-resposta-api';
import {
  CadastrarFuncionarioModel,
  CadastrarFuncionarioResponseModel,
  DetalhesFuncionarioModel,
  EditarFuncionarioModel,
  EditarFuncionarioResponseModel,
  ListarFuncionariosApiResponseModel,
  ListarFuncionariosModel,
  AutoEditarFuncionarioModel,
  AutoEditarFuncionarioResponseModel
} from './funcionario.models';

@Injectable()
export class FuncionarioService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = environment.apiUrl + '/api/funcionarios';

  public cadastrar(funcionarioModel: CadastrarFuncionarioModel): Observable<CadastrarFuncionarioResponseModel> {
    return this.http
      .post<RespostaApiModel>(this.apiUrl, funcionarioModel)
      .pipe(map(mapearRespostaApi<CadastrarFuncionarioResponseModel>));
  }

  public autoEditar(autoEditarModel: AutoEditarFuncionarioModel): Observable<AutoEditarFuncionarioResponseModel> {
    const urlCompleto = `${this.apiUrl}/auto-editar`;

    return this.http
      .put<RespostaApiModel>(urlCompleto, autoEditarModel)
      .pipe(map(mapearRespostaApi<AutoEditarFuncionarioResponseModel>));
  }

  public editar(
    id: string,
    editarFuncionarioModel: EditarFuncionarioModel
  ): Observable<EditarFuncionarioResponseModel> {
    const urlCompleto = `${this.apiUrl}/${id}`;

    return this.http
      .put<RespostaApiModel>(urlCompleto, editarFuncionarioModel)
      .pipe(map(mapearRespostaApi<EditarFuncionarioResponseModel>));
  }

  public excluir(id: string): Observable<null> {
    const urlCompleto = `${this.apiUrl}/${id}`;
    return this.http.delete<null>(urlCompleto);
  }

  public selecionarPorId(id: string): Observable<DetalhesFuncionarioModel> {
    const urlCompleto = `${this.apiUrl}/${id}`;

    return this.http
      .get<RespostaApiModel>(urlCompleto)
      .pipe(map(mapearRespostaApi<DetalhesFuncionarioModel>));
  }

  public selecionarTodos(): Observable<ListarFuncionariosModel[]> {
    return this.http.get<RespostaApiModel>(this.apiUrl).pipe(
      map(mapearRespostaApi<ListarFuncionariosApiResponseModel>),
      map((res) => res.registros)
    );
  }
}
