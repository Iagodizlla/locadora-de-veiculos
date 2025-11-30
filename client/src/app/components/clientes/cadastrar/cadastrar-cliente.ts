import { Observer } from 'rxjs';
import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';

import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatOptionModule } from '@angular/material/core';
import { MatCheckboxModule } from '@angular/material/checkbox';

import { NotificacaoService } from '../../shared/notificacao/notificacao.service';
import { ClienteService } from '../cliente.service';
import { CadastrarClienteModel, ClienteTipoEnum } from '../cliente.models';

@Component({
  selector: 'app-cadastrar-cliente',
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
  templateUrl: './cadastrar-cliente.html',
})
export class CadastrarCliente {
  protected readonly formBuilder = inject(FormBuilder);
  protected readonly router = inject(Router);
  protected readonly clienteService = inject(ClienteService);
  protected readonly notificacaoService = inject(NotificacaoService);

  protected clienteForm: FormGroup = this.formBuilder.group({
    nome: ['', [Validators.required, Validators.minLength(3)]],
    documento: ['', [Validators.required, Validators.minLength(11)]],
    telefone: ['', [Validators.required, Validators.minLength(8)]],
    tipoCliente: [ClienteTipoEnum.PessoaFisica, [Validators.required]],
    cnh: [''], // Opcional, só para Pessoa Física
    endereco: this.formBuilder.group({
      logradouro: ['', Validators.required],
      numero: [null, Validators.required],
      bairro: ['', Validators.required],
      cidade: ['', Validators.required],
      estado: ['', Validators.required],
    }),
  });

  protected readonly tiposClientes = Object.values(ClienteTipoEnum);

  get nome() { return this.clienteForm.get('nome'); }
  get documento() { return this.clienteForm.get('documento'); }
  get telefone() { return this.clienteForm.get('telefone'); }
  get tipoCliente() { return this.clienteForm.get('tipoCliente'); }
  get cnh() { return this.clienteForm.get('cnh'); }
  get endereco() { return this.clienteForm.get('endereco') as FormGroup; }

  public cadastrar() {
    if (this.clienteForm.invalid) return;

    const formValue = this.clienteForm.value;

    const clienteModel: CadastrarClienteModel = {
      ...this.clienteForm.value,
      cnh: formValue.tipoCliente === 'PessoaFisica' ? formValue.cnh : null,
      endereco: {
        logradouro: formValue.endereco.logradouro,
        numero: formValue.endereco.numero,
        bairro: formValue.endereco.bairro,
        cidade: formValue.endereco.cidade,
        estado: formValue.endereco.estado
      }
    };

    this.clienteService.cadastrar(clienteModel).subscribe({
      next: () =>
        this.notificacaoService.sucesso(
          `O registro "${clienteModel.nome}" foi cadastrado com sucesso!`
        ),
      error: (err) => this.notificacaoService.erro(err),
      complete: () => this.router.navigate(['/clientes'])
    });
  }
}
