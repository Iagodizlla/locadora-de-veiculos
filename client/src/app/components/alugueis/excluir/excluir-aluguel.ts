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
import { AluguelService } from '../aluguel.service';
import { DetalhesAluguelModel } from '../aluguel.models';

@Component({
  selector: 'app-excluir-aluguel',
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
  templateUrl: './excluir-aluguel.html',
})
export class ExcluirAluguel {
  protected readonly route = inject(ActivatedRoute);
  protected readonly router = inject(Router);
  protected readonly aluguelService = inject(AluguelService);
  protected readonly notificacaoService = inject(NotificacaoService);

  protected readonly aluguel$ = this.route.data.pipe(
    filter((data) => data['aluguel']),
    map((data) => data['aluguel'] as DetalhesAluguelModel),
    shareReplay({ bufferSize: 1, refCount: true })
  );

  public excluir() {
    const exclusaoObserver: Observer<null> = {
      next: () => this.notificacaoService.sucesso(`O registro foi excluído com sucesso!`),
      error: (err) => this.notificacaoService.erro(err),
      complete: () => this.router.navigate(['/alugueis']),
    };

    this.aluguel$
      .pipe(
        take(1),
        switchMap((aluguel) => this.aluguelService.excluir(aluguel.id))
      )
      .subscribe(exclusaoObserver);
  }
}
