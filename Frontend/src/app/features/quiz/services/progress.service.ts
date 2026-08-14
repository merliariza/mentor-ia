import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { environment } from '../../../../environments/environment';

import { Progress } from '../models/progress';
import { ProgressRequest } from '../models/progress-request';

@Injectable({
  providedIn: 'root'
})
export class ProgressService {

  private readonly http = inject(HttpClient);

  private readonly api =
    `${environment.apiUrl}/Progress`;

  create(
    request: ProgressRequest
  ): Observable<Progress> {

    return this.http.post<Progress>(
      this.api,
      request
    );

  }

}