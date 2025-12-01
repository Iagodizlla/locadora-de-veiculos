import { filter, map, Observer, shareReplay } from 'rxjs';
import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatOptionModule } from '@angular/material/core';
import { GrupoAutomovelService } from '../../grupo-automoveis/grupo-automovel.service';

import { ActivatedRoute, Router, RouterLink } from '@angular/router';

import { NotificacaoService } from '../../shared/notificacao/notificacao.service';
import { PlanoService } from '../plano.service';
import { CadastrarPlanoModel, CadastrarPlanoResponseModel } from '../plano.models';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-cadastrar-plano',
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
  ],
  templateUrl: './cadastrar-plano.html',
})
export class CadastrarPlano {
  protected readonly formBuilder = inject(FormBuilder);
  protected readonly router = inject(Router);
  protected readonly planoService = inject(PlanoService);
  protected readonly notificacaoService = inject(NotificacaoService);
  protected readonly grupoAutomovelService = inject(GrupoAutomovelService);
  protected readonly route = inject(ActivatedRoute);

  protected planoForm: FormGroup = this.formBuilder.group({
    grupoAutomovelId: [undefined, [Validators.required]],
    precoDiario: ['', [Validators.required, Validators.min(0)],],
    precoDiarioControlado: ['', [Validators.required, Validators.min(0)],],
    precoPorKm: ['', [Validators.required, Validators.min(0)],],
    kmLivres: ['', [Validators.required, Validators.min(0)],],
    precoPorKmExplorado: ['', [Validators.required, Validators.min(0)],],
    precoLivre: ['', [Validators.required, Validators.min(0)],],
  });

  get grupoAutomovelId() {
    return this.planoForm.get('grupoAutomovelId');
  }
  get precoDiario() {
    return this.planoForm.get('precoDiario');
  }
  get precoDiarioControlado() {
    return this.planoForm.get('precoDiarioControlado');
  }
  get precoPorKm() {
    return this.planoForm.get('precoPorKm');
  }
  get kmLivres() {
    return this.planoForm.get('kmLivres');
  }
  get precoPorKmExplorado() {
    return this.planoForm.get('precoPorKmExplorado');
  }
  get precoLivre() {
    return this.planoForm.get('precoLivre');
  }

  protected readonly grupoAutomoveis$ = this.route.data.pipe(
    map((data) => data['grupoAutomoveis'] ?? [])
  );

  public cadastrar() {
    if (this.planoForm.invalid) return;

    const planoModel: CadastrarPlanoModel = this.planoForm.value;

    const cadastroObserver: Observer<CadastrarPlanoResponseModel> = {
      next: () =>
        this.notificacaoService.sucesso(
          `O registro do grupo "${planoModel.grupoAutomovel.nome}" foi cadastrado com sucesso!`
        ),
      error: (err) => this.notificacaoService.erro(err),
      complete: () => this.router.navigate(['/planos']),
    };

    this.planoService.cadastrar(planoModel).subscribe(cadastroObserver);
  }
}