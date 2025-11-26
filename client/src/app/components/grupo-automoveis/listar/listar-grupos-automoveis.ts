import { filter, map } from 'rxjs';

import { AsyncPipe } from '@angular/common';
import { Component, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { ActivatedRoute, RouterLink } from '@angular/router';

import { ListarGruposAutomoveisModel } from '../grupo-automovel.models';

@Component({
  selector: 'app-listar-grupos-automoveis',
  imports: [MatButtonModule, MatIconModule, MatCardModule, RouterLink, AsyncPipe],
  templateUrl: './listar-grupos-automoveis.html',
})
export class ListarGruposAutomoveis {
  protected readonly route = inject(ActivatedRoute);

  protected readonly gruposAutomoveis$ = this.route.data.pipe(
    filter((data) => data['gruposAutomoveis']),
    map((data) => data['gruposAutomoveis'] as ListarGruposAutomoveisModel[])
  );
}