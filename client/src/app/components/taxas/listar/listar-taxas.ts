import { filter, map } from 'rxjs';

import { AsyncPipe } from '@angular/common';
import { Component, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { ActivatedRoute, RouterLink } from '@angular/router';

import { ListarTaxasModel } from '../taxa.models';

@Component({
  selector: 'app-listar-taxas',
  imports: [MatButtonModule, MatIconModule, MatCardModule, RouterLink, AsyncPipe],
  templateUrl: './listar-taxas.html',
})
export class ListarTaxas {
  protected readonly route = inject(ActivatedRoute);

  protected readonly taxas$ = this.route.data.pipe(
    filter((data) => data['taxas']),
    map((data) => data['taxas'] as ListarTaxasModel[])
  );
}
