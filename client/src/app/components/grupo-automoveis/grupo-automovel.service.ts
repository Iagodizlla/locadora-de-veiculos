import { map, Observable } from 'rxjs';

import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';

import { environment } from '../../../environments/environment';
import { mapearRespostaApi, RespostaApiModel } from '../../util/mapear-resposta-api';
import {
  CadastrarGrupoAutomovelModel,
  CadastrarGrupoAutomovelResponseModel,
  DetalhesGrupoAutomovelModel,
  EditarGrupoAutomovelModel,
  EditarGrupoAutomovelResponseModel,
  ListarGruposAutomoveisApiResponseModel,
  ListarGruposAutomoveisModel
} from './grupo-automovel.models';

@Injectable()
export class GrupoAutomovelService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = environment.apiUrl + '/api/grupo-automoveis';

  public cadastrar(grupoAutomovelModel: CadastrarGrupoAutomovelModel): Observable<CadastrarGrupoAutomovelResponseModel> {
    return this.http
      .post<RespostaApiModel>(this.apiUrl, grupoAutomovelModel)
      .pipe(map(mapearRespostaApi<CadastrarGrupoAutomovelResponseModel>));
  }

  public editar(
    id: string,
    editarGrupoAutomovelModel: EditarGrupoAutomovelModel
  ): Observable<EditarGrupoAutomovelResponseModel> {
    const urlCompleto = `${this.apiUrl}/${id}`;

    return this.http
      .put<RespostaApiModel>(urlCompleto, editarGrupoAutomovelModel)
      .pipe(map(mapearRespostaApi<EditarGrupoAutomovelResponseModel>));
  }

  public excluir(id: string): Observable<null> {
    const urlCompleto = `${this.apiUrl}/${id}`;
    return this.http.delete<null>(urlCompleto);
  }

  public selecionarPorId(id: string): Observable<DetalhesGrupoAutomovelModel> {
    const urlCompleto = `${this.apiUrl}/${id}`;

    return this.http
      .get<RespostaApiModel>(urlCompleto)
      .pipe(map(mapearRespostaApi<DetalhesGrupoAutomovelModel>));
  }

  public selecionarTodos(): Observable<ListarGruposAutomoveisModel[]> {
    return this.http.get<RespostaApiModel>(this.apiUrl).pipe(
      map(mapearRespostaApi<ListarGruposAutomoveisApiResponseModel>),
      map((res) => res.registros)
    );
  }
}
