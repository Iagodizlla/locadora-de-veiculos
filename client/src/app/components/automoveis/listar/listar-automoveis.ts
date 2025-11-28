import { filter, map } from 'rxjs';

import { AsyncPipe, CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { ActivatedRoute, RouterLink } from '@angular/router';

import { ListarAutomoveisModel } from '../automovel.models';

@Component({
  selector: 'app-listar-automoveis',
  imports: [CommonModule, MatButtonModule, MatIconModule, MatCardModule, RouterLink, AsyncPipe],
  templateUrl: './listar-automoveis.html',
})
export class ListarAutomoveis {
  protected readonly route = inject(ActivatedRoute);

  protected readonly automoveis$ = this.route.data.pipe(
    filter((data) => data['automoveis']),
    map((data) => data['automoveis'] as ListarAutomoveisModel[])
  );
}
