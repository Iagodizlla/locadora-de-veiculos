import { map, Observable } from 'rxjs';

import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';

import { environment } from '../../../environments/environment';
import { mapearRespostaApi, RespostaApiModel } from '../../util/mapear-resposta-api';

import {
  ListarCondutoresApiResponseModel,
  ListarCondutoresModel,
  CadastrarCondutorModel,
  CadastrarCondutorResponseModel,
  EditarCondutorModel,
  EditarCondutorResponseModel,
  DetalhesCondutorModel
} from './condutor.models';

@Injectable()
export class CondutorService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = environment.apiUrl + '/condutores';

  public cadastrar(condutorModel: CadastrarCondutorModel): Observable<CadastrarCondutorResponseModel> {
    return this.http
      .post<RespostaApiModel>(this.apiUrl, condutorModel)
      .pipe(map(mapearRespostaApi<CadastrarCondutorResponseModel>));
  }

  public editar(id: string, editarCondutorModel: EditarCondutorModel): Observable<EditarCondutorResponseModel> {
    const urlCompleto = `${this.apiUrl}/${id}`;

    return this.http
      .put<RespostaApiModel>(urlCompleto, editarCondutorModel)
      .pipe(map(mapearRespostaApi<EditarCondutorResponseModel>));
  }

  public excluir(id: string): Observable<null> {
    const urlCompleto = `${this.apiUrl}/${id}`;

    return this.http.delete<null>(urlCompleto);
  }

  public selecionarPorId(id: string): Observable<DetalhesCondutorModel> {
    const urlCompleto = `${this.apiUrl}/${id}`;

    return this.http
      .get<RespostaApiModel>(urlCompleto)
      .pipe(map(mapearRespostaApi<DetalhesCondutorModel>));
  }

  public selecionarTodos(): Observable<ListarCondutoresModel[]> {
    return this.http.get<RespostaApiModel>(this.apiUrl).pipe(
      map(mapearRespostaApi<ListarCondutoresApiResponseModel>),
      map(res => res.registros)
    );
  }
}
