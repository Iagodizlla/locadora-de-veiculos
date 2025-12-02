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
import { TaxaService } from '../taxa.service';
import { DetalhesTaxaModel } from '../taxa.models';

@Component({
  selector: 'app-excluir-taxa',
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
  templateUrl: './excluir-taxa.html',
})
export class ExcluirTaxa {
  protected readonly route = inject(ActivatedRoute);
  protected readonly router = inject(Router);
  protected readonly taxaService = inject(TaxaService);
  protected readonly notificacaoService = inject(NotificacaoService);

  protected readonly taxa$ = this.route.data.pipe(
    filter((data) => data['taxa']),
    map((data) => data['taxa'] as DetalhesTaxaModel),
    shareReplay({ bufferSize: 1, refCount: true })
  );

  public excluir() {
    const exclusaoObserver: Observer<null> = {
      next: () => this.notificacaoService.sucesso(`O registro foi excluído com sucesso!`),
      error: (err) => this.notificacaoService.erro(err),
      complete: () => this.router.navigate(['/taxas']),
    };

    this.taxa$
      .pipe(
        take(1),
        switchMap((taxa) => this.taxaService.excluir(taxa.id))
      )
      .subscribe(exclusaoObserver);
  }
}
