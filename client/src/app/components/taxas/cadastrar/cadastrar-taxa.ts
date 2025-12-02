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
import { TaxaService } from '../taxa.service';
import { CadastrarTaxaModel, CadastrarTaxaResponseModel, ServicoEnum } from '../taxa.models';
import { MatSelectModule } from '@angular/material/select';

@Component({
  selector: 'app-cadastrar-taxa',
  imports: [
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    RouterLink,
    ReactiveFormsModule,
    MatSelectModule,
  ],
  templateUrl: './cadastrar-taxa.html',
})
export class CadastrarTaxa {
  protected readonly formBuilder = inject(FormBuilder);
  protected readonly router = inject(Router);
  protected readonly taxaService = inject(TaxaService);
  protected readonly notificacaoService = inject(NotificacaoService);

  protected taxaForm: FormGroup = this.formBuilder.group({
    nome: ['', [Validators.required, Validators.minLength(3)]],
    preco: ['', [Validators.required, Validators.min(0)],],
    servico: [ServicoEnum.PrecoFixo, [Validators.required]],
  });

  get nome() {
    return this.taxaForm.get('nome');
  }
  get preco() {
    return this.taxaForm.get('preco');
  }
  get servico() {
    return this.taxaForm.get('servico');
  }

  protected readonly tiposServicos = Object.values(ServicoEnum);

  public cadastrar() {
    if (this.taxaForm.invalid) return;

    const taxaModel: CadastrarTaxaModel = this.taxaForm.value;

    const cadastroObserver: Observer<CadastrarTaxaResponseModel> = {
      next: () =>
        this.notificacaoService.sucesso(
          `O registro "${taxaModel.nome}" foi cadastrado com sucesso!`
        ),
      error: (err) => this.notificacaoService.erro(err),
      complete: () => this.router.navigate(['/taxas']),
    };

    this.taxaService.cadastrar(taxaModel).subscribe(cadastroObserver);
  }
}
