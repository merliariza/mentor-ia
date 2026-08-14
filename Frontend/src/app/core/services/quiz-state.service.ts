import { Injectable, signal } from '@angular/core';

import { QuizQuestion } from '../../features/quiz/models/quiz-question';

@Injectable({
  providedIn: 'root'
})
export class QuizStateService {

  readonly progressId =
    signal<number | null>(null);

  readonly topic =
    signal<string>('');

  readonly questions =
    signal<QuizQuestion[]>([]);

  readonly answers =
    signal<Record<number, string>>({});

  readonly score =
    signal<number | null>(null);

  readonly loading =
    signal(false);

  setQuiz(
    progressId: number,
    topic: string,
    questions: QuizQuestion[]
  ) {

    this.progressId.set(progressId);

    this.topic.set(topic);

    this.questions.set(questions);

    this.answers.set({});

    this.score.set(null);

  }

  answerQuestion(
    index: number,
    answer: string
  ) {

    this.answers.update(current => ({

      ...current,

      [index]: answer

    }));

  }

  clear() {

    this.progressId.set(null);

    this.topic.set('');

    this.questions.set([]);

    this.answers.set({});

    this.score.set(null);

    this.loading.set(false);

  }

}