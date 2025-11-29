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
import { CondutorService } from '../condutor.service';
import { CategoriaCnhEnum, DetalhesCondutorModel, EditarCondutorModel, EditarCondutorResponseModel } from '../condutor.models';
import { MatSelectModule } from '@angular/material/select';
import { MatOptionModule } from '@angular/material/core';
import { MatCheckboxModule } from '@angular/material/checkbox';

@Component({
  selector: 'app-editar-condutor',
  imports: [
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    RouterLink,
    AsyncPipe,
    ReactiveFormsModule,
    MatSelectModule,
    MatOptionModule,
    MatCheckboxModule
  ],
  templateUrl: './editar-condutor.html',
})
export class EditarCondutor {
  protected readonly formBuilder = inject(FormBuilder);
  protected readonly route = inject(ActivatedRoute);
  protected readonly router = inject(Router);
  protected readonly condutorService = inject(CondutorService);
  protected readonly notificacaoService = inject(NotificacaoService);

  protected condutorForm: FormGroup = this.formBuilder.group({
    nome: ['', [Validators.required, Validators.minLength(3)]],
        cnh: ['', [Validators.required, Validators.minLength(3)]],
        cpf: ['', [Validators.required, Validators.minLength(11)]],
        telefone: ['', [Validators.required, Validators.minLength(8)]],
        categoria: [CategoriaCnhEnum.A, [Validators.required]],
        validadeCnh: [new Date().toLocaleString('pt-Br'), [Validators.required]],
        eCliente: [false]
      });

      get nome() {
        return this.condutorForm.get('nome');
      }
      get cnh() {
        return this.condutorForm.get('cnh');
      }
      get cpf() {
        return this.condutorForm.get('cpf');
      }
      get telefone() {
        return this.condutorForm.get('telefone');
      }
      get categoria() {
        return this.condutorForm.get('categoria');
      }
      get validadeCnh() {
        return this.condutorForm.get('validadeCnh');
      }
      get eCliente() {
        return this.condutorForm.get('eCliente');
      }

      protected readonly tiposCategorias = Object.values(CategoriaCnhEnum);

  protected readonly condutor$ = this.route.data.pipe(
    filter((data) => data['condutor']),
    map((data) => data['condutor'] as DetalhesCondutorModel),
    tap((condutor) => this.condutorForm.patchValue(condutor)),
    shareReplay({ bufferSize: 1, refCount: true })
  );

  public editar() {
    if (this.condutorForm.invalid) return;

    const condutorModel: EditarCondutorModel = this.condutorForm.value;

    const edicaoObserver: Observer<EditarCondutorResponseModel> = {
      next: () =>
        this.notificacaoService.sucesso(
          `O registro "${condutorModel.nome}" foi editado com sucesso!`
        ),
      error: (err) => this.notificacaoService.erro(err),
      complete: () => this.router.navigate(['/condutores']),
    };

    this.condutor$
      .pipe(
        take(1),
        switchMap((condutor) => this.condutorService.editar(condutor.id, condutorModel))
      )
      .subscribe(edicaoObserver);
  }
}
