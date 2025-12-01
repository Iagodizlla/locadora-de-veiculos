import { Component, inject } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { AsyncPipe } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatSelectModule } from '@angular/material/select';
import { BehaviorSubject, switchMap, startWith, map } from 'rxjs';

import { AutomovelService } from '../automovel.service';
import { ListarAutomoveisModel } from '../automovel.models';

@Component({
  selector: 'app-listar-automoveis',
  templateUrl: './listar-automoveis.html',
  standalone: true,
  imports: [
    MatButtonModule,
    MatCardModule,
    MatIconModule,
    MatSelectModule,
    AsyncPipe,
    RouterLink
  ],
})
export class ListarAutomoveis {
  private readonly route = inject(ActivatedRoute);
  private readonly service = inject(AutomovelService);

  // grupos vindos do resolver
  protected readonly grupos$ = this.route.data.pipe(
    map(data => data['grupoAutomoveis'])
  );

  // filtro selecionado
  private filtroGrupoId$ = new BehaviorSubject<string | null>(null);

  // carrega automóveis com base no filtro
  protected automoveis$ = this.filtroGrupoId$.pipe(
    startWith(null),
    switchMap(grupoId => {
      if (!grupoId) {
        return this.service.selecionarTodos();
      }
      return this.service.selecionarPorGrupo(grupoId);
    })
  );

  filtrarPorGrupo(grupoId: string) {
    this.filtroGrupoId$.next(grupoId);
  }
}
