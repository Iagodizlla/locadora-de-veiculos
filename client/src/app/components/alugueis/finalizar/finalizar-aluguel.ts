import { filter, map, Observer, shareReplay, switchMap, take, tap } from 'rxjs';

import { AsyncPipe } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';

import { NotificacaoService } from '../../shared/notificacao/notificacao.service';
import { AluguelService } from '../aluguel.service';
import { DetalhesAluguelModel, FinalizarAluguelModel, FinalizarAluguelResponseModel } from '../aluguel.models';
import { format } from 'date-fns';

@Component({
  selector: 'app-finalizar-aluguel',
  imports: [
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    RouterLink,
    ReactiveFormsModule,
    AsyncPipe,
  ],
  templateUrl: './finalizar-aluguel.html',
})
export class FinalizarAluguel {
  protected readonly formBuilder = inject(FormBuilder);
  protected readonly route = inject(ActivatedRoute);
  protected readonly router = inject(Router);
  protected readonly aluguelService = inject(AluguelService);
  protected readonly notificacaoService = inject(NotificacaoService);

  protected aluguelForm: FormGroup = this.formBuilder.group({
    kmFinal: [  '',  [Validators.required, Validators.min(0), Validators.max(100000000),],],
    nivelCombustivelNaDevolucao: [  '',  [Validators.required, Validators.min(1), Validators.max(10000),],],
    dataDevolucao: [new Date().toLocaleString('pt-Br'), [Validators.required]],
  });

  get kmFinal() {return this.aluguelForm.get('kmFinal');}
  get nivelCombustivelNaDevolucao() {return this.aluguelForm.get('nivelCombustivelNaDevolucao');}
  get dataDevolucao() {return this.aluguelForm.get('dataDevolucao');}

  protected readonly aluguel$ = this.route.data.pipe(
    filter((data) => data['aluguel']),
    map((data) => data['aluguel'] as DetalhesAluguelModel),
    tap((aluguel) => {
      this.aluguelForm.patchValue({
        ...aluguel,
        dataDevolucao: format(new Date(), 'dd/MM/yyyy HH:mm'),
      });
    }),
    shareReplay({ bufferSize: 1, refCount: true })
  );

  public finalizar() {
    if (this.aluguelForm.invalid) return;

    const aluguelModel: FinalizarAluguelModel = this.aluguelForm.value;

    const dataDevolucaoIso = new Date(aluguelModel.dataDevolucao).toISOString();

    const edicaoObserver: Observer<FinalizarAluguelResponseModel> = {
      next: () =>
        this.notificacaoService.sucesso(
          `O registro aluguel foi finalizado com sucesso!`
        ),
      error: (err) => this.notificacaoService.erro(err),
      complete: () => this.router.navigate(['/alugueis']),
    };

    this.aluguel$
      .pipe(
        take(1),
        switchMap((aluguel) => {

          const finalizarModel: FinalizarAluguelModel = {
            id: aluguel.id,
            dataDevolucao: dataDevolucaoIso as any,
            kmFinal: aluguelModel.kmFinal,
            nivelCombustivelNaDevolucao: aluguelModel.nivelCombustivelNaDevolucao,
          };
          return this.aluguelService.finalizar(aluguel.id, finalizarModel);
        })
      )
      .subscribe(edicaoObserver);
  }
}
