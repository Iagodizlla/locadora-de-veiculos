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
import { DetalhesAluguelModel, EditarAluguelModel, EditarAluguelResponseModel } from '../aluguel.models';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatOptionModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';
import { ClienteService } from '../../clientes/cliente.service';
import { CondutorService } from '../../condutores/condutor.service';
import { AutomovelService } from '../../automoveis/automovel.service';
import { PlanoService } from '../../planos/plano.service';
import { TaxaService } from '../../taxas/taxa.service';

@Component({
  selector: 'app-editar-aluguel',
  imports: [
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    RouterLink,
    ReactiveFormsModule,
    MatSelectModule,
    MatOptionModule,
    AsyncPipe,
    MatCheckboxModule,
    ReactiveFormsModule,
  ],
  templateUrl: './editar-aluguel.html',
})
export class EditarAluguel {
  protected readonly formBuilder = inject(FormBuilder);
  protected readonly route = inject(ActivatedRoute);
  protected readonly router = inject(Router);
  protected readonly aluguelService = inject(AluguelService);
  protected readonly notificacaoService = inject(NotificacaoService);
  protected readonly clienteService = inject(ClienteService);
  protected readonly condutorService = inject(CondutorService);
  protected readonly automovelService = inject(AutomovelService);
  protected readonly planoService = inject(PlanoService);
  protected readonly taxaService = inject(TaxaService);

  protected aluguelForm: FormGroup = this.formBuilder.group({
    clienteId: [undefined, [Validators.required]],
    condutorId: [undefined, [Validators.required]],
    automovelId: [undefined, [Validators.required]],
    planoId: [undefined, [Validators.required]],
    kmInicial: [  '',  [Validators.required, Validators.min(0), Validators.max(100000000),],],
    nivelCombustivelNaSaida: [  '',  [Validators.required, Validators.min(1), Validators.max(10000),],],
    seguroCliente: [false],
    seguroTerceiro: [false],
    valorSeguroPorDia: [  '',  [Validators.min(0), Validators.max(10000),],],
    dataSaida: [new Date().toLocaleString('pt-Br'), [Validators.required]],
    dataRetornoPrevista: [new Date().toLocaleString('pt-Br'), [Validators.required]],
    taxas: [],
  });

  get clienteId() {return this.aluguelForm.get('clienteId');}
  get condutorId() {return this.aluguelForm.get('condutorId');}
  get automovelId() {return this.aluguelForm.get('automovelId');}
  get planoId() {return this.aluguelForm.get('planoId');}
  get taxas() {return this.aluguelForm.get('taxas');}
  get kmInicial() {return this.aluguelForm.get('kmInicial');}
  get nivelCombustivelNaSaida() {return this.aluguelForm.get('nivelCombustivelNaSaida');}
  get seguroCliente() {return this.aluguelForm.get('seguroCliente');}
  get seguroTerceiro() {return this.aluguelForm.get('seguroTerceiro');}
  get valorSeguroPorDia() {return this.aluguelForm.get('valorSeguroPorDia');}
  get dataSaida() {return this.aluguelForm.get('dataSaida');}
  get dataRetornoPrevista() {return this.aluguelForm.get('dataRetornoPrevista');}

  protected readonly clientes$ = this.route.data.pipe(map((data) => data['clientes'] ?? []));
  protected readonly condutores$ = this.route.data.pipe(map((data) => data['condutores'] ?? []));
  protected readonly automoveis$ = this.route.data.pipe(map((data) => data['automoveis'] ?? []));
  protected readonly planos$ = this.route.data.pipe(map((data) => data['planos'] ?? []));
  protected readonly taxas$ = this.route.data.pipe(map((data) => data['taxas'] ?? []));

  protected readonly aluguel$ = this.route.data.pipe(
    filter((data) => data['aluguel']),
    map((data) => data['aluguel'] as DetalhesAluguelModel),
    tap((aluguel) => {
      this.aluguelForm.patchValue({
        ...aluguel,
        automovelId: aluguel.automovel.id,
        condutorId: aluguel.condutor.id,
        clienteId: aluguel.cliente.id,
        planoId: aluguel.plano.id,
        taxas: aluguel.taxas.map((taxa) => taxa.id),
      });
    }),
    shareReplay({ bufferSize: 1, refCount: true })
  );

  public editar() {
    if (this.aluguelForm.invalid) return;

    const aluguelModel: EditarAluguelModel = this.aluguelForm.value;

    const edicaoObserver: Observer<EditarAluguelResponseModel> = {
      next: () =>
        this.notificacaoService.sucesso(
          `O registro aluguel foi editado com sucesso!`
        ),
      error: (err) => this.notificacaoService.erro(err),
      complete: () => this.router.navigate(['/alugueis']),
    };

    this.aluguel$
      .pipe(
        take(1),
        switchMap((aluguel) => this.aluguelService.editar(aluguel.id, aluguelModel))
      )
      .subscribe(edicaoObserver);
  }
}
