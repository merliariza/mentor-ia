import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormBuilder,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';
import { Router, RouterLink } from '@angular/router';

import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterLink
  ],
  templateUrl: './register.html',
  styleUrls: ['./register.css']
})
export class RegisterComponent {

  private readonly fb = inject(FormBuilder);
  private readonly auth = inject(AuthService);
  private readonly router = inject(Router);

  readonly loading = signal(false);

  error = '';

  form = this.fb.nonNullable.group({

    fullName: [
      '',
      Validators.required
    ],

    email: [
      '',
      [
        Validators.required,
        Validators.email
      ]
    ],

    password: [
      '',
      [
        Validators.required,
        Validators.minLength(6)
      ]
    ],

    confirmPassword: [
      '',
      Validators.required
    ]

  });

  register(): void {

    this.error = '';

    this.form.markAllAsTouched();

    if (this.form.invalid) {
      return;
    }

    const value = this.form.getRawValue();

    if (value.password !== value.confirmPassword) {

      this.error = 'Las contraseñas no coinciden.';

      return;

    }

    this.loading.set(true);

    this.auth.register({

      fullName: value.fullName,
      email: value.email,
      password: value.password

    }).subscribe({

      next: () => {

        this.loading.set(false);

        this.router.navigate(['/login']);

      },

      error: err => {

        this.loading.set(false);

        this.error =
          err?.error?.message ??
          err?.error?.Message ??
          'No fue posible registrar el usuario.';

      }

    });

  }

}