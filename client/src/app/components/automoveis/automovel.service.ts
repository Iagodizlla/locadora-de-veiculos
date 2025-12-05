import { map, Observable } from 'rxjs';

import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';

import { environment } from '../../../environments/environment';
import { mapearRespostaApi, RespostaApiModel } from '../../util/mapear-resposta-api';
import {
  CadastrarAutomovelModel,
  CadastrarAutomovelResponseModel,
  DetalhesAutomovelModel,
  EditarAutomovelModel,
  EditarAutomovelResponseModel,
  ListarAutomoveisApiResponseModel,
  ListarAutomoveisModel
} from './automovel.models';

@Injectable()
export class AutomovelService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = environment.apiUrl + '/api/automoveis';

  public cadastrar(automovelModel: CadastrarAutomovelModel): Observable<CadastrarAutomovelResponseModel> {
    return this.http
      .post<RespostaApiModel>(this.apiUrl, automovelModel)
      .pipe(map(mapearRespostaApi<CadastrarAutomovelResponseModel>));
  }

  public editar(
    id: string,
    editarAutomovelModel: EditarAutomovelModel
  ): Observable<EditarAutomovelResponseModel> {
    const urlCompleto = `${this.apiUrl}/${id}`;

    return this.http
      .put<RespostaApiModel>(urlCompleto, editarAutomovelModel)
      .pipe(map(mapearRespostaApi<EditarAutomovelResponseModel>));
  }

  public excluir(id: string): Observable<null> {
    const urlCompleto = `${this.apiUrl}/${id}`;
    return this.http.delete<null>(urlCompleto);
  }

  public selecionarPorId(id: string): Observable<DetalhesAutomovelModel> {
    const urlCompleto = `${this.apiUrl}/${id}`;

    return this.http
      .get<RespostaApiModel>(urlCompleto)
      .pipe(map(mapearRespostaApi<DetalhesAutomovelModel>));
  }

  public selecionarTodos(): Observable<ListarAutomoveisModel[]> {
    return this.http.get<RespostaApiModel>(this.apiUrl).pipe(
      map(mapearRespostaApi<ListarAutomoveisApiResponseModel>),
      map((res) => res.registros)
    );
  }

  public selecionarPorGrupo(grupoId: string): Observable<ListarAutomoveisModel[]> {
    const url = `${this.apiUrl}/grupo/${grupoId}`;

    return this.http
      .get<RespostaApiModel>(url)
      .pipe(
        map(mapearRespostaApi<ListarAutomoveisApiResponseModel>),
        map(r => r.registros)
      );
  }
}
