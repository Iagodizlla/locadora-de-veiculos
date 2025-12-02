import { map, Observer, shareReplay, switchMap, take, tap } from 'rxjs';

import { AsyncPipe } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { Router, RouterLink } from '@angular/router';

import { NotificacaoService } from '../../shared/notificacao/notificacao.service';
import { ConfigService } from '../config.service';
import { DetalhesConfigModel, EditarConfigModel, EditarConfigResponseModel } from '../config.models';

@Component({
  providers: [ConfigService],
  selector: 'app-editar-config',
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
  templateUrl: './editar-config.html',
})
export class EditarConfig {
  protected readonly formBuilder = inject(FormBuilder);
  protected readonly router = inject(Router);
  protected readonly configService = inject(ConfigService);
  protected readonly notificacaoService = inject(NotificacaoService);

  protected configForm: FormGroup = this.formBuilder.group({
    gasolina: [0, [Validators.required, Validators.min(1)]],
    gas: [0, [Validators.required, Validators.min(1)]],
    diesel: [0, [Validators.required, Validators.min(1)]],
    alcool: [0, [Validators.required, Validators.min(1)]],
  });

  get gasolina() { return this.configForm.get('gasolina'); }
  get gas() { return this.configForm.get('gas'); }
  get diesel() { return this.configForm.get('diesel'); }
  get alcool() { return this.configForm.get('alcool'); }

  protected readonly config$ = this.configService.selecionar().pipe(
    tap((config: DetalhesConfigModel) => this.configForm.patchValue(config)),
    shareReplay({ bufferSize: 1, refCount: true })
  );

  public editar() {
    if (this.configForm.invalid) return;

    const model: EditarConfigModel = this.configForm.value;

    const edicaoObserver: Observer<EditarConfigResponseModel> = {
      next: () => this.notificacaoService.sucesso('Configuração editada com sucesso!'),
      error: (err) => this.notificacaoService.erro(err),
      complete: () => this.router.navigate(['/inicio']),
    };

    this.config$
      .pipe(
        take(1),
        switchMap(() => this.configService.editar(model))
      )
      .subscribe(edicaoObserver);
  }
}
