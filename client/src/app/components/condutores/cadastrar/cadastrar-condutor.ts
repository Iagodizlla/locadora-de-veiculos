import { parse } from 'date-fns';
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
import { CondutorService } from '../condutor.service';
import { CadastrarCondutorModel, CadastrarCondutorResponseModel, CategoriaCnhEnum } from '../condutor.models';
import { MatOptionModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';

@Component({
  selector: 'app-cadastrar-condutor',
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
    MatCheckboxModule
  ],
  templateUrl: './cadastrar-condutor.html',
})
export class CadastrarCondutor {
  protected readonly formBuilder = inject(FormBuilder);
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

  public cadastrar() {
    if (this.condutorForm.invalid) return;

    const condutorModel: CadastrarCondutorModel = {
      ...this.condutorForm.value,
    validadeCnh: parse(this.validadeCnh?.value, 'dd/MM/yyyy, HH:mm:ss', new Date().toISOString()),};

    const cadastroObserver: Observer<CadastrarCondutorResponseModel> = {
      next: () =>
        this.notificacaoService.sucesso(
          `O registro "${condutorModel.nome}" foi cadastrado com sucesso!`
        ),
      error: (err) => this.notificacaoService.erro(err),
      complete: () => this.router.navigate(['/condutores']),
    };

    this.condutorService.cadastrar(condutorModel).subscribe(cadastroObserver);
  }
}
