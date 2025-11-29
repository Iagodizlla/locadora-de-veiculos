import { filter, map } from 'rxjs';

import { AsyncPipe, DatePipe } from '@angular/common';
import { Component, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { ActivatedRoute, RouterLink } from '@angular/router';

import { ListarCondutoresModel } from '../condutor.models';

@Component({
  selector: 'app-listar-condutores',
  imports: [MatButtonModule, MatIconModule, MatCardModule, RouterLink, AsyncPipe, DatePipe],
  templateUrl: './listar-condutores.html',
})
export class ListarCondutores {
  protected readonly route = inject(ActivatedRoute);

  protected readonly condutores$ = this.route.data.pipe(
    filter((data) => data['condutores']),
    map((data) => data['condutores'] as ListarCondutoresModel[])
  );
}
