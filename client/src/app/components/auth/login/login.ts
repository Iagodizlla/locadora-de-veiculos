import { PartialObserver } from 'rxjs';

import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { Router, RouterLink } from '@angular/router';

import { NotificacaoService } from '../../shared/notificacao/notificacao.service';
import { AccessTokenModel, LoginModel } from '../auth.models';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-login',
  imports: [
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    RouterLink,
    ReactiveFormsModule,
  ],
  templateUrl: './login.html',
})
export class Login {
  protected readonly formBuilder = inject(FormBuilder);
  protected readonly router = inject(Router);
  protected readonly authService = inject(AuthService);
  protected readonly notificacaoService = inject(NotificacaoService);

  protected loginForm: FormGroup = this.formBuilder.group({
    userName: ['', [Validators.required, Validators.minLength(3)]],
    password: ['', [Validators.required, Validators.minLength(6)]],
  });

  get userName() {
    return this.loginForm.get('userName');
  }

  get password() {
    return this.loginForm.get('password');
  }

  public login() {
    if (this.loginForm.invalid) return;

    const loginModel: LoginModel = this.loginForm.value;

    const loginObserver: PartialObserver<AccessTokenModel> = {
      error: (err) => this.notificacaoService.erro(err),
      complete: () => this.router.navigate(['/inicio']),
    };

    this.authService.login(loginModel).subscribe(loginObserver);
  }
}
