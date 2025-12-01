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
import { PlanoService } from '../plano.service';
import { DetalhesPlanoModel } from '../plano.models';

@Component({
  selector: 'app-excluir-plano',
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
  templateUrl: './excluir-plano.html',
})
export class ExcluirPlano {
  protected readonly route = inject(ActivatedRoute);
  protected readonly router = inject(Router);
  protected readonly planoService = inject(PlanoService);
  protected readonly notificacaoService = inject(NotificacaoService);

  protected readonly plano$ = this.route.data.pipe(
    filter((data) => data['plano']),
    map((data) => data['plano'] as DetalhesPlanoModel),
    shareReplay({ bufferSize: 1, refCount: true })
  );

  public excluir() {
    const exclusaoObserver: Observer<null> = {
      next: () => this.notificacaoService.sucesso(`O registro foi excluído com sucesso!`),
      error: (err) => this.notificacaoService.erro(err),
      complete: () => this.router.navigate(['/planos']),
    };

    this.plano$
      .pipe(
        take(1),
        switchMap((plano) => this.planoService.excluir(plano.id))
      )
      .subscribe(exclusaoObserver);
  }
}
