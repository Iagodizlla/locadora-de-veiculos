import { filter, map } from 'rxjs';

import { AsyncPipe, CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { ListarPlanosModel } from '../plano.models';

@Component({
  selector: 'app-listar-planos',
  imports: [CommonModule, MatButtonModule, MatIconModule, MatCardModule, RouterLink, AsyncPipe],
  templateUrl: './listar-planos.html',
})
export class ListarPlanos {
  protected readonly route = inject(ActivatedRoute);

  protected readonly planos$ = this.route.data.pipe(
    filter((data) => data['planos']),
    map((data) => data['planos'] as ListarPlanosModel[])
  );
}
