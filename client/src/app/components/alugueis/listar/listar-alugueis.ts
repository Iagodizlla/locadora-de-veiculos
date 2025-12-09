import { filter, map } from 'rxjs';

import { AsyncPipe, CommonModule, DatePipe } from '@angular/common';
import { Component, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { ListarAlugueisModel } from '../aluguel.models';


@Component({
  selector: 'app-listar-alugueis',
  imports: [CommonModule, MatButtonModule, MatIconModule, MatCardModule, RouterLink, AsyncPipe, DatePipe],
  templateUrl: './listar-alugueis.html',
})
export class ListarAlugueis {
  protected readonly route = inject(ActivatedRoute);

  protected readonly alugueis$ = this.route.data.pipe(
    filter((data) => data['alugueis']),
    map((data) => data['alugueis'] as ListarAlugueisModel[])
  );
}
