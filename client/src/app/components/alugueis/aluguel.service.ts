import { map, Observable } from 'rxjs';

import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';

import { environment } from '../../../environments/environment';
import { mapearRespostaApi, RespostaApiModel } from '../../util/mapear-resposta-api';
import {
  CadastrarAluguelModel,
  CadastrarAluguelResponseModel,
  DetalhesAluguelModel,
  EditarAluguelModel,
  EditarAluguelResponseModel,
  FinalizarAluguelModel,
  FinalizarAluguelResponseModel,
  ListarAlugueisApiResponseModel,
  ListarAlugueisModel
} from './aluguel.models';

@Injectable()
export class AluguelService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = environment.apiUrl + '/api/alugueis';

  public cadastrar(aluguelModel: CadastrarAluguelModel): Observable<CadastrarAluguelResponseModel> {
    return this.http
      .post<RespostaApiModel>(this.apiUrl, aluguelModel)
      .pipe(map(mapearRespostaApi<CadastrarAluguelResponseModel>));
  }

  public editar(
    id: string,
    editarAluguelModel: EditarAluguelModel
  ): Observable<EditarAluguelResponseModel> {
    const urlCompleto = `${this.apiUrl}/${id}`;

    return this.http
      .put<RespostaApiModel>(urlCompleto, editarAluguelModel)
      .pipe(map(mapearRespostaApi<EditarAluguelResponseModel>));
  }

  public finalizar(
    id: string,
    finalizarAluguelModel: FinalizarAluguelModel
  ): Observable<FinalizarAluguelResponseModel> {
    const urlCompleto = `${this.apiUrl}/finalizar/${id}`;

    return this.http
      .put<RespostaApiModel>(urlCompleto, finalizarAluguelModel)
      .pipe(map(mapearRespostaApi<FinalizarAluguelResponseModel>));
  }

  public excluir(id: string): Observable<null> {
    const urlCompleto = `${this.apiUrl}/${id}`;
    return this.http.delete<null>(urlCompleto);
  }

  public selecionarPorId(id: string): Observable<DetalhesAluguelModel> {
    const urlCompleto = `${this.apiUrl}/${id}`;

    return this.http
      .get<RespostaApiModel>(urlCompleto)
      .pipe(map(mapearRespostaApi<DetalhesAluguelModel>));
  }

  public selecionarTodos(): Observable<ListarAlugueisModel[]> {
    return this.http.get<RespostaApiModel>(this.apiUrl).pipe(
      map(mapearRespostaApi<ListarAlugueisApiResponseModel>),
      map((res) => res.registros)
    );
  }
}
