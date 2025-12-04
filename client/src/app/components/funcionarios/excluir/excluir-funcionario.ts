import { filter, map, Observer, shareReplay, switchMap, take } from 'rxjs';

import { AsyncPipe } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';

import { NotificacaoService } from '../../shared/notificacao/notificacao.service';
import { FuncionarioService } from '../funcionario.service';
import { DetalhesFuncionarioModel } from '../funcionario.models';

@Component({
  selector: 'app-excluir-funcionario',
  imports: [
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    RouterLink,
    AsyncPipe,
    FormsModule,
  ],
  templateUrl: './excluir-funcionario.html',
})
export class ExcluirFuncionario {
  protected readonly route = inject(ActivatedRoute);
  protected readonly router = inject(Router);
  protected readonly funcionarioService = inject(FuncionarioService);
  protected readonly notificacaoService = inject(NotificacaoService);

  protected readonly funcionario$ = this.route.data.pipe(
    filter((data) => data['funcionario']),
    map((data) => data['funcionario'] as DetalhesFuncionarioModel),
    shareReplay({ bufferSize: 1, refCount: true })
  );

  public excluir() {
    const exclusaoObserver: Observer<null> = {
      next: () => this.notificacaoService.sucesso(`O registro foi excluído com sucesso!`),
      error: (err) => this.notificacaoService.erro(err),
      complete: () => this.router.navigate(['/funcionarios']),
    };

    this.funcionario$
      .pipe(
        take(1),
        switchMap((funcionario) => this.funcionarioService.excluir(funcionario.id))
      )
      .subscribe(exclusaoObserver);
  }
}