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
import { DetalhesFuncionarioModel, EditarFuncionarioModel, EditarFuncionarioResponseModel } from '../funcionario.models';
import { FuncionarioService } from '../funcionario.service';

@Component({
  selector: 'app-editar-funcionario',
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
  templateUrl: './editar-funcionario.html',
})
export class EditarFuncionario {
  protected readonly formBuilder = inject(FormBuilder);
  protected readonly route = inject(ActivatedRoute);
  protected readonly router = inject(Router);
  protected readonly funcionarioService = inject(FuncionarioService);
  protected readonly notificacaoService = inject(NotificacaoService);

  protected funcionarioForm: FormGroup = this.formBuilder.group({
    nome: ['', [Validators.required, Validators.minLength(3)]],
    admissao: [new Date().toLocaleString('pt-Br'), [Validators.required]],
    salario: ['', [Validators.required, Validators.min(0)],]
  });

  get nome() {
    return this.funcionarioForm.get('nome');
  }
  get admissao() {
    return this.funcionarioForm.get('admissao');
  }
  get salario() {
    return this.funcionarioForm.get('salario');
  }

  protected readonly funcionario$ = this.route.data.pipe(
    filter((data) => data['funcionario']),
    map((data) => data['funcionario'] as DetalhesFuncionarioModel),
    tap((funcionario) => this.funcionarioForm.patchValue(funcionario)),
    shareReplay({ bufferSize: 1, refCount: true })
  );

  public editar() {
    if (this.funcionarioForm.invalid) return;

    const funcionarioModel: EditarFuncionarioModel = this.funcionarioForm.value;

    const edicaoObserver: Observer<EditarFuncionarioResponseModel> = {
      next: () =>
        this.notificacaoService.sucesso(
          `O registro "${funcionarioModel.nome}" foi editado com sucesso!`
        ),
      error: (err) => this.notificacaoService.erro(err),
      complete: () => this.router.navigate(['/funcionarios']),
    };

    this.funcionario$
      .pipe(
        take(1),
        switchMap((funcionario) => this.funcionarioService.editar(funcionario.id, funcionarioModel))
      )
      .subscribe(edicaoObserver);
  }
}
