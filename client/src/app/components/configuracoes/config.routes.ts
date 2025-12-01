import { Routes } from '@angular/router';
import { inject } from '@angular/core';
import { ConfiguracoesService } from './config.service';

export const selecionarConfigResolver = () => {
  return inject(ConfiguracoesService).selecionar();
};

export const configuracoesRoutes: Routes = [
  //{
  //  path: '',
  //  component: EditarConfiguracoesComponent,
  //  resolve: { configuracoes: selecionarConfigResolver },
  //  providers: [ConfiguracoesService],
  //},
];
