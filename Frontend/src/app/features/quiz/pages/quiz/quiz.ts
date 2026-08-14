import {
  Component,
  computed,
  inject,
  signal
} from '@angular/core';

import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

import { QuizStateService } from '../../../../core/services/quiz-state.service';
import { QuizService } from '../../services/quiz.service';

import { FlashcardService } from '../../../flashcards/services/flashcard.service';
import { FlashcardStateService } from '../../../../core/services/flashcard-state.service';

import { QuizAnswer } from '../../models/quiz-answer';
import { QuizResult } from '../../models/quiz-result';

@Component({
  selector: 'app-quiz',
  standalone: true,
  imports: [
    CommonModule
  ],
  templateUrl: './quiz.html',
  styleUrls: ['./quiz.css']
})
export class QuizComponent {

  private readonly quizService =
    inject(QuizService);

  private readonly flashcardService =
    inject(FlashcardService);

  private readonly router =
    inject(Router);


  readonly quizState =
    inject(QuizStateService);

  readonly flashcardState =
    inject(FlashcardStateService);


  readonly questions =
    this.quizState.questions;

  readonly loading =
    this.quizState.loading;

  readonly topic =
    this.quizState.topic;

  readonly selectedAnswers =
    this.quizState.answers;


  readonly result =
    signal<QuizResult | null>(null);

  readonly error =
    signal('');

  readonly currentQuestion =
    signal(0);

  readonly generatingFlashcards =
    signal(false);


  readonly current =
    computed(() => {

      const questions =
        this.questions();

      const index =
        this.currentQuestion();

      return questions[index];

    });


  readonly progressPercentage =
    computed(() => {

      const total =
        this.questions().length;

      if (total === 0) {
        return 0;
      }

      return (
        (this.currentQuestion() + 1) /
        total
      ) * 100;

    });


  selectOption(
    option: string
  ): void {

    this.quizState.answerQuestion(

      this.currentQuestion(),

      option

    );

  }


  nextQuestion(): void {

    if (
      this.currentQuestion() <
      this.questions().length - 1
    ) {

      this.currentQuestion.update(
        value => value + 1
      );

    }

  }


  previousQuestion(): void {

    if (
      this.currentQuestion() > 0
    ) {

      this.currentQuestion.update(
        value => value - 1
      );

    }

  }


  submitQuiz(): void {

    const progressId =
      this.quizState.progressId();


    if (!progressId) {

      this.error.set(
        'No existe un progreso asociado.'
      );

      return;

    }


    const answers: QuizAnswer[] =

      this.questions().map(

        (question, index) => ({

          question:
            question.question,

          givenAnswer:
            this.selectedAnswers()[index] ?? '',

          correctAnswer:
            question.correctAnswer

        })

      );


    this.error.set('');

    this.loading.set(true);


    this.quizService

      .submitQuiz(

        progressId,

        answers

      )

      .subscribe({

        next: (response: QuizResult) => {

          this.result.set(response);

          this.loading.set(false);

        },

        error: () => {

          this.loading.set(false);

          this.error.set(
            'No fue posible enviar el cuestionario.'
          );

        }

      });

  }


  goToFlashcards(): void {

    const progressId =
      this.quizState.progressId();


    if (!progressId) {

      this.error.set(
        'No existe un progreso asociado para consultar las flashcards.'
      );

      return;

    }


    this.error.set('');

    this.generatingFlashcards.set(true);

    this.flashcardState.loading.set(true);


    this.flashcardService

      .getFlashcards(progressId)

      .subscribe({

        next: cards => {

          if (cards.length === 0) {

            this.generatingFlashcards.set(false);

            this.flashcardState.loading.set(false);

            this.error.set(
              'No se encontraron flashcards para este quiz.'
            );

            return;

          }


          this.flashcardState.setCards(

            progressId,

            this.topic(),

            cards

          );


          this.flashcardState.loading.set(false);

          this.generatingFlashcards.set(false);


          this.router.navigate([
            '/flashcards'
          ]);

        },


        error: () => {

          this.flashcardState.loading.set(false);

          this.generatingFlashcards.set(false);

          this.error.set(
            'No fue posible cargar las flashcards.'
          );

        }

      });

  }

}