import { Injectable, computed, inject, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { tap } from 'rxjs';

import { environment } from '../../../../environments/environment';

import { LoginRequest } from '../models/login-request';
import { LoginResponse } from '../models/login-response';
import { RegisterRequest } from '../models/register-request';


@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private readonly http = inject(HttpClient);


  private readonly api =
    `${environment.apiUrl}/auth`;


  readonly token = signal<string | null>(
    localStorage.getItem('token')
  );


  readonly currentUser = signal<LoginResponse | null>(
    this.loadUser()
  );


  readonly isAuthenticated = computed(
    () => !!this.token()
  );


  login(request: LoginRequest) {

    return this.http
      .post<LoginResponse>(
        `${this.api}/login`,
        request
      )
      .pipe(

        tap(response => {

          this.saveSession(response);

        })

      );

  }


  register(request: RegisterRequest) {

    return this.http
      .post(
        `${this.api}/register`,
        request
      );

  }


  logout() {

    localStorage.removeItem('token');

    localStorage.removeItem('user');

    this.token.set(null);

    this.currentUser.set(null);

  }


  getCurrentUserId(): number | null {

    const token = this.token();

    if (!token) {

      return null;

    }


    try {

      const parts = token.split('.');

      if (parts.length !== 3) {

        return null;

      }


      const payload = JSON.parse(
        atob(parts[1])
      );


      if (
        payload.id === undefined ||
        payload.id === null
      ) {

        return null;

      }


      const userId = Number(payload.id);


      if (Number.isNaN(userId)) {

        return null;

      }


      return userId;

    } catch {

      return null;

    }

  }


  private saveSession(
    response: LoginResponse
  ) {

    localStorage.setItem(
      'token',
      response.token
    );


    localStorage.setItem(
      'user',
      JSON.stringify(response)
    );


    this.token.set(
      response.token
    );


    this.currentUser.set(
      response
    );

  }


  private loadUser(): LoginResponse | null {

    const user =
      localStorage.getItem('user');


    if (!user) {

      return null;

    }


    try {

      return JSON.parse(user);

    } catch {

      localStorage.removeItem('user');

      return null;

    }

  }

}