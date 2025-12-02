import { map, Observable } from 'rxjs';

import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';

import { environment } from '../../../environments/environment';
import { mapearRespostaApi, RespostaApiModel } from '../../util/mapear-resposta-api';
import {
  CadastrarTaxaModel,
  CadastrarTaxaResponseModel,
  DetalhesTaxaModel,
  EditarTaxaModel,
  EditarTaxaResponseModel,
  ListarTaxasApiResponseModel,
  ListarTaxasModel
} from './taxa.models';

@Injectable()
export class TaxaService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = environment.apiUrl + '/taxas';

  public cadastrar(taxaModel: CadastrarTaxaModel): Observable<CadastrarTaxaResponseModel> {
    return this.http
      .post<RespostaApiModel>(this.apiUrl, taxaModel)
      .pipe(map(mapearRespostaApi<CadastrarTaxaResponseModel>));
  }

  public editar(
    id: string,
    editarTaxaModel: EditarTaxaModel
  ): Observable<EditarTaxaResponseModel> {
    const urlCompleto = `${this.apiUrl}/${id}`;

    return this.http
      .put<RespostaApiModel>(urlCompleto, editarTaxaModel)
      .pipe(map(mapearRespostaApi<EditarTaxaResponseModel>));
  }

  public excluir(id: string): Observable<null> {
    const urlCompleto = `${this.apiUrl}/${id}`;
    return this.http.delete<null>(urlCompleto);
  }

  public selecionarPorId(id: string): Observable<DetalhesTaxaModel> {
    const urlCompleto = `${this.apiUrl}/${id}`;

    return this.http
      .get<RespostaApiModel>(urlCompleto)
      .pipe(map(mapearRespostaApi<DetalhesTaxaModel>));
  }

  public selecionarTodos(): Observable<ListarTaxasModel[]> {
    return this.http.get<RespostaApiModel>(this.apiUrl).pipe(
      map(mapearRespostaApi<ListarTaxasApiResponseModel>),
      map((res) => res.registros)
    );
  }
}
