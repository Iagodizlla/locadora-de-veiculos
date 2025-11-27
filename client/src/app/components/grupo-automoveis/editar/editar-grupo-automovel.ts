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
import { GrupoAutomovelService } from '../grupo-automovel.service';
import { DetalhesGrupoAutomovelModel, EditarGrupoAutomovelModel, EditarGrupoAutomovelResponseModel } from '../grupo-automovel.models';

@Component({
  selector: 'app-editar-grupo-automovel',
  imports: [
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    RouterLink,
    AsyncPipe,
    ReactiveFormsModule,
  ],
  templateUrl: './editar-grupo-automovel.html',
})
export class EditarGrupoAutomovel {
  protected readonly formBuilder = inject(FormBuilder);
  protected readonly route = inject(ActivatedRoute);
  protected readonly router = inject(Router);
  protected readonly grupoAutomovelService = inject(GrupoAutomovelService);
  protected readonly notificacaoService = inject(NotificacaoService);

  protected grupoAutomovelForm: FormGroup = this.formBuilder.group({
    nome: ['', [Validators.required, Validators.minLength(3)]],
  });

  get nome() {
    return this.grupoAutomovelForm.get('nome');
  }

  protected readonly grupoAutomovel$ = this.route.data.pipe(
    filter((data) => data['grupoAutomovel']),
    map((data) => data['grupoAutomovel'] as DetalhesGrupoAutomovelModel),
    tap((grupoAutomovel) => this.grupoAutomovelForm.patchValue(grupoAutomovel)),
    shareReplay({ bufferSize: 1, refCount: true })
  );

  public editar() {
    if (this.grupoAutomovelForm.invalid) return;

    const grupoAutomovelModel: EditarGrupoAutomovelModel = this.grupoAutomovelForm.value;

    const edicaoObserver: Observer<EditarGrupoAutomovelResponseModel> = {
      next: () =>
        this.notificacaoService.sucesso(
          `O registro "${grupoAutomovelModel.nome}" foi editado com sucesso!`
        ),
      error: (err) => this.notificacaoService.erro(err),
      complete: () => this.router.navigate(['/grupo-automoveis']),
    };

    this.grupoAutomovel$
      .pipe(
        take(1),
        switchMap((grupoAutomovel) => this.grupoAutomovelService.editar(grupoAutomovel.id, grupoAutomovelModel))
      )
      .subscribe(edicaoObserver);
  }
}