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
import { AutomovelService } from '../automovel.service';
import { CombustivelEnum, DetalhesAutomovelModel, EditarAutomovelModel, EditarAutomovelResponseModel } from '../automovel.models';

@Component({
  selector: 'app-editar-automovel',
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

  templateUrl: './editar-automovel.html',
})
export class EditarAutomovel {
  protected readonly formBuilder = inject(FormBuilder);
  protected readonly route = inject(ActivatedRoute);
  protected readonly router = inject(Router);
  protected readonly automovelService = inject(AutomovelService);
  protected readonly notificacaoService = inject(NotificacaoService);
  protected readonly grupoAutomovelService = inject(GrupoAutomovelService);

  protected automovelForm: FormGroup = this.formBuilder.group({
    placa: ['', [Validators.required, Validators.minLength(3)]],
        modelo: ['', [Validators.required, Validators.minLength(3)]],
        marca: ['', [Validators.required, Validators.minLength(3)]],
        cor: ['', [Validators.required, Validators.minLength(3)]],
        foto: ['', [Validators.required, Validators.minLength(3)]],
        combustivel: [CombustivelEnum.Gasolina, [Validators.required]],
        ano: [  '',  [Validators.required, Validators.min(1900), Validators.max(2100),],],
        capacidadeTanque: [  '',  [Validators.required, Validators.min(10), Validators.max(10000),],],
        grupoAutomovelId: ['', [Validators.required]],
  });

    get modelo() {
      return this.automovelForm.get('modelo');
    }
    get placa() {
      return this.automovelForm.get('placa');
    }
    get marca() {
      return this.automovelForm.get('marca');
    }
    get cor() {
      return this.automovelForm.get('cor');
    }
    get foto() {
      return this.automovelForm.get('foto');
    }
    get combustivel() {
      return this.automovelForm.get('combustivel');
    }
    get ano() {
      return this.automovelForm.get('ano');
    }
    get capacidadeTanque() {
      return this.automovelForm.get('capacidadeTanque');
    }
    get grupoAutomovelId() {
      return this.automovelForm.get('grupoAutomovelId');
    }

    protected readonly tiposCombustivel = Object.values(CombustivelEnum);

    protected readonly grupoAutomoveis$ = this.route.data.pipe(
        map((data) => data['grupoAutomoveis'] ?? [])
      );

  protected readonly automovel$ = this.route.data.pipe(
    filter(data => data['automovel']),
    map(data => data['automovel'] as DetalhesAutomovelModel),
    tap(automovel => {
      this.automovelForm.patchValue({
        ...automovel,
        grupoAutomovelId: automovel.grupoAutomovel.id
      });
    }),
    shareReplay({ bufferSize: 1, refCount: true })
  );

    public editar() {
        if (this.automovelForm.invalid) return;

        const automovelModel: EditarAutomovelModel = this.automovelForm.value;

        const edicaoObserver: Observer<EditarAutomovelResponseModel> = {
          next: () =>
            this.notificacaoService.sucesso(
              `O registro "${automovelModel.modelo}" foi editado com sucesso!`
            ),
          error: (err) => this.notificacaoService.erro(err),
          complete: () => this.router.navigate(['/automoveis']),
        };

        this.automovel$
          .pipe(
            take(1),
            switchMap((automovel) => this.automovelService.editar(automovel.id, automovelModel))
          )
          .subscribe(edicaoObserver);
      }
}
