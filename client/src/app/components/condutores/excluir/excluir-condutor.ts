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
import { CondutorService } from '../condutor.service';
import { DetalhesCondutorModel } from '../condutor.models';

@Component({
  selector: 'app-excluir-condutor',
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
  templateUrl: './excluir-condutor.html',
})
export class ExcluirCondutor {
  protected readonly route = inject(ActivatedRoute);
  protected readonly router = inject(Router);
  protected readonly condutorService = inject(CondutorService);
  protected readonly notificacaoService = inject(NotificacaoService);

  protected readonly condutor$ = this.route.data.pipe(
    filter((data) => data['condutor']),
    map((data) => data['condutor'] as DetalhesCondutorModel),
    shareReplay({ bufferSize: 1, refCount: true })
  );

  public excluir() {
    const exclusaoObserver: Observer<null> = {
      next: () => this.notificacaoService.sucesso(`O registro foi excluído com sucesso!`),
      error: (err) => this.notificacaoService.erro(err),
      complete: () => this.router.navigate(['/condutores']),
    };

    this.condutor$
      .pipe(
        take(1),
        switchMap((condutor) => this.condutorService.excluir(condutor.id))
      )
      .subscribe(exclusaoObserver);
  }
}
