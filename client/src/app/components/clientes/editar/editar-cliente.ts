import { filter, map, Observer, shareReplay, switchMap, take, tap } from 'rxjs';

import { AsyncPipe } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';

import { NotificacaoService } from '../../shared/notificacao/notificacao.service';
import { ClienteService } from '../cliente.service';
import { ClienteTipoEnum, DetalhesClienteModel, EditarClienteModel, EditarClienteResponseModel } from '../cliente.models';

@Component({
  selector: 'app-editar-cliente',
  imports: [
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    RouterLink,
    AsyncPipe,
    ReactiveFormsModule,
  ],
  templateUrl: './editar-cliente.html',
})
export class EditarCliente {
  private fb = inject(FormBuilder);
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private clienteService = inject(ClienteService);
  private notificacaoService = inject(NotificacaoService);

  protected clienteForm: FormGroup = this.fb.group({
    nome: ['', [Validators.required, Validators.minLength(3)]],
    documento: ['', [Validators.required, Validators.minLength(11)]],
    telefone: ['', [Validators.required, Validators.minLength(8)]],
    tipoCliente: [ClienteTipoEnum.PessoaFisica, [Validators.required]],
    cnh: [''], // opcional
    logradouro: ['', Validators.required],
    numero: [null, Validators.required],
    bairro: ['', Validators.required],
    cidade: ['', Validators.required],
    estado: ['', Validators.required],
  });

  get nome() { return this.clienteForm.get('nome'); }
  get documento() { return this.clienteForm.get('documento'); }
  get telefone() { return this.clienteForm.get('telefone'); }
  get tipoCliente() { return this.clienteForm.get('tipoCliente'); }
  get cnh() { return this.clienteForm.get('cnh'); }
  get logradouro() { return this.clienteForm.get('logradouro'); }
  get numero() { return this.clienteForm.get('numero'); }
  get bairro() { return this.clienteForm.get('bairro'); }
  get cidade() { return this.clienteForm.get('cidade'); }
  get estado() { return this.clienteForm.get('estado'); }

  protected readonly tiposCliente = Object.values(ClienteTipoEnum);

  protected readonly cliente$ = this.route.data.pipe(
      filter((data) => data['cliente']),
      map((data) => data['cliente'] as DetalhesClienteModel),
      tap((cliente) => this.clienteForm.patchValue(cliente)),
      shareReplay({ bufferSize: 1, refCount: true })
    );

  public editar() {
    if (this.clienteForm.invalid) return;

    const f = this.clienteForm.value;

    const clienteModel: EditarClienteModel = {
      ...this.clienteForm.value,
      cnh: f.tipoCliente === 'PessoaFisica' ? f.cnh : null,
      endereco: {
        logradouro: f.logradouro,
        numero: Number(f.numero),
        bairro: f.bairro,
        cidade: f.cidade,
        estado: f.estado
      }
    };

    const edicaoObserver: Observer<EditarClienteResponseModel> = {
      next: () => this.notificacaoService.sucesso(`O registro "${clienteModel.nome}" foi editado com sucesso!`),
      error: (err) => this.notificacaoService.erro(err),
      complete: () => this.router.navigate(['/clientes']),
    };

    this.cliente$
      .pipe(
        take(1),
        switchMap((cliente) => this.clienteService.editar(cliente.id, clienteModel))
      )
      .subscribe(edicaoObserver);
  }
}
