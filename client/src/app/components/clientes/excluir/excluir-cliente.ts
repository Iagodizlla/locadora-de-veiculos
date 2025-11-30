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
import { ClienteService } from '../cliente.service';
import { DetalhesClienteModel } from '../cliente.models';

@Component({
  selector: 'app-excluir-cliente',
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
  templateUrl: './excluir-cliente.html',
})
export class ExcluirCliente {
  protected readonly route = inject(ActivatedRoute);
  protected readonly router = inject(Router);
  protected readonly clienteService = inject(ClienteService);
  protected readonly notificacaoService = inject(NotificacaoService);

  protected readonly cliente$ = this.route.data.pipe(
    filter((data) => data['cliente']),
    map((data) => data['cliente'] as DetalhesClienteModel),
    shareReplay({ bufferSize: 1, refCount: true })
  );

  public excluir() {
    const exclusaoObserver: Observer<null> = {
      next: () => this.notificacaoService.sucesso(`O registro foi excluído com sucesso!`),
      error: (err) => this.notificacaoService.erro(err),
      complete: () => this.router.navigate(['/clientes']),
    };

    this.cliente$
      .pipe(
        take(1),
        switchMap((cliente) => this.clienteService.excluir(cliente.id))
      )
      .subscribe(exclusaoObserver);
  }
}
