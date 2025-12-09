import { map, Observer } from 'rxjs';

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
import { CadastrarAluguelModel, CadastrarAluguelResponseModel } from '../aluguel.models';
import { MatSelectModule } from '@angular/material/select';
import { MatOptionModule } from '@angular/material/core';
import { AsyncPipe, DecimalPipe } from '@angular/common';
import { ClienteService } from '../../clientes/cliente.service';
import { CondutorService } from '../../condutores/condutor.service';
import { AutomovelService } from '../../automoveis/automovel.service';
import { PlanoService } from '../../planos/plano.service';
import { TaxaService } from '../../taxas/taxa.service';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { parse } from 'date-fns';

@Component({
  selector: 'app-cadastrar-aluguel',
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
  templateUrl: './cadastrar-aluguel.html',
})
export class CadastrarAluguel {
  protected readonly formBuilder = inject(FormBuilder);
  protected readonly router = inject(Router);
  protected readonly aluguelService = inject(AluguelService);
  protected readonly notificacaoService = inject(NotificacaoService);
  protected readonly clienteService = inject(ClienteService);
  protected readonly condutorService = inject(CondutorService);
  protected readonly automovelService = inject(AutomovelService);
  protected readonly planoService = inject(PlanoService);
  protected readonly taxaService = inject(TaxaService);
  protected readonly route = inject(ActivatedRoute);

  protected aluguelForm: FormGroup = this.formBuilder.group({
    clienteId: [undefined, [Validators.required]],
    condutorId: [undefined, [Validators.required]],
    automovelId: [undefined, [Validators.required]],
    planoId: [undefined, [Validators.required]],
    quilometragemInicial: [  '',  [Validators.required, Validators.min(1), Validators.max(100000000),],],
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
  get quilometragemInicial() {return this.aluguelForm.get('quilometragemInicial');}
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

  public cadastrar() {
    if (this.aluguelForm.invalid) return;

    const aluguelModel: CadastrarAluguelModel = {
      ...this.aluguelForm.value,
      dataSaida: parse(this.dataSaida?.value, 'dd/MM/yyyy, HH:mm:ss', new Date().toISOString()),
      dataRetornoPrevista: parse(this.dataRetornoPrevista?.value, 'dd/MM/yyyy, HH:mm:ss', new Date().toISOString()),
      taxas: Array.isArray(this.taxas?.value)
        ? [...this.taxas?.value]
        : [this.taxas?.value],
    };

    const cadastroObserver: Observer<CadastrarAluguelResponseModel> = {
      next: () =>
        this.notificacaoService.sucesso(
          `O registro de Aluguel foi cadastrado com sucesso!`
        ),
      error: (err) => this.notificacaoService.erro(err),
      complete: () => this.router.navigate(['/alugueis']),
    };

    this.aluguelService.cadastrar(aluguelModel).subscribe(cadastroObserver);
  }
}
