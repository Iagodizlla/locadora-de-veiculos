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
import { GrupoAutomovelService } from '../../grupo-automoveis/grupo-automovel.service';
import { MatSelectModule } from '@angular/material/select';
import { MatOptionModule } from '@angular/material/core';

import { NotificacaoService } from '../../shared/notificacao/notificacao.service';
import { PlanoService } from '../plano.service';
import { DetalhesPlanoModel, EditarPlanoModel, EditarPlanoResponseModel } from '../plano.models';

@Component({
  selector: 'app-editar-plano',
  imports: [
  MatCardModule,
  MatButtonModule,
  MatIconModule,
  MatFormFieldModule,
  MatInputModule,
  MatSelectModule,
  MatOptionModule,
  RouterLink,
  AsyncPipe,
  ReactiveFormsModule,
],

  templateUrl: './editar-plano.html',
})
export class EditarPlano {
  protected readonly formBuilder = inject(FormBuilder);
  protected readonly route = inject(ActivatedRoute);
  protected readonly router = inject(Router);
  protected readonly planoService = inject(PlanoService);
  protected readonly notificacaoService = inject(NotificacaoService);
  protected readonly grupoAutomovelService = inject(GrupoAutomovelService);

  protected planoForm: FormGroup = this.formBuilder.group({
        grupoAutomovelId: ['', [Validators.required]],
        precoDiario: ['', [Validators.required, Validators.min(0)],],
        precoDiarioControlado: ['', [Validators.required, Validators.min(0)],],
        precoPorKm: ['', [Validators.required, Validators.min(0)],],
        kmLivres: ['', [Validators.required, Validators.min(0)],],
        precoPorKmExplorado: ['', [Validators.required, Validators.min(0)],],
        precoLivre: ['', [Validators.required, Validators.min(0)],],
  });

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
    get grupoAutomovelId() {
      return this.planoForm.get('grupoAutomovelId');
    }

    protected readonly grupoAutomoveis$ = this.route.data.pipe(
        map((data) => data['grupoAutomoveis'] ?? [])
      );

  protected readonly plano$ = this.route.data.pipe(
    filter(data => data['plano']),
    map(data => data['plano'] as DetalhesPlanoModel),
    tap(plano => {
      this.planoForm.patchValue({
        ...plano,
        grupoAutomovelId: plano.grupoAutomovel.id
      });
    }),
    shareReplay({ bufferSize: 1, refCount: true })
  );

    public editar() {
        if (this.planoForm.invalid) return;

        const planoModel: EditarPlanoModel = this.planoForm.value;

        const edicaoObserver: Observer<EditarPlanoResponseModel> = {
          next: () =>
            this.notificacaoService.sucesso(
              `O registro do grupo "${planoModel.grupoAutomovel.nome}" foi editado com sucesso!`
            ),
          error: (err) => this.notificacaoService.erro(err),
          complete: () => this.router.navigate(['/planos']),
        };

        this.plano$
          .pipe(
            take(1),
            switchMap((plano) => this.planoService.editar(plano.id, planoModel))
          )
          .subscribe(edicaoObserver);
      }
}
