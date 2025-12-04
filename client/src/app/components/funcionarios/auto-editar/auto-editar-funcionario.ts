import { filter, map, Observer, shareReplay, switchMap, take, tap } from 'rxjs';

import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';

import { NotificacaoService } from '../../shared/notificacao/notificacao.service';
import { AutoEditarFuncionarioModel, AutoEditarFuncionarioResponseModel } from '../funcionario.models';
import { FuncionarioService } from '../funcionario.service';

@Component({
  selector: 'app-auto-editar-funcionario',
  imports: [
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    RouterLink,
    ReactiveFormsModule,
  ],
  templateUrl: './auto-editar-funcionario.html',
})
export class AutoEditarFuncionario {
  protected readonly formBuilder = inject(FormBuilder);
  protected readonly route = inject(ActivatedRoute);
  protected readonly router = inject(Router);
  protected readonly funcionarioService = inject(FuncionarioService);
  protected readonly notificacaoService = inject(NotificacaoService);

  protected funcionarioForm: FormGroup = this.formBuilder.group({
    nome: ['', [Validators.required, Validators.minLength(3)]]
  });

  get nome() {
    return this.funcionarioForm.get('nome');
  }

  constructor() {
  const usuarioLogadoString = sessionStorage.getItem('usuarioLogado');
  if (usuarioLogadoString) {
    const usuarioLogado = JSON.parse(usuarioLogadoString);
    this.funcionarioForm.patchValue({ nome: usuarioLogado.nome });
  }
}

  public editar() {
    if (this.funcionarioForm.invalid) return;

    const autoEditarModel: AutoEditarFuncionarioModel = this.funcionarioForm.value;

    const edicaoObserver: Observer<AutoEditarFuncionarioResponseModel> = {
      next: () =>
        this.notificacaoService.sucesso(
          `Seu nome foi atualizado para "${autoEditarModel.nome}" com sucesso!`
        ),
      error: (err) => this.notificacaoService.erro(err),
      complete: () => this.router.navigate(['/funcionarios']),
    };

    this.funcionarioService.autoEditar(autoEditarModel).subscribe(edicaoObserver);
  }
}
