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
import { GrupoAutomovelService } from '../grupo-automovel.service';
import { CadastrarGrupoAutomovelModel, CadastrarGrupoAutomovelResponseModel } from '../grupo-automovel.models';

@Component({
  selector: 'app-cadastrar-grupo-automovel',
  imports: [
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    RouterLink,
    ReactiveFormsModule,
  ],
  templateUrl: './cadastrar-grupo-automovel.html',
})
export class CadastrarGrupoAutomovel {
  protected readonly formBuilder = inject(FormBuilder);
  protected readonly router = inject(Router);
  protected readonly grupoAutomovelService = inject(GrupoAutomovelService);
  protected readonly notificacaoService = inject(NotificacaoService);

  protected grupoAutomovelForm: FormGroup = this.formBuilder.group({
    nome: ['', [Validators.required, Validators.minLength(3)]],
  });

  get nome() {
    return this.grupoAutomovelForm.get('nome');
  }

  public cadastrar() {
    if (this.grupoAutomovelForm.invalid) return;

    const grupoAutomovelModel: CadastrarGrupoAutomovelModel = this.grupoAutomovelForm.value;

    const cadastroObserver: Observer<CadastrarGrupoAutomovelResponseModel> = {
      next: () =>
        this.notificacaoService.sucesso(
          `O registro "${grupoAutomovelModel.nome}" foi cadastrado com sucesso!`
        ),
      error: (err) => this.notificacaoService.erro(err),
      complete: () => this.router.navigate(['/grupo-automoveis']),
    };

    this.grupoAutomovelService.cadastrar(grupoAutomovelModel).subscribe(cadastroObserver);
  }
}