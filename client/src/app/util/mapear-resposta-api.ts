import { throwError } from 'rxjs';

import { HttpErrorResponse } from '@angular/common/http';

export interface RespostaApiModel {
  sucesso: boolean;
  dados?: Object;
  erros?: string[];
}

export function mapearRespostaApi<T>(resposta: RespostaApiModel): T {
  if (!resposta.sucesso && resposta.erros) throw new Error(resposta.erros.join('. '));

  return resposta.dados as T;
}

export function mapearRespostaErroApi(respostaErro: HttpErrorResponse) {
  const obj = respostaErro.error as RespostaApiModel;

  return throwError(() => obj.erros?.join('. '));
}
