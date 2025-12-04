import { filter, map } from 'rxjs';

import { AsyncPipe } from '@angular/common';
import { Component, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { ListarFuncionariosModel } from '../funcionario.models';

@Component({
  selector: 'app-listar-funcionarios',
  imports: [MatButtonModule, MatIconModule, MatCardModule, RouterLink, AsyncPipe],
  templateUrl: './listar-funcionarios.html',
})
export class ListarFuncionarios {
  protected readonly route = inject(ActivatedRoute);

  protected readonly funcionarios$ = this.route.data.pipe(
    filter((data) => data['funcionarios']),
    map((data) => data['funcionarios'] as ListarFuncionariosModel[])
  );
}
