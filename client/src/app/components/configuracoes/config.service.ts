import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { environment } from '../../../environments/environment';
import { DetalhesConfigModel, EditarConfigModel, EditarConfigResponseModel } from './config.models';
import { RespostaApiModel, mapearRespostaApi } from '../../util/mapear-resposta-api';

@Injectable()
export class ConfigService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = environment.apiUrl + '/api/configuracoes/combustivel';

  selecionar(): Observable<DetalhesConfigModel> {
    return this.http
      .get<RespostaApiModel>(this.apiUrl)
      .pipe(map(mapearRespostaApi<DetalhesConfigModel>));
  }

  editar(model: EditarConfigModel): Observable<EditarConfigResponseModel> {
    return this.http
      .put<RespostaApiModel>(this.apiUrl, model)
      .pipe(map(mapearRespostaApi<EditarConfigResponseModel>));
  }
}
