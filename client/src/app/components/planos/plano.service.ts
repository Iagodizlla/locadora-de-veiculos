import { map, Observable } from 'rxjs';

import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';

import { environment } from '../../../environments/environment';
import { mapearRespostaApi, RespostaApiModel } from '../../util/mapear-resposta-api';
import {
  CadastrarPlanoModel,
  CadastrarPlanoResponseModel,
  DetalhesPlanoModel,
  EditarPlanoModel,
  EditarPlanoResponseModel,
  ListarPlanosApiResponseModel,
  ListarPlanosModel
} from './plano.models';

@Injectable()
export class PlanoService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = environment.apiUrl + '/api/planos';

  public cadastrar(planoModel: CadastrarPlanoModel): Observable<CadastrarPlanoResponseModel> {
    return this.http
      .post<RespostaApiModel>(this.apiUrl, planoModel)
      .pipe(map(mapearRespostaApi<CadastrarPlanoResponseModel>));
  }

  public editar(
    id: string,
    editarPlanoModel: EditarPlanoModel
  ): Observable<EditarPlanoResponseModel> {
    const urlCompleto = `${this.apiUrl}/${id}`;

    return this.http
      .put<RespostaApiModel>(urlCompleto, editarPlanoModel)
      .pipe(map(mapearRespostaApi<EditarPlanoResponseModel>));
  }

  public excluir(id: string): Observable<null> {
    const urlCompleto = `${this.apiUrl}/${id}`;
    return this.http.delete<null>(urlCompleto);
  }

  public selecionarPorId(id: string): Observable<DetalhesPlanoModel> {
    const urlCompleto = `${this.apiUrl}/${id}`;

    return this.http
      .get<RespostaApiModel>(urlCompleto)
      .pipe(map(mapearRespostaApi<DetalhesPlanoModel>));
  }

  public selecionarTodos(): Observable<ListarPlanosModel[]> {
    return this.http.get<RespostaApiModel>(this.apiUrl).pipe(
      map(mapearRespostaApi<ListarPlanosApiResponseModel>),
      map((res) => res.registros)
    );
  }
}
