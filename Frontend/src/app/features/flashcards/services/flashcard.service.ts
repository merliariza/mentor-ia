import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { Flashcard } from '../models/flashcard';

@Injectable({
  providedIn: 'root'
})
export class FlashcardService {

  private readonly http =
    inject(HttpClient);

  private readonly api =
    'http://localhost:5253/api/Evaluation';


  getFlashcards(
    progressId: number
  ): Observable<Flashcard[]> {

    return this.http.get<Flashcard[]>(

      `${this.api}/flashcards/${progressId}`

    );

  }

}