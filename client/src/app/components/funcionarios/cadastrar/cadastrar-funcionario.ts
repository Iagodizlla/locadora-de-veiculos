import { Observer } from 'rxjs';

import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { Router, RouterLink } from '@angular/router';

import { NotificacaoService } from '../../shared/notificacao/notificacao.service';
import { FuncionarioService } from '../funcionario.service';
import { CadastrarFuncionarioModel, CadastrarFuncionarioResponseModel } from '../funcionario.models';
import { parse } from 'date-fns';

@Component({
  selector: 'app-cadastrar-funcionario',
  imports: [
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    RouterLink,
    ReactiveFormsModule,
  ],
  templateUrl: './cadastrar-funcionario.html',
})
export class CadastrarFuncionario {
  protected readonly formBuilder = inject(FormBuilder);
  protected readonly router = inject(Router);
  protected readonly funcionarioService = inject(FuncionarioService);
  protected readonly notificacaoService = inject(NotificacaoService);

  protected funcionarioForm: FormGroup = this.formBuilder.group({
    nome: ['', [Validators.required, Validators.minLength(3)]],
    admissao: [new Date().toLocaleString('pt-Br'), [Validators.required]],
    salario: ['', [Validators.required, Validators.min(0)],],
    userName: ['', [Validators.required, Validators.minLength(3)]],
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6)]],
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
  get userName() {
    return this.funcionarioForm.get('userName');
  }
  get email() {
    return this.funcionarioForm.get('email');
  }
  get password() {
    return this.funcionarioForm.get('password');
  }

  public cadastrar() {
    if (this.funcionarioForm.invalid) return;

    const funcionarioModel: CadastrarFuncionarioModel = {
      ...this.funcionarioForm.value,
      admissao: parse(this.admissao?.value, 'dd/MM/yyyy, HH:mm:ss', new Date().toISOString()),};

    const cadastroObserver: Observer<CadastrarFuncionarioResponseModel> = {
      next: () =>
        this.notificacaoService.sucesso(
          `O registro "${funcionarioModel.nome}" foi cadastrado com sucesso!`
        ),
      error: (err) => this.notificacaoService.erro(err),
      complete: () => this.router.navigate(['/funcionarios']),
    };

    this.funcionarioService.cadastrar(funcionarioModel).subscribe(cadastroObserver);
  }
}
