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
import { TaxaService } from '../taxa.service';
import { DetalhesTaxaModel, EditarTaxaModel, EditarTaxaResponseModel, ServicoEnum } from '../taxa.models';
import { MatSelectModule } from '@angular/material/select';

@Component({
  selector: 'app-editar-taxa',
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
  ],
  templateUrl: './editar-taxa.html',
})
export class EditarTaxa {
  protected readonly formBuilder = inject(FormBuilder);
  protected readonly route = inject(ActivatedRoute);
  protected readonly router = inject(Router);
  protected readonly taxaService = inject(TaxaService);
  protected readonly notificacaoService = inject(NotificacaoService);

  protected  taxaForm: FormGroup = this.formBuilder.group({
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

  protected readonly taxa$ = this.route.data.pipe(
    filter((data) => data['taxa']),
    map((data) => data['taxa'] as DetalhesTaxaModel),
    tap((taxa) => this.taxaForm.patchValue(taxa)),
    shareReplay({ bufferSize: 1, refCount: true })
  );

  public editar() {
    if (this.taxaForm.invalid) return;

    const taxaModel: EditarTaxaModel = this.taxaForm.value;

    const edicaoObserver: Observer<EditarTaxaResponseModel> = {
      next: () =>
        this.notificacaoService.sucesso(
          `O registro "${taxaModel.nome}" foi editado com sucesso!`
        ),
      error: (err) => this.notificacaoService.erro(err),
      complete: () => this.router.navigate(['/taxas']),
    };

    this.taxa$
      .pipe(
        take(1),
        switchMap((taxa) => this.taxaService.editar(taxa.id, taxaModel))
      )
      .subscribe(edicaoObserver);
  }
}
