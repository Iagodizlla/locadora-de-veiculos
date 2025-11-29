import { map, Observable } from 'rxjs';

import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';

import { environment } from '../../../environments/environment';
import { mapearRespostaApi, RespostaApiModel } from '../../util/mapear-resposta-api';

import {
  ListarClientesApiResponseModel,
  ListarClientesModel,
  CadastrarClienteModel,
  CadastrarClienteResponseModel,
  EditarClienteModel,
  EditarClienteResponseModel,
  DetalhesClienteModel
} from './cliente.models';

@Injectable()
export class ClienteService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = environment.apiUrl + '/clientes';

  public cadastrar(clienteModel: CadastrarClienteModel): Observable<CadastrarClienteResponseModel> {
    return this.http
      .post<RespostaApiModel>(this.apiUrl, clienteModel)
      .pipe(map(mapearRespostaApi<CadastrarClienteResponseModel>));
  }

  public editar(id: string, editarClienteModel: EditarClienteModel): Observable<EditarClienteResponseModel> {
    const urlCompleto = `${this.apiUrl}/${id}`;

    return this.http
      .put<RespostaApiModel>(urlCompleto, editarClienteModel)
      .pipe(map(mapearRespostaApi<EditarClienteResponseModel>));
  }

  public excluir(id: string): Observable<null> {
    const urlCompleto = `${this.apiUrl}/${id}`;

    return this.http.delete<null>(urlCompleto);
  }

  public selecionarPorId(id: string): Observable<DetalhesClienteModel> {
    const urlCompleto = `${this.apiUrl}/${id}`;

    return this.http
      .get<RespostaApiModel>(urlCompleto)
      .pipe(map(mapearRespostaApi<DetalhesClienteModel>));
  }

  public selecionarTodos(): Observable<ListarClientesModel[]> {
    return this.http.get<RespostaApiModel>(this.apiUrl).pipe(
      map(mapearRespostaApi<ListarClientesApiResponseModel>),
      map(res => res.registros)
    );
  }
}
