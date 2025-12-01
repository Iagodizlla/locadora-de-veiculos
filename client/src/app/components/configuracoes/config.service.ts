import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';

import { environment } from '../../../environments/environment';
import {
  ConfigModel,
  EditarConfigModel,
  EditarConfigResponseModel,
  SelecionarConfigResponseModel
} from './config.models';
import { RespostaApiModel, mapearRespostaApi } from '../../util/mapear-resposta-api';

@Injectable()
export class ConfiguracoesService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = environment.apiUrl + '/configuracoes/combustivel';

  selecionar(): Observable<SelecionarConfigResponseModel> {
    return this.http
      .get<RespostaApiModel>(this.apiUrl)
      .pipe(map(mapearRespostaApi<SelecionarConfigResponseModel>));
  }

  editar(model: EditarConfigModel): Observable<EditarConfigResponseModel> {
    return this.http
      .put<RespostaApiModel>(this.apiUrl, model)
      .pipe(map(mapearRespostaApi<EditarConfigResponseModel>));
  }
}
