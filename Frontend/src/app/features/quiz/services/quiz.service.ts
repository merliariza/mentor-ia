import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { QuizQuestion } from '../models/quiz-question';
import { QuizAnswer } from '../models/quiz-answer';
import { QuizResult } from '../models/quiz-result';

@Injectable({
  providedIn: 'root'
})
export class QuizService {

  private readonly http =
    inject(HttpClient);

  private readonly api =
    'http://localhost:5253/api/Evaluation';

  generateQuiz(
    progressId: number
  ): Observable<QuizQuestion[]> {

    return this.http.post<QuizQuestion[]>(

      `${this.api}/generate-quiz/${progressId}`,

      {}

    );

  }

  submitQuiz(
    progressId: number,
    answers: QuizAnswer[]
  ): Observable<QuizResult> {

    return this.http.post<QuizResult>(

      `${this.api}/submit-quiz`,

      {

        progressId,

        answers

      }

    );

  }

}